using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

using Image = UnityEngine.UI.Image;

public class HumanEnergyBar : MonoBehaviour
{
    [Tooltip("The Energy component of the human. Do not leave empty!")]
    public Energy energy;

    [Tooltip("The bar to signify energy. Do not leave empty!")]
    public Image img;

    void Update(){
        img.fillAmount = energy.PercentEnergy;

        Vector3 lookPos = new Vector3(transform.position.x, transform.position.y+1, transform.position.z-1);
        transform.LookAt(lookPos);
    }
}
