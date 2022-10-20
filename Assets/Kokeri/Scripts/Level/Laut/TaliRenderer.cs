using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaliRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform startPos;

    private float line_width = 0.05f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = line_width;
        lineRenderer.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    public void RenderLine(Vector3 endPosition, bool enableRender)
    {
        if (enableRender)
        {
            if (!lineRenderer.enabled)
                lineRenderer.enabled = true;

            lineRenderer.positionCount = 2;

        }
        else{

            lineRenderer.positionCount = 0;

            if (lineRenderer.enabled)
                lineRenderer.enabled = false;

        }

        if (lineRenderer.enabled)
        {
            Vector3 temp = startPos.position;
            temp.z = -10f;

            startPos.position = temp;
            temp = endPosition;
            temp.z = 0f;

            endPosition = temp;

            lineRenderer.SetPosition(0, startPos.position);
            lineRenderer.SetPosition(1, endPosition);
            
        }

    }
}
