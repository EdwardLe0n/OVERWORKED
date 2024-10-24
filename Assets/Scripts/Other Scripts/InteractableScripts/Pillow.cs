using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Pillow : MonoBehaviour
{
    [Range(0, -10)]
    public float customGravity;
    private Vector3 gravity;
    [Range(0, 20)]
    public float throwSpeed;
    private Rigidbody rb;
    public delegate void PillowHit();
    public static event PillowHit pillowHit;
    private Energy energy;
    private Mood mood;

    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        gravity = new Vector3(0, customGravity, 0);
        ThrowObject();
        Destroy(gameObject, 2.5f);
    }

    void Update(){
        
        ApplyCustomGravity();//Applys the custom gravity to the game object. Only if not being held.  
    }

    private void OnCollisionEnter(Collision collision){
        Debug.Log("I hit " + collision.transform.name);
        pillowHit.Invoke();
        energy = collision.gameObject.GetComponent<Energy>();
        mood = collision.gameObject.GetComponent<Mood>();
        if(energy != null){
            energy.TurnOff();
            mood.TurnOff();
        }
        Destroy(gameObject);
    }

    public void ThrowObject(){        
        rb.AddForce(transform.forward.normalized * throwSpeed, ForceMode.Impulse);
    }

    public void ApplyCustomGravity()
    {
        rb.AddForce(gravity, ForceMode.Acceleration);//Adds the artificial gravity to the items.
    }
}
