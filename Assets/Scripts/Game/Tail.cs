using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : Singleton<Tail>
{
    [SerializeField] private float pointSpacing = .1f;
    [SerializeField] private Transform head;
    [SerializeField] private GameObject TailPrefab;

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
        SetColour();
        points = new List<Vector2>();

        SetPoint();

        drawCoroutine = SetDraw();
        StartCoroutine(drawCoroutine);
    }

    private void SetColour()
    {
        string colourHex = (string)PhotonNetwork.LocalPlayer.CustomProperties["colour"];
        Color playerColour;
        ColorUtility.TryParseHtmlString("#" + colourHex, out playerColour);
        line.startColor = playerColour;
        line.endColor = playerColour;
    }
    private void SetPoint()
    {
        if (points.Count > 1)
        {
            col.points = points.ToArray<Vector2>();
        }

        // this solution is kinda hacky (?) Ask someone for a better one
        Vector2 parentPos = GetComponentInParent<Transform>().position;
        col.offset = new Vector2(-parentPos.x, -parentPos.y);
        
        points.Add(head.position);

        line.positionCount = points.Count;
        line.SetPosition(points.Count - 1, head.position);
    }

    private IEnumerator SetDraw()
    {
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        draw = false;
        yield return new WaitForSeconds(Random.Range(deltaGap.Item1, deltaGap.Item2));
        Instantiate(TailPrefab, transform.parent);
        StopCoroutine(drawCoroutine);
    }

    public void StopDrawCoroutine()
    {
        if (drawCoroutine != null) StopCoroutine(drawCoroutine);
    }

    private void Update()
    {
        if (Vector3.Distance(points.Last(), head.position) > pointSpacing && draw)
        {
            SetPoint();
        }
    }


}
