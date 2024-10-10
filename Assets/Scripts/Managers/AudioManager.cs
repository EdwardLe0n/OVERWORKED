using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource playerWalking;
    void Start()
    {
        PlayerController.StartWalkingAudio += StartWalking;//The delegate for the walking sound
        PlayerController.StopWalkingAudio += StopWalking;
        backgroundMusic.Play();
    }

    void OnDestroy(){
        PlayerController.StartWalkingAudio += StartWalking;//The delegate for the walking sound
        PlayerController.StopWalkingAudio += StopWalking;
    }

    public void StartWalking(){
        playerWalking.Play();//Starts the walking audio
    }

    public void StopWalking(){
        playerWalking.Stop();//Stops the walking audio
    }
}
