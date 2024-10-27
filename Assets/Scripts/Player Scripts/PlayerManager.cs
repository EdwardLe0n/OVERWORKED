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
            StartCoroutine(DelaySpawn(1));
        }
    }

    public void SpawnPlayer2(InputAction.CallbackContext ctx){
        if(!player2){
            player2 = true; //Spawns player2
            StartCoroutine(DelaySpawn(2));
        }
    }

    // wait for player inputs to be ready before creating the player
    IEnumerator DelaySpawn(int playerNum)
    {
        yield return new WaitForSeconds(0.1f);
        if(playerNum == 1) {
            Instantiate(player1Prefab, transform.position, Quaternion.identity);
        }
        else {
            Instantiate(player2Prefab, transform.position, Quaternion.identity);
        }
    }
}
