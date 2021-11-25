using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [Header("Player")]
    public GunHandler player;

    [Header("UI Objects")]
    public Image chamber1; // FRONT slot
    public Image chamber2; // SECONDARY slot
    public GameObject pauseUI;

    [Header("Ability Icons")]
    public Sprite img_grapple;
    public Sprite img_timestop;
    public Sprite img_none;

    [Header("States")]
    public bool isPaused = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<GunHandler>();
    }

    void ManageChamber()
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

    void PauseUI()
    {
        switch (isPaused)
        {
            case true:
                pauseUI.SetActive(true);
                Time.timeScale = 0;
                break;
            case false:
                pauseUI.SetActive(false);
                Time.timeScale = 1;
                break;
        }
    }
    
    public void SetPaused()
    {
        isPaused = !isPaused;
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void Update()
    {
        ManageChamber();
        PauseUI();

        // Key Inputs
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            player.GetComponent<Player>().Restart();
        }
    }
}
