using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Pickup : MonoBehaviour
{
    [Range(0, -10)]
    public float customGravity;
    private Vector3 gravity;
    [Range(0, 20)]
    public float throwSpeed;
    private bool Pucked = false;
    public GameObject currentHolder;
    private Rigidbody rb;
    public bool currentlyHeld = false;
    public float dropDistance = 1f;
    public float throwForce = 5f;

    // I know this is pretty redundant but I didn't want to just change existing code -Andrew
    public bool IsPickedUp{
        get { return Pucked; }
    }

    public float type;
    public ItemTrajectoryScript itemTrajectory;

    public delegate void PickupBonked();
    public static event PickupBonked bonk;

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
        rb.useGravity = false;
        gravity = new Vector3(0, customGravity, 0);
        itemTrajectory = GetComponentInChildren<ItemTrajectoryScript>();
        itemTrajectory.enabled = false;

    }

    void Update(){
        if(Pucked){
            transform.position = new Vector3(currentHolder.transform.position.x, currentHolder.transform.position.y + 1, currentHolder.transform.position.z);
            transform.rotation = currentHolder.transform.rotation; //sets position and rotation to be with the holder.
        }
        if(!currentlyHeld){
            ApplyCustomGravity();//Applys the custom gravity to the game object. Only if not being held.
        }   
    }

    public void ItemGrabbed(GameObject holder){
        if(!Pucked){
            gameObject.transform.SetParent(holder.transform);
            Pucked = true;
            currentlyHeld = true; //Sets the state so the item is grabbed by a holder
            currentHolder = holder;
        }
    }

    public void ItemDropped(){
        gameObject.transform.SetParent(null);
        currentlyHeld = false; //Sets state back to not being picked up
        Vector3 dropPosition = currentHolder.transform.position + currentHolder.transform.forward * dropDistance;
        gameObject.transform.position = dropPosition; //Drops the object infront of the holder
        Pucked = false;
        currentHolder = null;
    }

    public void ThrowObject(){
        gameObject.transform.SetParent(null);
        currentlyHeld = false; //Sets state back to not being picked up
        
        rb.AddForce(transform.forward.normalized * throwSpeed, ForceMode.Impulse);
        //Throws the object infront of the holder throwing it.

        Pucked = false;
        currentHolder = null;
    }

    private void OnCollisionEnter(Collision collision){
        bonk.Invoke();
    }

    public void EnableLineRenderer(){
        itemTrajectory.enabled = true;//enables the line renderer
    }

    public void DisableLineRenderer(){
        itemTrajectory.enabled = false;//Disables the line renderer
    }

    public void ApplyCustomGravity()
    {
        rb.AddForce(gravity, ForceMode.Acceleration);//Adds the artificial gravity to the items.
    }

    public float GetCustomGravity() => customGravity;
    public float GetThrowSpeed() => throwSpeed;//Gets all three values for the throwing of the item so it can be used for the line renderer
    public Vector3 GetDirection() => transform.forward.normalized;
}
