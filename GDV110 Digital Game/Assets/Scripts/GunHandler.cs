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
    public float fireDelay;                     // How often can the gun fire (seconds)?

    [Header("Gun Properties")]
    public List<BulletType> gunChamber;

    [Header("Audio")]
    public AudioClip snd_gun_fire;              // Sound played on fire.
    public AudioClip snd_gun_reload;            // Sound played on reload.
    public AudioClip snd_gun_reload_complete;   // Sound played upon completed reload.

    [Header("States")]
    bool canFire = true;
    
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
        LoadGun();                                                          // Load the gun.
        GetComponent<AudioSource>().PlayOneShot(snd_gun_reload_complete);   // Play reload sound.
    }
    
    public IEnumerator FireDelay()
    {
        canFire = false;
        yield return new WaitForSeconds(fireDelay);
        canFire = true;
    }

    public IEnumerator FireGun()
    {
        if (canFire && gunChamber.Count > 0)                            // Check if we can fire and the chamber has at least 1 bullet.
        {
            GetComponent<AudioSource>().PlayOneShot(snd_gun_fire);      // Play fire sound.
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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FireGun());
        }
    }
}
