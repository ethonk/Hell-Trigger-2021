using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    public Transform m_Player;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {   
        transform.position = new Vector3 (m_Player.position.x + offset.x, m_Player.position.y + offset.y, offset.z);
    }
}
