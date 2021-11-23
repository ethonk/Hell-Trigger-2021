using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingBlockKill : MonoBehaviour
{
    public GameObject crushingBlock;

    void Start()
    {
        crushingBlock = transform.root.gameObject;
    }
    
    // Detect collision with player
    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits a solid object, kill.
        if (col.transform.root.gameObject.layer == 8 && !crushingBlock.GetComponent<CrushingBlock>().frozen)
        {
            col.transform.root.GetComponent<Player>().KillPlayer();
        }
    }
}
