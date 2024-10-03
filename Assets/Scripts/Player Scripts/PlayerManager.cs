using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    private bool player1 = false;
    private bool player2 = false;

    public void SpawnPlayer1(InputAction.CallbackContext ctx){
        if(!player1){
            player1 = true; //Spawns player1
            Instantiate(player1Prefab, transform.position, Quaternion.identity);
        }
    }

    public void SpawnPlayer2(InputAction.CallbackContext ctx){
        if(!player2){
            player2 = true; //Spawns player2
            Instantiate(player2Prefab, transform.position, Quaternion.identity);
        }
    }
}
