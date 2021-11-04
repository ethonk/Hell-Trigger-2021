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
        m_DistanceJoint = GetComponent<DistanceJoint2D>();
        m_LineRenderer = GetComponent<LineRenderer>();

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
        if (!GetComponent<Player>().isGrappling)             // Unable to grapple: quit script.
        {
            print(m_DistanceJoint.gameObject.name);
            m_DistanceJoint.enabled = true;                 // Enable distance joint.
            m_DistanceJoint.connectedAnchor = new Vector2(_bulletPos.x, _bulletPos.y);   // Anchor player to point.
            m_LineRenderer.positionCount = 2;               // Enable line renderer
            m_TempPos = _bulletPos;                         // Pivot is equal to mouse point.

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
