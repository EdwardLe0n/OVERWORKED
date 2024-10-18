using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    public AudioSource playerWalking;

    public void PlayAudio(){
        playerWalking.Play();
    }

    public void StopAudio(){
        playerWalking.Stop();
    }
}
