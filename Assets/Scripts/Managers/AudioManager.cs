using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource pillowShoot;
    public AudioSource pillowHit;
    public AudioSource itemBonked;
    public AudioSource humanBonked;
    public AudioSource humanDied;
    public AudioSource taskComplete;

    private LevelManager levelManager;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // runs every time new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // play bg music if scene is a level (only levels contain LevelManager)
        levelManager = FindAnyObjectByType<LevelManager>();
        if(levelManager != null) {
            backgroundMusic.Play(); // need to restart music if level bc start will not be called again (bc DontDestroyOnLoad)
        }
        else {
            backgroundMusic.Stop();
        }
    }

    void Start()
    {
        backgroundMusic.Play();
        PillowGun.ShotGun += PillowShot;
        Pillow.pillowHit += PillowHit;
        Pickup.bonk += PickupItem;
        HumanDie.bonk += HumanItem;
        HumanDie.died += HumanDied;
        WorkStation.done += TaskComplete;
    }

    void OnDestroy()
    {
        PillowGun.ShotGun -= PillowShot;
        Pillow.pillowHit -= PillowHit;
        Pickup.bonk -= PickupItem;
        HumanDie.bonk -= HumanItem;
        HumanDie.died -= HumanDied;
        WorkStation.done -= TaskComplete;
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

    public void HumanItem(){
        humanBonked.Play();
    }

    public void HumanDied(){
        humanDied.Play();
    }

    public void TaskComplete(){
        taskComplete.Play();
    }
}
