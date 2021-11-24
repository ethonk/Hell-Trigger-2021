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
        sign.GetComponent<TextMeshPro>().text = sign_text;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.root.gameObject.layer == 8)
        {
            sign.gameObject.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.root.gameObject.layer == 8)
        {
            sign.gameObject.SetActive(false);
        }
    }
}
