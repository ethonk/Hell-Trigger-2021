using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public GameObject player;
    [SerializeField] [Range(0f,4f)] float lerpTime;
    [SerializeField] Vector2[] positions;

    int posIndex = 0;
    int length;
    float t = 0f;

    public bool frozen = false;
    [Header("Sound")]
    public AudioClip snd_timestop_resume;

    void Start()
    {
        length = positions.Length;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!frozen)
        {
            transform.position = Vector2.Lerp(transform.position, positions[posIndex], lerpTime*Time.deltaTime);

            t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
            if (t > .9f)
            {
                t = 0;
                posIndex++;
                posIndex = (posIndex >= length) ? 0 : posIndex;
            }
        }
    }

    // Handle freeze    
    public IEnumerator Freeze(float _freezeTime)
    {
        frozen = true;
        yield return new WaitForSeconds(_freezeTime);
        GetComponent<AudioSource>().PlayOneShot(snd_timestop_resume);
        frozen = false;
    }

    // Keep player attached.
    public void OnCollisionEnter2D(Collision2D col)
    {
        print("hi " + col.gameObject.name);
        col.transform.SetParent(transform);
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        print("bye " + col.gameObject.name);
        col.transform.SetParent(null);
    }
}