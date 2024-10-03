using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Variables")]
    public float speed = 5;
    public float sprint = 7.5f;
    private Vector2 movementInput;
    private float currentSpeed;

    [Header("Picking up Variables")]

    // Checks for elements in a certain radius listed here
    public float radiusCheck = .5f;

    // Checks for elements in a certain layer listed here
    public LayerMask layerToLookFor;

    // Collider array that deals with picking up items
    private List<colliderSpecs> listOfPossibleColliders;

    [Header("Current Item Variables")]
    private GameObject currentPick;
    private bool holding = false;
    private bool hasItem = false;
    private float holdDuration = 1f;
    private float holdTime = 0f;
    private Pickup currentItem;

    // Functions similarly to pairs so that developers can store multiple vcariables under an object in a list
    [System.Serializable]
    public class colliderSpecs
    {
        // Reference to a collider within a certain area
        public Collider collider;
        // Stores the type of object that a certain collider is
        public int collderObjectType;
        // References the priority of any given action
        public float priority;
    }

    void Start(){
        currentSpeed = speed;
    }

    // When the player is initially awakened, then the list of possible colliders is set to a fresh list
    private void Awake()
    {
        listOfPossibleColliders = new List<colliderSpecs>();
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

        // TODO: FIX!!!
        // This allows players to pick up items behind them, and may limit grabbing any items in front of them
        // System works but look into fixing later

        // Gets an array of colliders that overlap a new sphere in a specific layer
        Collider[] hitColliders = Physics.OverlapCapsule(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                                                         new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                                                            radiusCheck, layerToLookFor);

        foreach (var hitCollider in hitColliders)
        {

            // Debug.Log("Found Something!");

            // hitCollider.gameObject.GetComponent<Pickup>();
            // Debug.Log(hitCollider.gameObject.name);

            // Checks is a game onbject has the pick up script
            if (hitCollider.gameObject.GetComponent<Pickup>() != null)
            {

                maybeDoThis(hitCollider, 1);

                // Sanity check
                //Debug.Log(hitCollider.gameObject.name + " is pick up type " + hitCollider.gameObject.GetComponent<Pickup>().debugType());

            }
            else if (hitCollider.gameObject.GetComponent<PlacingArea>() != null)
            {

                maybeDoThis(hitCollider, 2);

                // Sanity check
                //Debug.Log(hitCollider.gameObject.name + " is placing area type " + hitCollider.gameObject.GetComponent<PlacingArea>().debugType());

            }
            else
            {
                Debug.Log(hitCollider.gameObject.name + " does not have the one of the interactable script!");
            }

        }

        doSomething();

    }

    // colliderObjectType == 0 => button
    // colliderObjectType == 1 => pickup
    // colliderObjectType == 2 => placementArea

    public int maybeDoThis(Collider hitCollider, int colliderObjectType)
    {

        // Sanity checks to make sure the right elements have the appropriate scripts
        // These also check the current state of the player and interacted element
        switch (colliderObjectType)
        {
            case 0:
                if (hitCollider.gameObject.GetComponent<InGameButton>() == null)
                {
                    Debug.Log(hitCollider.gameObject.name + " doesn't have the In Game Button Script!");
                    return -1;
                }
                else if (hasItem)
                {
                    Debug.Log("Careful " + gameObject.name+ "! You have an item in hand!");
                    return -1;
                }
                break;
            case 1:
                if (hitCollider.gameObject.GetComponent<Pickup>() == null)
                {
                    Debug.Log(hitCollider.gameObject.name + " doesn't have the Pickup Script!");
                    return -1;
                }
                else if (hasItem)
                {
                    Debug.Log("Careful " + gameObject.name + "! You have an item in hand!");
                    return -1;
                }
                break;
            case 2:
                if (hitCollider.gameObject.GetComponent<PlacingArea>() == null)
                {
                    Debug.Log(hitCollider.gameObject.name + "  doesn't have the Placing Area Script");
                    return -1;
                }
                else if (hitCollider.gameObject.GetComponent<PlacingArea>().hasItem && hasItem)
                {
                    Debug.Log("Careful " + gameObject.name + "! You can't place items on top of each other!");
                    return -1;
                }
                else if (!hitCollider.gameObject.GetComponent<PlacingArea>().hasItem && !hasItem)
                {
                    Debug.Log("Careful " + gameObject.name + "! You can't place items on top of each other!");
                    return -1;
                }
                break;
            default:
                Debug.Log(colliderObjectType + " is not yet implemented in the maybeDoThis function!");
                return -1;
        }

        // creates an empty collider specs object
        colliderSpecs someNewSpecs = new colliderSpecs();

        // Attaches tossed in vars into the fresh specs object
        someNewSpecs.collider = hitCollider;
        someNewSpecs.collderObjectType = colliderObjectType;
        someNewSpecs.priority = -1.0f;

        listOfPossibleColliders.Add(someNewSpecs);

        return 1;

    }

    private void throwObject(){
        currentItem.ThrowObject(); //calls function in pickup to throw the object
    }

    // Once all of the elements have been tossed into the collider array, checks the left over possible elements and decides what it can do
    public void doSomething()
    {
        // Checks if there's anything in the list
        if (listOfPossibleColliders.Count == 0)
        {
            Debug.Log(gameObject.name + " says there's nothing to do!");
        }
        else
        {
            // If there is, it will interact with the first element in the array
            doThisThing(listOfPossibleColliders[0]);
        }

    }

    public void doThisThing (colliderSpecs someColldierSpecs)
    {
        
        switch (someColldierSpecs.collderObjectType)
        {
            // TODO: Comment This!!!!!!
            case 1:
                Pickup grabber = someColldierSpecs.collider.gameObject.GetComponent<Pickup>();
                if (grabber.currentlyHeld && grabber.currentPlayer == gameObject)
                {
                    grabber.ItemDropped();
                    currentItem = null; //Drops the item if the player has one and is the one holding it.
                    hasItem = false;
                    return;
                }
                else if (grabber.currentlyHeld && grabber.currentPlayer != gameObject)
                {
                    return; //Stops other players from interating with object in hand.
                }
                grabber.ItemGrabbed(gameObject);
                currentItem = grabber; // grabs the item and remembers the item
                hasItem = true;
                return;
            default:
                Debug.Log("Some error in doThisThing!!!!");
                return;
        }

    }

    private void StartTimer(){
        holdTime += Time.deltaTime; //Timer for throwing
    }
}
