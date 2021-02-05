using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
	public float speed = 3f;
	public float rotationSpeed = 200f;

	private float horizontal = 0f;

	void Update()
	{
		SetHorizontal();
	}

	void FixedUpdate()
	{
		transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
		// for reverse rotation I can use just horizontal (without the minus value)
		transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.fixedDeltaTime);
	}

	private void SetHorizontal()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Stationary)
			{
				if (touch.position.x > Screen.width / 2)
				{
					horizontal = 1f;
				}

				if (touch.position.x < Screen.width / 2)
				{
					horizontal = -1f;
				}
			}
			else
			{
				horizontal = 0f;
			}

		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("killsPlayer"))
		{
			speed = 0;
			rotationSpeed = 0;
			// todo: use singleton instead of this (this is expensive)
			FindObjectOfType<GameManager>().EndGame();

		}
	}
}
