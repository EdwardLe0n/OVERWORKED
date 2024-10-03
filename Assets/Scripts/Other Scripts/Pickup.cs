using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool Pucked = false;
    public GameObject currentHolder;
    private Rigidbody rb;
    public bool currentlyHeld = false;
    public float dropDistance = 1f;
    public float throwForce = 5f;

    public float type;

    public string debugType()
    {

        switch (type)
        {
            case 1:
                return "human";
            case 2:
                return "object";
            default:
                return "error";
        }
    }
    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if(Pucked){
            transform.position = new Vector3(currentHolder.transform.position.x, currentHolder.transform.position.y + 1, currentHolder.transform.position.z);
            transform.rotation = currentHolder.transform.rotation; //sets position and rotation to be with the holder.
        }
    }

    public void ItemGrabbed(GameObject holder){
        if(!Pucked){
            gameObject.transform.SetParent(holder.transform);
            Pucked = true;
            currentlyHeld = true; //Sets the state so the item is grabbed by a holder
            rb.useGravity = false;
            currentHolder = holder;
        }
    }

    public void ItemDropped(){
        gameObject.transform.SetParent(null);
        currentlyHeld = false; //Sets state back to not being picked up
        Vector3 dropPosition = currentHolder.transform.position + currentHolder.transform.forward * dropDistance;
        gameObject.transform.position = dropPosition; //Drops the object infront of the holder
        Pucked = false;
        rb.useGravity = true;
        currentHolder = null;
    }

    public void ThrowObject(){
        gameObject.transform.SetParent(null);
        currentlyHeld = false; //Sets state back to not being picked up
        
        rb.AddForce(transform.forward.normalized * throwForce, ForceMode.Impulse);
        //Throws the object infront of the holder throwing it.

        Pucked = false;
        rb.useGravity = true;
        currentHolder = null;
    }

}
