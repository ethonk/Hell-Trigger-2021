using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    public enum BulletType {Grapple, Freeze};
    public List<int> bulletIndexes;

    [Header("Gun Settings")]
    public int chamberSize;                     // How many bullets does this gun hold at a time?
    
    public float reloadSpeed;                   // How fast does this gun reload (seconds)?
    public float bulletSpeed;                   // How fast will the bullet travel?
    public float bulletLife;                    // How far long the bullet travel?
    public float fireDelay;                     // How often can the gun fire (seconds)?

    [Header("Gun Properties")]
    public List<BulletType> gunChamber;

    [Header("Gun Shooting Properties")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("References")]
    public GameObject UI;

    [Header("Audio")]
    public AudioClip snd_gun_fire;              // Sound played on generic fire.
    public AudioClip snd_gun_fire_grapple;      // Sound played on grapple fire.
    public AudioClip snd_gun_fire_endgrapple;   // Sound played on grapple end fire.
    public AudioClip snd_gun_fire_timestop;     // Sound played on time stop fire.
    public AudioClip snd_gun_reload;            // Sound played on reload.
    public AudioClip snd_gun_reload_complete;   // Sound played upon completed reload.
    public AudioClip snd_gun_cycle;             // Sound played when bullets are cycled.

    [Header("States")]
    public bool canFire = true;

    // == FUNCTIONS ==
    public void LoadGun()   // Load the gun with bullets equivalent to chamber size and available bullet types.
    {
        // Copy bullet index list
        var bulletIndexCopy = new List<int>(bulletIndexes);
        
        // Randomly select indexes for chamber load.
        for (int i = 0; i < chamberSize; i++)
        {
            int randBulletIndex = Random.Range(bulletIndexCopy[0], bulletIndexCopy.Count);   // Select random bullet index.
            gunChamber.Add((BulletType)randBulletIndex);                    // Add bullet type to the gun chamber.

            // Delete index option from list.
            for (int x = 0; x < bulletIndexCopy.Count; x++)
            {
                if (bulletIndexCopy[x] == randBulletIndex) bulletIndexCopy.Remove(bulletIndexCopy[x]);
                
            }                                         
        }
    }

    public IEnumerator Reload()
    {
        GetComponent<AudioSource>().PlayOneShot(snd_gun_reload);            // Play reload sound.
        gunChamber.Clear();                                                 // Clear all bullets from the gun.
        yield return new WaitForSeconds(reloadSpeed);                       // Wait reload speed.
        GetComponent<AudioSource>().PlayOneShot(snd_gun_reload_complete);   // Play reload sound.
        yield return new WaitForSeconds(0.5f);                              // Small reload completion delay.
        LoadGun();                                                          // Load the gun.
    }
    
    public IEnumerator FireDelay()
    {
        canFire = false;
        yield return new WaitForSeconds(fireDelay);
        canFire = true;
    }

    public IEnumerator FireGun()
    {
        if (!GetComponent<Player>().isGrappling)                     // Only commit to firing if the player isn't currently grappling.
        {
            if (canFire && gunChamber.Count > 0)                            // Check if we can fire and the chamber has at least 1 bullet.
            {
                #region Physical bullet firing
                    GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    newBullet.GetComponent<Bullet>().bulletType = gunChamber[0];
                #endregion

                // Play corresponding firing sounds
                switch(gunChamber[0])
                {
                    case BulletType.Grapple:
                        GetComponent<AudioSource>().PlayOneShot(snd_gun_fire_grapple);      // Play fire sound.
                        break;
                
                    case BulletType.Freeze:
                        GetComponent<AudioSource>().PlayOneShot(snd_gun_fire_timestop);      // Play fire sound.
                        break;
                }

                // Bullet management
                gunChamber.Remove(gunChamber[0]);                           // Delete the top bullet
                StartCoroutine(FireDelay());                                // Account for fire delay.

                // Check if chamber is empty, if so reload the gun.
                if (gunChamber.Count == 0) 
                {
                    yield return new WaitForSeconds(fireDelay);
                    StartCoroutine(Reload());
                }
            }
        }
        else
        {
            GetComponent<GrappleScript>().StopGrapple();
        }
    }

    public void OnBulletHit(BulletType _bulletType, Vector3 _bulletPos, GameObject _collideObj)
    {
        switch(_bulletType)
        {
            case BulletType.Grapple:
                GetComponent<GrappleScript>().FireGrapple(_bulletPos, _collideObj);
                break;
                
            case BulletType.Freeze:
                if (_collideObj.tag == "Loose Object")
                {
                    _collideObj.GetComponent<Object>().ApplyTimestop();
                }
                break;
        }
    }

    void CycleBullet()
    {
        if (gunChamber.Count > 1)
        {
            // Play a sound
            GetComponent<AudioSource>().PlayOneShot(snd_gun_cycle);

            // Swap
            var temp = gunChamber[0];
            gunChamber[0] = gunChamber[1];
            gunChamber[1] = temp;
        }
    }

    // == CORE START AND UPDATE ==
    void Start()
    {
        // Generate index of bullets to choose from relative to amount of bullet types.
        for (int i = 0; i < System.Enum.GetValues(typeof(BulletType)).Length; i++)
        {
            bulletIndexes.Add(i);
        }

        // Load the gun on game start.
        LoadGun();

        // Define UI
        UI = GameObject.Find("UI");
    }

    void Update()
    {
        if (UI.GetComponent<UIHandler>().isPaused == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(FireGun());
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                CycleBullet();
            }
        }
    }
}
