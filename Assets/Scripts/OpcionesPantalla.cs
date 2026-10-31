using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesPantalla : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownResolucion;
    [SerializeField] private Toggle togglePantallaCompleta;

    private const string ClaveResolucion = "Resolucion";
    private const string ClavePantallaCompleta = "PantallaCompleta";

    private Resolution[] resoluciones;

    private void Start()
    {
        PrepararResoluciones();
        PrepararPantallaCompleta();
    }

    private void PrepararResoluciones()
    {
        // Quitamos duplicados (misma resolución con distinta frecuencia de refresco)
        var lista = new List<Resolution>();
        var textos = new List<string>();

        foreach (Resolution r in Screen.resolutions)
        {
            string texto = r.width + " x " + r.height;
            if (textos.Contains(texto)) continue;

            textos.Add(texto);
            lista.Add(r);
        }

        resoluciones = lista.ToArray();

        dropdownResolucion.ClearOptions();
        dropdownResolucion.AddOptions(textos);

        // Resolución guardada, o la actual si es la primera vez
        int indiceActual = 0;
        for (int i = 0; i < resoluciones.Length; i++)
        {
            if (resoluciones[i].width == Screen.width &&
                resoluciones[i].height == Screen.height)
            {
                indiceActual = i;
                break;
            }
        }

        int indice = PlayerPrefs.GetInt(ClaveResolucion, indiceActual);
        indice = Mathf.Clamp(indice, 0, resoluciones.Length - 1);

        dropdownResolucion.SetValueWithoutNotify(indice);
        dropdownResolucion.RefreshShownValue();
        AplicarResolucion(indice);

        dropdownResolucion.onValueChanged.AddListener(AplicarResolucion);
    }

    private void PrepararPantallaCompleta()
    {
        bool completa = PlayerPrefs.GetInt(ClavePantallaCompleta, Screen.fullScreen ? 1 : 0) == 1;

        togglePantallaCompleta.SetIsOnWithoutNotify(completa);
        Screen.fullScreen = completa;

        togglePantallaCompleta.onValueChanged.AddListener(CambiarPantallaCompleta);
    }

    private void AplicarResolucion(int indice)
    {
        Resolution r = resoluciones[indice];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
        PlayerPrefs.SetInt(ClaveResolucion, indice);
    }

    private void CambiarPantallaCompleta(bool completa)
    {
        Screen.fullScreen = completa;
        PlayerPrefs.SetInt(ClavePantallaCompleta, completa ? 1 : 0);
    }
}