using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    // Getting current mouse position
    private Vector3 m_MousePos;
    private Camera m_Camera;

    [Header("Grapple Values")]
    public bool IsGrappling;
    private DistanceJoint2D m_DistanceJoint;
    private LineRenderer m_LineRenderer;

    private Vector3 m_TempPos;

    void Start()
    {
        m_Camera = Camera.main; // Set camera
        m_DistanceJoint = GetComponent<DistanceJoint2D>();
        m_LineRenderer = GetComponent<LineRenderer>();

        // Initialize Grapple Values
        m_DistanceJoint.enabled = false;
        IsGrappling = false;
        m_LineRenderer.positionCount = 0;
    }

    void Update()
    {
        GetMousePos();
        DrawLine();                                         // Draw grapple line

        // Get key input, check if not already grappling.
        if (Input.GetMouseButtonDown(0) && !IsGrappling)
        {
            m_DistanceJoint.enabled = true;
            m_DistanceJoint.connectedAnchor = m_MousePos;
            m_LineRenderer.positionCount = 2;

            m_TempPos = m_MousePos;

            IsGrappling = true;                             // Set grappling to true
        }
        else if (Input.GetMouseButtonDown(0))
        {
            m_DistanceJoint.enabled = false;
            m_LineRenderer.positionCount = 0;

            IsGrappling = false;                            // Set grappling to false
        }
    }

    private void DrawLine()
    {
        if (m_LineRenderer.positionCount <= 0) return;
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, m_TempPos);
    }

    private void GetMousePos()  // Get position of mouse relative to camera.
    {
        m_MousePos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
