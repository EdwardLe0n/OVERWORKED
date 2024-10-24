using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class FaxMachineActivation : Activation
{
    [Tooltip("PlacingArea script of the fax machine")]
    public PlacingArea faxPlacingArea;

    [Tooltip("Object tag to match upon activation")]
    public string matchTag;

    public override void Activate()
    {

        // reference item in the placing area
        GameObject placedItem = faxPlacingArea.currentPick;

        // empty placing area variables
        faxPlacingArea.currentPick = null;
        faxPlacingArea.hasItem = false;

        Destroy(placedItem);
    }

    public override bool CanActivate()
    {

        // Error handling to make sure that a dead human exists on th fax machine, and can there fore be destroyed
        if (faxPlacingArea != null)
        {

            GameObject placedItem = faxPlacingArea.currentPick;

            if (placedItem != null)
            {

                if (placedItem.CompareTag(matchTag))
                {
                    return true;
                }

            }
        }


        return false;
    }
}
