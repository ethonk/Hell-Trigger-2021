using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
