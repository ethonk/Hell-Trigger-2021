using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    [Header("Grapple Values")]
    public DistanceJoint2D m_DistanceJoint;
    public LineRenderer m_LineRenderer;

    private Vector3 m_TempPos;

    void Start()
    {
        // Initialize Grapple Values
        m_DistanceJoint.enabled = false;
        GetComponent<Player>().isGrappling = false;
        m_LineRenderer.positionCount = 0;
    }
    
    void Update()
    {
        DrawLine();                                         // Draw grapple line
    }

    public bool FireGrapple(Vector3 _bulletPos)
    {   
        if (GetComponent<Player>().isGrappling) return false;   // Unable to grapple: quit script.
 
        m_DistanceJoint.enabled = true;                 // Enable distance joint.
        //GetComponent<Rigidbody2D>().gravityScale = 0;   // Disable gravity.
        m_DistanceJoint.connectedAnchor = _bulletPos;   // Anchor player to point.
        m_LineRenderer.positionCount = 2;               // Enable line renderer
        m_TempPos = _bulletPos;                         // Pivot is equal to mouse point.
        GetComponent<Player>().isGrappling = true;      // Set grappling to true
        return true;                                    // Grapple success.
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
}
