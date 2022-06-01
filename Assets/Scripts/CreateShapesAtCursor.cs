using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShapesAtCursor : MonoBehaviour
{
    public Camera cam;
    public DrawScript toolType;
    int held;
    int heldsize;
    LineRenderer line=null;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        held = toolType.tool;
        heldsize = toolType.toolSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        if (held != toolType.tool)
        {
            switch (toolType.tool)
            {
                case 2:
                    DrawCircle(toolType.toolSize);
                    break;
                case 3:
                    drawSquare(toolType.toolSize);
                    break;
                default:
                    Destroy(line);
                    break;
            }
            held = toolType.tool;
        }
        else if (toolType.toolSize != heldsize)
        {
            switch (toolType.tool)
            {
                case 2:
                    UpdateCircle(toolType.toolSize);
                    break;
                case 3:
                    UpdateSquare(toolType.toolSize);
                    break;
            }
            heldsize = toolType.toolSize;
        }
    }

    //created from : https://www.loekvandenouweland.com/content/use-linerenderer-in-unity-to-draw-a-circle.html
    void DrawCircle(float radius)
    {
        radius /= 2;
        var segments = 360;
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.startWidth = toolType.lineWidth;
            line.endWidth = toolType.lineWidth;
        }
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }

    void UpdateCircle(float radius)
    {
        radius /= 2;
        var segments = 360;
        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }

    void drawSquare (float size)
    {
        size /= 2;
        size -= .25f;
        var segments = 4;
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.startWidth = toolType.lineWidth;
            line.endWidth = toolType.lineWidth;
        }
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the square
        var points = new Vector3[pointCount];
        points[0] = new Vector3(size, 0, size);
        points[1] = new Vector3(-size, 0, size);
        points[2] = new Vector3(-size, 0, -size);
        points[3] = new Vector3(size, 0, -size);
        points[4] = new Vector3(size, 0, size);
        line.SetPositions(points);
    }

    void UpdateSquare(float size)
    {
        size /= 2;
        size -= .25f;
        var segments = 4;
        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the square
        var points = new Vector3[pointCount];
        points[0] = new Vector3(size, 0, size);
        points[1] = new Vector3(-size, 0, size);
        points[2] = new Vector3(-size, 0, -size);
        points[3] = new Vector3(size, 0, -size);
        points[4] = new Vector3(size, 0, size);
        line.SetPositions(points);
    }
}
