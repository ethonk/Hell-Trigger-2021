using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timestop : MonoBehaviour
{   
    public int Stopduration;
    public GameObject cam;
    public void Start()
    {
        cam = GameObject.Find("Main Camera");
    }
    
    public void ApplyTimestop(GameObject _collideObj)
    {
        MonoBehaviour CamMono = cam.GetComponent<MonoBehaviour>();
        print ("freeze nigga, the niggas name is: " + _collideObj.name);
        Debug.Log("Active? "+gameObject.activeInHierarchy);
        CamMono.StartCoroutine(TimeStopCoroutine(_collideObj));
    }

    IEnumerator TimeStopCoroutine(GameObject _StoppedObj)
    {   
        _StoppedObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(Stopduration);
        _StoppedObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}
