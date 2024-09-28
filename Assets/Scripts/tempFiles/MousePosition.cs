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
    public float radiusCheck = 1f;

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

    // Ref: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
    private void checkNearby()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusCheck, layerToLookFor);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Found Something!");
        }

    }

}
