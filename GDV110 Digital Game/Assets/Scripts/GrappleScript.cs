using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    [Header("Grapple Objects")]
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;

    [Header("Grapple Values")]
    public float minGrappleDistance = 0.5f;
    public float maxGrappleDistance = 10.0f;

    private Vector3 m_TempPos;

    [Header("Where I'm Grappling")]
    private bool objectMoving;
    public Transform objGrappling;
    public Vector3 objGrapplingVector;

    void Start()
    {
        _distanceJoint.enabled = false;
    }
    
    void Update()
    {
        if (GetComponent<Player>().isGrappling)
        {
            // Set position 1 to player's position
            _lineRenderer.SetPosition(1, transform.position);   
            if (objGrappling.tag == "Loose Object")
            {
                _lineRenderer.SetPosition(0, objGrappling.position);
                _distanceJoint.connectedAnchor = objGrappling.position;
            }

            // Check if grapple is within disance
            if (objGrappling.tag == "Loose Object")
            {
                if (Vector3.Distance(transform.position, objGrappling.position) > maxGrappleDistance)
                {
                    StopGrapple();
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, objGrapplingVector) > maxGrappleDistance)
                {
                    StopGrapple();
                }
            }

            // If holding right click, reduce grapple?
            if (Input.GetMouseButton(1))
            {
                _distanceJoint.distance -= 0.05f;
                if (_distanceJoint.distance < minGrappleDistance)
                {
                    StopGrapple();  // Stop grappling if distance is too small.
                }
            }
        }

    }

    public bool FireGrapple(Vector3 _bulletPos, GameObject _collideobj)
    {   
        if (GetComponent<Player>().isGrappling) return false;   // Failed to grapple: currently grappling
        // def object
        objGrappling = _collideobj.transform;
        // is grappling
        GetComponent<Player>().isGrappling = true;

        // line renderer related
        _lineRenderer.SetPosition(0, _bulletPos);
        _lineRenderer.SetPosition(1, transform.position);
        _lineRenderer.enabled = true;

        // distance joint related
        _distanceJoint.connectedAnchor = _bulletPos;
        _distanceJoint.enabled = true;

        // set bullet pos
        objGrapplingVector = _bulletPos;

        return true;
    }

    public bool StopGrapple()
    {
        if (!GetComponent<Player>().isGrappling) return false;  // Unable to cancel grapple: quit script.        
        // play sound
        GetComponent<AudioSource>().PlayOneShot(GetComponent<GunHandler>().snd_gun_fire_endgrapple);
        // is not grappling
        GetComponent<Player>().isGrappling = false;         // Set grappling to false
        // disconnect object
        objGrappling = null;
        // disable distancejoint & line renderer
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
        return true;                 // Grapple cancel success.
    }
}
