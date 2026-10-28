using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
	public Vector2 Direccion => direccion;
    private Rigidbody2D rb;
    private InputAction moverAccion;
    private Vector2 direccion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Definimos la acción de mover directamente en código
        moverAccion = new InputAction("Mover", InputActionType.Value);

        moverAccion.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        moverAccion.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");

        moverAccion.AddBinding("<Gamepad>/leftStick");
    }

    private void OnEnable()  => moverAccion.Enable();
    private void OnDisable() => moverAccion.Disable();

    private void Update()
    {
        direccion = moverAccion.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);
    }
}