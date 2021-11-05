using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    [Header("Grapple Values")]
    private Vector3 m_TempPos;

    void Start()
    {
        // Initialize Grapple Values
        GetComponent<DistanceJoint2D>().enabled = false;
        GetComponent<Player>().isGrappling = false;
        GetComponent<LineRenderer>().positionCount = 0;
    }
    
    void Update()
    {
        DrawLine();                                         // Draw grapple line
    }

    public bool FireGrapple(Vector3 _bulletPos)
    {   
        if (!GetComponent<Player>().isGrappling)             // Unable to grapple: quit script.
        {
            GetComponent<DistanceJoint2D>().enabled = true;                 // Enable distance joint.
            GetComponent<DistanceJoint2D>().connectedAnchor = new Vector2(_bulletPos.x, _bulletPos.y);   // Anchor player to point.
            GetComponent<LineRenderer>().positionCount = 2;               // Enable line renderer
            m_TempPos = _bulletPos;                         // Pivot is equal to mouse point.

            GetComponent<Player>().isGrappling = true;      // Set grappling to true
            return true;                                    // Grapple success.
        }
        return false;
        
    }

    public bool StopGrapple()
    {
        if (!GetComponent<Player>().isGrappling) return false;  // Unable to cancel grapple: quit script.        

        GetComponent<DistanceJoint2D>().enabled = false;
        GetComponent<LineRenderer>().positionCount = 0;

        GetComponent<Player>().isGrappling = false;         // Set grappling to false
        return true;                                        // Grapple cancel success.
    }

    private void DrawLine()
    {
        if (GetComponent<LineRenderer>().positionCount <= 0) return;
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, m_TempPos);
    }
}
