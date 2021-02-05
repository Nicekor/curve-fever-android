using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour
{
    public float pointSpacing = .1f;
    public Transform snake;

	private LineRenderer line;
    private List<Vector2> points;
    private EdgeCollider2D col;
    private bool draw = true;

    private void Start()
    {
		line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();

        points = new List<Vector2>();

        SetPoint();

        StartCoroutine(SetDraw());

    }

    private IEnumerator SetDraw()
	{
        while(true)
		{
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            draw = false;
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            draw = true;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(points.Last(), snake.position) > pointSpacing)
        {
            if (draw)
			{
                SetPoint();
            }
            else
			{
                gameObject.AddComponent(typeof(LineRenderer));
			}
        }
    }

    private void SetPoint()
	{
        if (points.Count > 1)
        {
            col.points = points.ToArray<Vector2>();
        }

        points.Add(snake.position);

        line.positionCount = points.Count;
		line.SetPosition(points.Count - 1, snake.position);
    }

}
