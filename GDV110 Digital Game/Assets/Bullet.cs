using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GunHandler gun;
    public GunHandler.BulletType bulletType;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * gun.bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            print("hit a block at: " + transform.position);
            gun.OnBulletHit(bulletType, transform.position);
        }
        Destroy(gameObject);
    }
}
