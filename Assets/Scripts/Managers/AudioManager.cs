using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource pillowShoot;
    public AudioSource pillowHit;
    public AudioSource itemBonked;
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
