using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Properties")]
    public bool is_CrushingBlock;

    [Header("Interpolation")]
    [SerializeField] [Range(0f,4f)] float lerpTime;
    [SerializeField] Vector2[] positions;
    int posIndex = 0;
    int length;
    float t = 0f;

    [Header("Time Stop")]
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
        if (col.transform.gameObject.layer == 8)    // if a player
        {
            if (is_CrushingBlock)   // kill instead
            {
                col.gameObject.GetComponent<Player>().KillPlayer();
            }
        }
        col.transform.SetParent(transform); // stick player on
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        col.transform.SetParent(null);
    }
}