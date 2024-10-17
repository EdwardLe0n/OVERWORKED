using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowGun : Interactable
{
    public GameObject Pillow;
    public GameObject spawnPoint;

    public delegate void ItemShot();
    public static event ItemShot ShotGun;

    public override void UseItem()
    {
        Debug.Log("Pillow Gun");
        Instantiate(Pillow, spawnPoint.transform.position, transform.rotation);
        ShotGun.Invoke();
    }
}
