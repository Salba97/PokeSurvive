using UnityEngine;
using UnityEngine.UI;

public class OpcionesAudio : MonoBehaviour
{
    [SerializeField] private Slider sliderVolumen;

    private const string ClaveVolumen = "Volumen";

    private void Start()
    {
        // Cargamos el volumen guardado (1 = máximo por defecto)
        float volumen = PlayerPrefs.GetFloat(ClaveVolumen, 1f);

        sliderVolumen.SetValueWithoutNotify(volumen);
        AudioListener.volume = volumen;

        sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
    }

    private void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat(ClaveVolumen, valor);
    }
}