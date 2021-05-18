using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SnakeMovement : MonoBehaviourPun
{
    public float speed = 3f;
    public float rotationSpeed = 200f;

    private float horizontal = 0f;
    [SerializeField] private Snake snake;

    private void Awake()
    {
        //string headSpritePath = snake.GetHeadSpritePath(snake.GetPlayerColour());
        //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(headSpritePath);
    }

    void Update()
    {
        SetHorizontal();
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // input movement
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
            transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void SetHorizontal()
    {
        if (Input.touchCount > 0 && photonView.IsMine)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("killsPlayer") && photonView.IsMine)
        {
            snake.SetDeadPlayer();
            speed = 0;
            rotationSpeed = 0;

            //FindObjectOfType<GameManager>().EndGame();
        }
    }
}
