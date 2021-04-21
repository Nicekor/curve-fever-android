using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SnakeMovement : MonoBehaviourPun
{
    public float speed = 3f;
    public float rotationSpeed = 200f;

    private float horizontal = 0f;
    private Dictionary<string, float> BOUNDARIES;

    private void Awake()
    {
        float boundX = Camera.main.orthographicSize - 0.1f;
        float boundY = Camera.main.orthographicSize * 2 - 0.1f;
        BOUNDARIES = new Dictionary<string, float>() {
            { "topX",  boundX},
            { "botX", -boundX },
            { "leftY", -boundY },
            { "rightY", boundY },
        };
    }

    void Update()
    {
        SetHorizontal();
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // boundaries
            if (transform.position.y <= BOUNDARIES["botX"] || transform.position.y >= BOUNDARIES["topX"] ||
            transform.position.x <= BOUNDARIES["leftY"] || transform.position.x >= BOUNDARIES["rightY"])
            {
                Tail.Instance.StopDrawCoroutine();
                speed = 0;
                rotationSpeed = 0;
            }

            // input movement
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
            transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.fixedDeltaTime);
        }
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
        if (collision.CompareTag("killsPlayer") && photonView.IsMine)
        {
            print("collision");
            Tail.Instance.StopDrawCoroutine();
            speed = 0;
            rotationSpeed = 0;
            // todo: use singleton instead of this (this is expensive)
            //FindObjectOfType<GameManager>().EndGame();
        }
    }
}
