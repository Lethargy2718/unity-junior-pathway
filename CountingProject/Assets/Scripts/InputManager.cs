using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterMotor motor;
    private PlayerLook playerLook;

    private Vector2 moveInput;

    private void Awake()
    {
        controls = new PlayerControls();
        playerLook = GetComponent<PlayerLook>();
        motor = GetComponent<CharacterMotor>();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();        

        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Dash.performed += ctx => motor.Dash(moveInput);

        controls.Player.Jump.performed += ctx => motor.Jump();

        controls.Player.Look.performed += ctx => playerLook.Look(ctx.ReadValue<Vector2>());

        controls.Player.Sprint.performed += ctx => motor.ToggleSprint(true);
        controls.Player.Sprint.canceled += ctx => motor.ToggleSprint(false);
    }

    private void Update()
    {
        motor.Move(moveInput);
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
}
