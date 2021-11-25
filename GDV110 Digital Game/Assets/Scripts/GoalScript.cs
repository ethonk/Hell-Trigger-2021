using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    public string SceneName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.root.gameObject.layer == 8)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        }
    }
}
