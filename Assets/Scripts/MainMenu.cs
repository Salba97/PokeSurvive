using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string nombreEscenaJuego = "Game";

    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    public void Opciones()
    {
        // De momento no hace nada
        Debug.Log("Opciones: aún sin implementar");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}