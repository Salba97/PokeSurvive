using UnityEngine;

public class CamaraSeguir : MonoBehaviour
{
    [SerializeField] private Transform objetivo;
    [SerializeField] private float suavizado = 0.15f;

    private Vector3 velocidadActual;

    private void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 destino = new Vector3(
            objetivo.position.x,
            objetivo.position.y,
            transform.position.z); // mantenemos la Z de la cámara

        transform.position = Vector3.SmoothDamp(
            transform.position, destino, ref velocidadActual, suavizado);
    }
}