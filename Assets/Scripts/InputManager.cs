using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
	public delegate void StartTouchEvent(Vector2 position, float time);
	public event StartTouchEvent OnStartTouch;

	private InputActions inputActions;

	private void Awake()
	{
		inputActions = new InputActions();
	}

	private void OnEnable()
	{
		inputActions.Enable();
	}

	private void OnDisable()
	{
		inputActions.Disable();
	}

	private void Start()
	{
		inputActions.Player.TouchPress.started += ctx => TouchPerformed(ctx);
	}

	void TouchPerformed(InputAction.CallbackContext ctx)
	{
		print("Touch performed " + inputActions.Player.TouchPosition.ReadValue<Vector2>());
		OnStartTouch?.Invoke(inputActions.Player.TouchPosition.ReadValue<Vector2>(), (float)ctx.startTime);
	}
}