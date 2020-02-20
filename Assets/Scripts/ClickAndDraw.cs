using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDraw : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public Rigidbody rigidBody;
    public List<Vector2> mousePos;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(tempMousePos, mousePos[mousePos.Count - 1]) > .1f)
            {
                UpdateLine(tempMousePos);
            }
        }

        GameObject CreateLineCollider(Vector3 point1, Vector3 point2, float width)
        {
            GameObject obj = new GameObject("LineCollider");
            obj.transform.position = (point1 + point2) / 2;
            obj.transform.right = (point2 - point1).normalized;

            BoxCollider boxCollider = obj.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3((point2 - point1).magnitude, width, width);

            return obj;
        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        rigidBody = currentLine.GetComponent<Rigidbody>();
        mousePos.Clear();
        mousePos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, mousePos[0]);
        lineRenderer.SetPosition(1, mousePos[1]);
    }

    void UpdateLine(Vector2 newMousePos)
    {
        mousePos.Add(newMousePos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newMousePos);
    }
}
