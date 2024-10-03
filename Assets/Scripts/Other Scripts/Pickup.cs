using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool Pucked = false;
    public GameObject currentPlayer;
    private Rigidbody rb;
    public bool currentlyHeld = false;
    public float dropDistance = 1f;
    public float throwForce = 5f;
    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if(Pucked){
            transform.position = new Vector3(currentPlayer.transform.position.x, currentPlayer.transform.position.y + 1, currentPlayer.transform.position.z);
            transform.rotation = currentPlayer.transform.rotation; //sets position and rotation to be with the player.
        }
    }

    public void ItemGrabbed(GameObject player){
        if(!Pucked){
            gameObject.transform.SetParent(player.transform);
            Pucked = true;
            currentlyHeld = true; //Sets the state so the item is grabbed by a player
            rb.useGravity = false;
            currentPlayer = player;
        }
    }

    public void ItemDropped(){
        gameObject.transform.SetParent(null);
        currentlyHeld = false; //Sets state back to not being picked up
        Vector3 dropPosition = currentPlayer.transform.position + currentPlayer.transform.forward * dropDistance;
        gameObject.transform.position = dropPosition; //Drops the object infront of the player
        Pucked = false;
        rb.useGravity = true;
        currentPlayer = null;
    }

    public void ThrowObject(){
        gameObject.transform.SetParent(null);
        currentlyHeld = false; //Sets state back to not being picked up
        
        rb.AddForce(transform.forward.normalized * throwForce, ForceMode.Impulse);
        //Throws the object infront of the player throwing it.

        Pucked = false;
        rb.useGravity = true;
        currentPlayer = null;
    }

}
