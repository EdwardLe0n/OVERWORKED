using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activation : MonoBehaviour
{
    public abstract void Activate();
    public abstract bool CanActivate();
}
