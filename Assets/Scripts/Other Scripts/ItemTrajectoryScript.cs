using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrajectoryScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Pickup pickUp;
    private Rigidbody rb;

    public int numPoints = 20;
    private float timeStep = 0.1f;

    private void Awake(){
        lineRenderer = GetComponent<LineRenderer>();//Gets the component for the line renderer
        pickUp = GetComponentInParent<Pickup>();//Gets the component for parent pickup script
        rb = pickUp.GetComponent<Rigidbody>();//Gets the component for the rigidbody of the pickup object
    }

    private void OnEnable(){
        lineRenderer.positionCount = numPoints;
    }

    void Update(){
        float customGravity = pickUp.GetCustomGravity();
        Vector3 gravity = new Vector3(0, customGravity, 0);
        float throwForce = pickUp.GetThrowSpeed();//Gets the throw values for the object
        Vector3 direction = pickUp.GetDirection();

        Vector3 launchVelocity = direction * throwForce;
        Vector3 position = pickUp.transform.position;//Gets position and the throw direction in local variables

        lineRenderer.SetPosition(0, position);//The position of the object is where the line starts.

        Vector3 nextVel = launchVelocity;
        Vector3 nextPos = position;//The next position of the line variables

        int currentPoint = 1;

        for (int i = 1; i < numPoints; i++){
            nextVel += gravity * timeStep;
            nextVel *= (1 - rb.drag * timeStep);//Uses the gravity values and the drag of the object to try and fix the issue of how the item hits the floor before the line. 

            nextPos += nextVel * timeStep;

            RaycastHit hit;//Uses raycast to see if it will hit
            if (Physics.Linecast(position, nextPos, out hit))
            {
                if (i < lineRenderer.positionCount)
                {
                    lineRenderer.SetPosition(i, hit.point);
                }

                currentPoint = i + 1;//Stops the line renderer if the path hits an object
                break;
            }

            if (i < lineRenderer.positionCount)
            {
                lineRenderer.SetPosition(i, nextPos);//Makes the line keep going until it hits something.
            }
            position = nextPos;
            currentPoint = i + 1;
        }
        lineRenderer.positionCount = currentPoint;//Make the current point into the position
    }

    private void OnDisable(){
        lineRenderer.positionCount = 0;//Reset the line.
    }
}
