using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TransicionEscena : MonoBehaviour
{
    [SerializeField] private string nombreEscena = "MainMenu";

    [Header("Fade")]
    [SerializeField] private CanvasGroup fade;
    [SerializeField] private float duracionFade = 1.5f;

    [Header("Sonido")]
    [SerializeField] private AudioSource sonidoTransicion;

    private bool cambiando;

    private void Start()
    {
        // Aseguramos que empieza transparente
        fade.alpha = 0f;
        fade.blocksRaycasts = false;
    }

    private void Update()
    {
        if (cambiando) return;

        bool teclado = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;
        bool mando   = Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame;
        bool raton   = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        if (teclado || mando || raton)
            StartCoroutine(CambiarEscena());
    }

    public void IniciarTransicion()
    {
        if (!cambiando) StartCoroutine(CambiarEscena());
    }

    private IEnumerator CambiarEscena()
    {
        cambiando = true;
        fade.blocksRaycasts = true;

        if (sonidoTransicion != null)
            sonidoTransicion.Play();

        // Empezamos a cargar la escena en segundo plano, sin activarla aún
        AsyncOperation carga = SceneManager.LoadSceneAsync(nombreEscena);
        carga.allowSceneActivation = false;

        float tiempo = 0f;
        while (tiempo < duracionFade)
        {
            tiempo += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(tiempo / duracionFade);
            fade.alpha = Mathf.SmoothStep(0f, 1f, t);
            yield return null;
        }
        fade.alpha = 1f;

        // Esperamos a que la escena esté lista (se queda en 0.9 hasta activarse)
        while (carga.progress < 0.9f)
            yield return null;

        carga.allowSceneActivation = true;
    }
}