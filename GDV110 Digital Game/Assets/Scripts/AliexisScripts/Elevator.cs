using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    //starting position - automatically set at the start
    public float StartPos;
    //ending position - set this for end destination (how high)
    public float EndPos;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
