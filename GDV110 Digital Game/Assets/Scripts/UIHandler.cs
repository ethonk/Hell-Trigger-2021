using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Player")]
    public GunHandler player;

    [Header("UI Objects")]
    public Image chamber1; // FRONT slot
    public Image chamber2; // SECONDARY slot

    [Header("Ability Icons")]
    public Sprite img_grapple;
    public Sprite img_timestop;
    public Sprite img_none;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<GunHandler>();
    }

    void Update()
    {
        // chamber 1
        if (player.gunChamber.Count == 0)
        {
            chamber1.sprite = img_none;
        }
        else
        {
            switch (player.gunChamber[0])
            {
                case GunHandler.BulletType.Grapple:
                    chamber1.sprite = img_grapple;
                    break;
                case GunHandler.BulletType.Freeze:
                    chamber1.sprite = img_timestop;
                    break;
                default:
                    chamber1.sprite = img_none;
                    break;
            }
        }

        // chamber 2
        if (player.gunChamber.Count < 2)
        {
            chamber2.sprite = img_none;
        }
        else
        {
            switch (player.gunChamber[1])
            {
                case GunHandler.BulletType.Grapple:
                    chamber2.sprite = img_grapple;
                    break;
                case GunHandler.BulletType.Freeze:
                    chamber2.sprite = img_timestop;
                    break;
                default:
                    chamber2.sprite = img_none;
                    break;
            }
        }
    }
}
