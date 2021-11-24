using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignScript : MonoBehaviour
{
    [Header("Sign")]
    public GameObject sign;

    [Header("Sign Properties")]
    public string sign_text;
    
    void Start()
    {
        // Set sign text
        sign.GetComponent<TextMeshPro>().text = sign_text;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.root.gameObject.layer == 8)
        {
            sign.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.root.gameObject.layer == 8)
        {
            sign.gameObject.SetActive(false);
        }
    }
}
