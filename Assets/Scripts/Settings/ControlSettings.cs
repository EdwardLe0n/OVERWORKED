using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
    public List<GameObject> rebindButtonsList;
    public List<Button> otherButtons;

    public GameObject waitingText;

    public InputActionAsset inputActions;
    private InputActionMap playerControlsMap;
    
    // player 1 actions
    private InputAction moveActionP1;
    private InputAction pickupActionP1;
    private InputAction sprintActionP1; 

    // player 2 actions
    private InputAction moveActionP2;
    private InputAction pickupActionP2;
    private InputAction sprintActionP2; 

    private List<int> actionIndexes;
    private List<string> defaultKeys; // TODO: add to save data

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    void Start()
    {
        actionIndexes = new List<int>(new int[12]); // initialize list (TODO: change size if adding more keys)
        defaultKeys = new List<string>(new string[12]); // " " 

        playerControlsMap = inputActions.FindActionMap("Gameplay");
        
        moveActionP1 = playerControlsMap.FindAction("Move");
        pickupActionP1 = playerControlsMap.FindAction("PickUp1");
        sprintActionP1 = playerControlsMap.FindAction("Sprint1");

        moveActionP2 = playerControlsMap.FindAction("Move2");
        pickupActionP2 = playerControlsMap.FindAction("PickUp2");
        sprintActionP2 = playerControlsMap.FindAction("Sprint2");

        SaveDefaultBindings();
        UpdateKeyTextUI();

        // get action indexes for p1 movement
        for(int i = 0; i < 4; i++) {
            actionIndexes[i] = i + 2;
        }
        //get action indexes for p2 movement
        for(int i = 6; i < 10; i++) {
            actionIndexes[i] = i - 4;
        }
    }

    void SaveDefaultBindings()
    {
        // save default keys for p1
        for(int i = 0; i < 4; i++) {
            defaultKeys[i] = moveActionP1.bindings[i+2].effectivePath;
        }
        defaultKeys[4] = pickupActionP1.bindings[0].effectivePath;  
        defaultKeys[5] = sprintActionP1.bindings[0].effectivePath;

        // save default keys for p2
        for(int i = 6; i < 10; i++) {
            defaultKeys[i] = moveActionP2.bindings[i-4].effectivePath;
        }
        defaultKeys[10] = pickupActionP2.bindings[0].effectivePath;  
        defaultKeys[11] = sprintActionP2.bindings[0].effectivePath;
    }

    public void StartRebinding(GameObject clickedButton)
    {
        int pressedIndex = rebindButtonsList.IndexOf(clickedButton);
        for(int i = 0; i < rebindButtonsList.Count; i++) {
            if(i == pressedIndex) {
                rebindButtonsList[i].SetActive(false);
            }
            else {
                // disable other buttons while changing keybind
                rebindButtonsList[i].GetComponent<Button>().interactable = false;
            }
        }

        // also disable other buttons on menu
        foreach(Button button in otherButtons) {
            button.interactable = false;
        }

        // display waiting message
        waitingText.SetActive(true);

        // need to include action index
        int actionIndex = actionIndexes[pressedIndex];
        if(pressedIndex < 4) {
            // rebind movement
            rebindingOperation = moveActionP1.PerformInteractiveRebinding(actionIndex)
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(pressedIndex))
                .Start();
        } 
        else if(pressedIndex == 4) {
            // rebind interact
            rebindingOperation = pickupActionP1.PerformInteractiveRebinding(actionIndex)
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(pressedIndex))
                .Start();
        }
        else if(pressedIndex == 5) {
            // rebind sprint
            rebindingOperation = sprintActionP1.PerformInteractiveRebinding(actionIndex)
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(pressedIndex))
                .Start();
        }
        else if(pressedIndex >= 6 && pressedIndex < 10) {
            // rebind movement 2
            rebindingOperation = moveActionP2.PerformInteractiveRebinding(actionIndex)
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(pressedIndex))
                .Start();
        } 
        else if(pressedIndex == 10) {
            // rebind interact 2
            rebindingOperation = pickupActionP2.PerformInteractiveRebinding(actionIndex)
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(pressedIndex))
                .Start();
        }
        else if(pressedIndex == 11) {
            // rebind sprint 2
            rebindingOperation = sprintActionP2.PerformInteractiveRebinding(actionIndex)
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(pressedIndex))
                .Start();
        }
    }

    public void RebindComplete(int buttonIndex)
    {
        // update button text with new key (TODO: check for p1/p2 based on index)
        string humanReadableKey = GetKeyText(buttonIndex);
        rebindButtonsList[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text = humanReadableKey;
        rebindButtonsList[buttonIndex].SetActive(true);
        
        // re-enable all buttons
        for(int i = 0; i < rebindButtonsList.Count; i++) {
            rebindButtonsList[i].GetComponent<Button>().interactable = true;
        }
        // same for other buttons
        foreach(Button button in otherButtons) {
            button.interactable = true;
        }

        // hide waiting message
        waitingText.SetActive(false);

        rebindingOperation.Dispose(); // need to dispose of the memory
    }

    string GetKeyText(int buttonIndex)
    {
        string humanReadableKey = "";

        if(buttonIndex < 4) {
            // p1 movement
            int actionIndex = buttonIndex + 2;
            humanReadableKey = InputControlPath.ToHumanReadableString(moveActionP1.bindings[actionIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);            
        }
        else if(buttonIndex == 4) {
            // p1 interact
            humanReadableKey = InputControlPath.ToHumanReadableString(pickupActionP1.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        else if(buttonIndex == 5) {
            // p1 sprint
            humanReadableKey = InputControlPath.ToHumanReadableString(sprintActionP1.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        else if(buttonIndex >= 6 && buttonIndex < 10) {
            // p2 movement
            int actionIndex = buttonIndex - 4;
            humanReadableKey = InputControlPath.ToHumanReadableString(moveActionP2.bindings[actionIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        else if(buttonIndex == 10) {
            // p2 interact
            humanReadableKey = InputControlPath.ToHumanReadableString(pickupActionP2.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        else if(buttonIndex == 11) {
            // p2 sprint
            humanReadableKey = InputControlPath.ToHumanReadableString(sprintActionP2.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }

        return humanReadableKey;
    }

    public void ResetKeysToDefault(int playerNum)
    {
        if(playerNum == 1) {
            // reset for p1
            for(int i = 0; i < 4; i++) {
                moveActionP1.ApplyBindingOverride(i+2, defaultKeys[i]);
            }
            pickupActionP1.ApplyBindingOverride(0, defaultKeys[4]);
            sprintActionP1.ApplyBindingOverride(0, defaultKeys[5]);
        }
        else {
            // reset for p2
            for(int i = 6; i < 10; i++) {
                moveActionP2.ApplyBindingOverride(i-4, defaultKeys[i]);
            }
            pickupActionP2.ApplyBindingOverride(0, defaultKeys[10]);
            sprintActionP2.ApplyBindingOverride(0, defaultKeys[11]);
        }

        UpdateKeyTextUI();

        /* (old method --> reset all keys for both players)
        inputActions.RemoveAllBindingOverrides();
        for(int i = 0; i < rebindButtonsList.Count; i++) {
            int a = actionIndexes[i];
            string humanReadableKey = GetKeyText(i, a);
            rebindButtonsList[i].GetComponentInChildren<TextMeshProUGUI>().text = humanReadableKey;
        }
        */
    }

    void UpdateKeyTextUI()
    {
        for(int i = 0; i < 12; i++) {
            string humanReadableKey = GetKeyText(i);
            rebindButtonsList[i].GetComponentInChildren<TextMeshProUGUI>().text = humanReadableKey;
        }
    }    
}