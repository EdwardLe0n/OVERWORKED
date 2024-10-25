using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

using Image = UnityEngine.UI.Image;

public class HumanEnergyBar : MonoBehaviour
{
    [Tooltip("Transform of the indicator parent object.")]
    public Transform container;

    [Tooltip("The Energy component of the human. Do not leave empty!")]
    public Energy energy;

    [Tooltip("The bar to hold energy. Do not leave empty!")]
    public Image holder;

    [Tooltip("The bar to signify energy. Do not leave empty!")]
    public Image img;
    
    [Tooltip("Color gradient for the energy bar")]
    public Gradient energyBarColors;

    private bool isVisible;

    public void SetVisible(bool v){
        isVisible = v;
    }

    void Update(){
        img.fillAmount = energy.PercentEnergy;
        img.color = energyBarColors.Evaluate(energy.PercentEnergy);

        // handles the look towards for the entire transform
        container.localPosition = new Vector3(0, 0, 0);
        container.position += new Vector3(0, 2.5f, -2);

        Vector3 lookPos = new Vector3(container.position.x, container.position.y+1, container.position.z-1);
        container.LookAt(lookPos);

        holder.enabled = isVisible;
        img.enabled = isVisible;

        isVisible = false;
    }
}
