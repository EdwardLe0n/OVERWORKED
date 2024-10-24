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
    
    [Tooltip("Color gradient for the energy bar")]
    public Gradient energyBarColors;

    void Update(){
        img.fillAmount = energy.PercentEnergy;
        img.color = energyBarColors.Evaluate(energy.PercentEnergy);

        // handles the look towards for the entire transform
        Vector3 lookPos = new Vector3(transform.position.x, transform.position.y+1, transform.position.z-1);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.position += new Vector3(0, 2.5f, -2);
        transform.LookAt(lookPos);
    }
}
