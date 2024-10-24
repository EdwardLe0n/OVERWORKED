using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;


/* Energy is the lifeblood of a human. Also affects task speed.
 * Energized humans complete tasks faster.
 * Neutral is baseline task completion.
 * Tired humans complete tasks slower.
 * Energy also slowly drains naturally over time.
 * If energy ever gets to 0, the human dies.
 */

public class Energy : MonoBehaviour
{
    [Header("Energy value")]
    [Tooltip("The maximum value for the energy bar.")]
    public float maxEnergy;

    [Header("Thresholds")]

    [Tooltip("% Threshold for a human to be energized while above.")]
    [Range(0,1)]
    public float energizedThreshold;

    [Tooltip("% Threshold for a human to be tired while below.")]
    [Range(0,1)]
    public float tiredThreshold;

    [Tooltip("% Time spent asleep with pillow gun")]
    public float sleepTime;

    // 0 <= energy <= maxEnergy
    private float energy;

    public bool IsEnergized{
        get { return energy/maxEnergy >= energizedThreshold; }
    }

    public bool IsTired{
        get { return energy/maxEnergy <= tiredThreshold; }
    }

    public bool IsDead{
        get { return energy <= 0; }
    }

    public float PercentEnergy{
        get { return energy / maxEnergy; }
    }

    void Awake(){
        energy = maxEnergy;
    }

    public float GetEnergy(){
        return energy;
    }

    /* given a delta value, changes energy accordingly
     * returns delta if successful, 0 if not
     */
    public float ChangeEnergy(float delta){
        // if the human is dead, don't change energy.
        if(IsDead){
            return 0;
        }
        
        energy += delta;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
        return delta;
    }

    public void TurnOff(){
        EnergyHandler energyHandler = GetComponent<EnergyHandler>();
        if (energyHandler != null){
            energyHandler.enabled = false;
            StartCoroutine(Asleep());
        }
    }

    public IEnumerator Asleep(){
        yield return new WaitForSeconds(sleepTime);
        TurnOn();
    }

    public void TurnOn(){
        EnergyHandler energyHandler = GetComponent<EnergyHandler>();
        if (energyHandler != null){
            energyHandler.enabled = true;
            energy = maxEnergy;
        }
    }
}
