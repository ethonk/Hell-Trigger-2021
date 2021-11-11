using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float length, startpos;
    public GameObject camera;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float distance = (camera.transform.position.x * parallaxEffect);    // how far moved in world space

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.x);
    }
}
