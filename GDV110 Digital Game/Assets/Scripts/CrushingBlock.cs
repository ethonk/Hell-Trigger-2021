using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingBlock : MonoBehaviour
{
    public bool frozen = false;
    public GameObject crushHitbox;

    [Header("Sound")]
    public AudioClip snd_timestop_resume;

    public IEnumerator Freeze(float _freezeTime)
    {
        frozen = true;
        yield return new WaitForSeconds(_freezeTime);
        GetComponent<AudioSource>().PlayOneShot(snd_timestop_resume);
        frozen = false;
    }

    void Update()
    {
        // Detect frozen
        if (frozen)
        {
            crushHitbox.GetComponent<Animator>().speed = 0;
        }
        else
        {
            crushHitbox.GetComponent<Animator>().speed = 1;
        }
    }
}
