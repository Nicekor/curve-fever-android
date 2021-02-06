using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour
{
    public float pointSpacing = .1f;
    public Transform head;
    public GameObject TailPrefab;

	private LineRenderer line;
    private List<Vector2> points;
    private EdgeCollider2D col;
    private bool draw = true;
    private IEnumerator drawCoroutine;
    private (float, float) deltaGap = (0.3f, 0.6f);

    private void Start()
    {
		line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();

        points = new List<Vector2>();

        SetPoint();

        drawCoroutine = SetDraw();
        StartCoroutine(drawCoroutine);

    }

    private IEnumerator SetDraw()
    {
        while (draw)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            draw = false;
            yield return new WaitForSeconds(Random.Range(deltaGap.Item1, deltaGap.Item2));
            Instantiate(TailPrefab, transform.parent);
            StopCoroutine(drawCoroutine);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(points.Last(), head.position) > pointSpacing && draw)
        {
            SetPoint();
        }
    }

    private void SetPoint()
	{
        if (points.Count > 1)
        {
            col.points = points.ToArray<Vector2>();
        }

        points.Add(head.position);

        line.positionCount = points.Count;
		line.SetPosition(points.Count - 1, head.position);
    }

}
