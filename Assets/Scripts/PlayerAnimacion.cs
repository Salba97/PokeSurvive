using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimacion : MonoBehaviour
{
    [SerializeField] private PlayerController controlador;
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float tiempoEntreFrames = 0.2f;
    [SerializeField] private bool girarAlMoverseIzquierda = true;

    private SpriteRenderer sr;
    private float temporizador;
    private int frameActual;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 dir = controlador.Direccion;
        bool moviendose = dir.sqrMagnitude > 0.01f;

        if (moviendose)
        {
            temporizador += Time.deltaTime;

            if (temporizador >= tiempoEntreFrames)
            {
                temporizador = 0f;
                frameActual = (frameActual + 1) % frames.Length;
                sr.sprite = frames[frameActual];
            }

            if (girarAlMoverseIzquierda && Mathf.Abs(dir.x) > 0.01f)
                sr.flipX = dir.x > 0;
        }
        else
        {
            // Al parar, volvemos al primer frame
            temporizador = 0f;
            frameActual = 0;
            sr.sprite = frames[0];
        }
    }
}