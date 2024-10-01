using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using this tutorial for a debug mouse tempalate
// https://youtu.be/5NTmxDSKj-Q?si=n_eNzbcABHTYMnwQ

public class MousePosition : MonoBehaviour
{

    // Reference to the mouse position relative to the screen 
    public Vector3 screenPosition;

    // Ref to the actual world position relative to the camera
    public Vector3 worldPosition;

    // Ref of layers that we want the ray to hit
    public LayerMask layersToHit;

    // Ref of layers we want to look for
    public LayerMask layerToLookFor;

    // Radius of the space to check sphere wise
    public float radiusCheck = .5f;

    // Collider array that deals with the
    private List<colliderSpecs> listOfPossibleColliders;


    [System.Serializable]
    public class colliderSpecs
    {
        public Collider collider;
        public int collderObjectType;
        public float priority;
    }

    private void Awake()
    {
        listOfPossibleColliders = new List<colliderSpecs>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickEvent();
        }
    }

    // For debug purpose, will only check whats nearby on a mouse click, as to simulate a player hitting the interact button
    private void clickEvent()
    {

        // Gets where the mouse is on the screen
        screenPosition = Input.mousePosition;

        // creates a ray from the camera to the point a player is pointing at with a mouse
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);


        // Checks the output of the ray (w/ a max distance of 100  & whilst avoiding some layers) and sets..
        // the vector 3 called world position to the point that has been hit, with a buffer to the y var
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, layersToHit))
        {
            worldPosition = hitData.point;
            worldPosition.y += .5f;
        }

        // sets the world position to the transform of the current object
        transform.position = worldPosition;

        checkNearby();

    }

    // Checks all nearby elements in the scene
    // Gonna use overlap Sphere as a tempalate of what to look for

    // Ref: https://docs.unity3d.com/ScriptReference/Physics.OverlapCapsule.html
    private void checkNearby()
    {

        // Clears the list of possible colliders
        listOfPossibleColliders.Clear();

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
                Debug.Log(hitCollider.gameObject.name + " is pick up type " + hitCollider.gameObject.GetComponent<Pickup>().debugType());

            }
            else if (hitCollider.gameObject.GetComponent<PlacingArea>() != null)
            {

                maybeDoThis(hitCollider, 2);

                // Sanity check
                Debug.Log(hitCollider.gameObject.name + " is placing area type " + hitCollider.gameObject.GetComponent<PlacingArea>().debugType());

            }
            else
            {
                Debug.Log(hitCollider.gameObject.name + " does not have the pick up script!");
            }

        }

        doSomething();

    }

    // colliderObjectType == 0 => button
    // colliderObjectType == 1 => pickup
    // colliderObjectType == 2 => placementArea


    public void maybeDoThis( Collider hitCollider, int colliderObjectType)
    {

        colliderSpecs someNewSpecs = new colliderSpecs();

        someNewSpecs.collider = hitCollider;
        someNewSpecs.collderObjectType = colliderObjectType;
        someNewSpecs.priority = -1.0f;

        listOfPossibleColliders.Add(someNewSpecs);

    }

    // Once all of the elements have been tossed into the collider array, checks the left over possible elements and decides what it can do
    public void doSomething()
    {
        
    }

    // returns the context of a player
    // 0 == free hands
    public int getContext ()
    {

        return 0;

    }

}
