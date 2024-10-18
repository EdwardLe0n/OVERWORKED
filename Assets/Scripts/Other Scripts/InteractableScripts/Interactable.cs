using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float productivity;
    public float emotional;
    public float radiusCheck = .5f;
    public LayerMask layerToLookFor;
    public List<colliderSpecs> listOfPossibleColliders;

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

    public virtual void UseItem(){
        
    }
    
}
