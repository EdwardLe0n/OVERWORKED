using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource pillowShoot;
    public AudioSource pillowHit;
    public AudioSource itemBonked;

    private void Awake()
    {
        // Basic logic to make sure there's only ever one instance of the sound manager

        GameObject[] soundManagers = GameObject.FindGameObjectsWithTag("Sound Manager");

        if (soundManagers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }

    }

    void Start()
    {
        backgroundMusic.Play();
        PillowGun.ShotGun += PillowShot;
        Pillow.pillowHit += PillowHit;
        Pickup.bonk += PickupItem;
    }

    void OnDestroy(){
        PillowGun.ShotGun -= PillowShot;
        Pillow.pillowHit -= PillowHit;
        Pickup.bonk -= PickupItem;
    }

    public void PillowShot(){
        pillowShoot.Play();
    }

    public void PillowHit(){
        pillowHit.Play();
    }


    public void PickupItem(){
        itemBonked.Play();
    }
}
