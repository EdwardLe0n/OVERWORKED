using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Image = UnityEngine.UI.Image;


public class HumanMoodIndicator : MonoBehaviour
{
    [Header("Sprites")]
    [Tooltip("Image to display while happy. Do not leave empty!")]
    public Sprite happy;

    [Tooltip("Image to display while stressed. Do not leave empty!")]
    public Sprite stressed;

    [Tooltip("Image to display while dying. Do not leave empty!")]
    public Sprite dying;

    [Header("Unity Set Up")]
    [Tooltip("Image of the mood holder. Don't change.")]
    public Image moodHolder;

    [Tooltip("Actual image component to change. Don't change.")]
    public Image image;

    [Tooltip("Human's state component. Do not change.")]
    public HumanStates states;

    void Update(){
        if(states.isCoffeed){
            ChangeSprite(stressed);
            return;
        }

        if(states.isCatted){
            ChangeSprite(happy);
            return;
        }

        if(states.IsHappy){
            ChangeSprite(happy);
            return;
        }

        if(states.IsDying){
            ChangeSprite(dying);
            return;
        }

        if(states.IsStressed){
            ChangeSprite(stressed);
            return;
        }

        // only ever gets here if in the neutral state
        moodHolder.enabled = false;
        image.enabled = false;
    }

    private void ChangeSprite(Sprite sprite){
        moodHolder.enabled = true;
        image.sprite = sprite;
        image.enabled = true;
    }
}