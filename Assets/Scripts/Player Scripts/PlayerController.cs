using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float sprint = 7.5f;
    public float radiusCheck = .5f;
    public LayerMask layerToLookFor;
    private Vector2 movementInput;
    private float currentSpeed;
    private GameObject currentPick;
    private bool holding = false;
    private bool hasItem = false;
    private float holdDuration = 1f;
    private float holdTime = 0f;
    private Pickup currentItem;
    void Start(){
        currentSpeed = speed;
    }
    void Update()
    {
        // Movement
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * currentSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Rotation
        if (movement != Vector3.zero)
        {
            Quaternion Rotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotation, 720 * Time.deltaTime);
        }

        if(hasItem){
            currentSpeed = speed; //turns of sprint if they have object
        }

        if (holding && hasItem) {
            StartTimer(); //Timer for the the throwing.
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnSprint(InputAction.CallbackContext ctx){
        if(ctx.started || ctx.performed){
            if(!hasItem){
                currentSpeed = sprint;
            } //Lets the player sprint and stops them from them from sprinting if they have an object
        } else if (ctx.canceled) {
            currentSpeed = speed;
        }
    }

    public void OnPickup(InputAction.CallbackContext ctx){
        if (ctx.started){
            holdTime = 0f; //Resets the timer.
            holding = true;
        }else if (ctx.canceled){
            if (holdTime < holdDuration){
                checkNearby(); //grabs or drops item if timer is smaller than duration.
            }
            if (holdTime >= holdDuration) {
                throwObject();
            }
            holding = false;
            holdTime = 0f; //stops timer and resets.
        }
    }

    private void checkNearby()
    {

        // Gets an array of colliders that overlap a new sphere in a specific layer
        Collider[] hitColliders = Physics.OverlapCapsule(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                                                         new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                                                            radiusCheck, layerToLookFor);
        foreach (var hitCollider in hitColliders)
        {

            // Debug.Log("Found Something!");

            // hitCollider.gameObject.GetComponent<Pickup>();
            // Debug.Log(hitCollider.gameObject.name);

            if (hitCollider.gameObject.GetComponent<Pickup>() != null)
            {
                //does something
                Pickup grabber = hitCollider.gameObject.GetComponent<Pickup>();
                if(grabber.currentlyHeld && grabber.currentPlayer == gameObject){
                    grabber.ItemDropped();
                    currentItem = null; //Drops the item if the player has one and is the one holding it.
                    hasItem = false;
                    return;
                } else if(grabber.currentlyHeld && grabber.currentPlayer != gameObject){
                    return; //Stops other players from interating with object in hand.
                }
                grabber.ItemGrabbed(gameObject);
                currentItem = grabber; // grabs the item and remembers the item
                hasItem = true;
                return;
            }
            else
            {
                Debug.Log(hitCollider.gameObject.name + " does not have the pick up script!");
            }

        }

    }

    private void throwObject(){
        currentItem.ThrowObject(); //calls function in pickup to throw the object
    }

    private void StartTimer(){
        holdTime += Time.deltaTime; //Timer for throwing
    }
}
