using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timestop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ApplyTimestop(GameObject _collideObj)
    {
        print ("freeze nigga, the niggas name is: " + _collideObj.name);
    }
}
