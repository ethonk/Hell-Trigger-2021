using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed_current;
    public float moveSpeed_default;

    [Header("States")]
    public bool isGrappling = false;
    public Transform sign;

    public void KillPlayer()
    {
        if (sign == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            transform.position = sign.position;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // When touching the sign, assign checkpoint.
        if (col.transform.tag == "Sign")
        {
            sign = col.transform;
        }
    }
}
