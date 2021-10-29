using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    // Getting current mouse position
    private Vector3 m_MousePos;
    private Camera m_Camera;

    [Header("Grapple Values")]
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
        GetComponent<Player>().isGrappling = false;
        m_LineRenderer.positionCount = 0;
    }
    
    void Update()
    {
        GetMousePos();
        DrawLine();                                         // Draw grapple line
    }

    public bool FireGrapple()
    {   
        if (GetComponent<Player>().isGrappling) return false;   // Unable to grapple: quit script.
            
        // Raycast, check if touching collidable object
        RaycastHit2D hit = Physics2D.Raycast(m_MousePos, transform.TransformDirection(Vector2.up), GetComponent<GunHandler>().bulletRange);
        if (hit.collider != null && hit.collider.name == "Tilemap_Bounds")
        {
            m_DistanceJoint.enabled = true;                 // Enable distance joint.
            //GetComponent<Rigidbody2D>().gravityScale = 0;   // Disable gravity.
            m_DistanceJoint.connectedAnchor = m_MousePos;   // Anchor player to point.
            m_LineRenderer.positionCount = 2;               // Enable line renderer
            m_TempPos = m_MousePos;                         // Pivot is equal to mouse point.
            GetComponent<Player>().isGrappling = true;      // Set grappling to true
            return true;                                    // Grapple success.
        }
        return false;
    }

    public bool StopGrapple()
    {
        if (!GetComponent<Player>().isGrappling) return false;  // Unable to cancel grapple: quit script.        

        m_DistanceJoint.enabled = false;
        m_LineRenderer.positionCount = 0;
        //GetComponent<Rigidbody2D>().gravityScale = 3;       // Enable gravity.
        GetComponent<Player>().isGrappling = false;         // Set grappling to false
        return true;                                        // Grapple cancel success.
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
