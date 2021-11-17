using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Variables")]
    //starting position - automatically set at the start
    public float StartPos;
    //ending position - set this for end destination (how high)
    public float EndPos;
    public float LerpValue;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Object>().timestopped)
        {
            
        }
    }
}
