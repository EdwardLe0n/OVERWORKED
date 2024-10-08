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
        // check if the faxPlacingArea is empty
        if(faxPlacingArea == null){
            // don't do anything and quit
            return;
        }

        // reference item in the placing area
        GameObject placedItem = faxPlacingArea.currentPick;

        // check if item placed matched tag
        if(!placedItem.CompareTag(matchTag))
        {
            // don't do anything and quit
            return;
        }

        // empty placing area variables
        faxPlacingArea.currentPick = null;
        faxPlacingArea.hasItem = false;

        Destroy(placedItem);
    }
}
