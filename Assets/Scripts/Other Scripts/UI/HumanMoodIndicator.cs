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

    [Tooltip("Image to display while content. Do not leave empty!")]
    public Sprite content;

    [Tooltip("Image to display while stressed. Do not leave empty!")]
    public Sprite stressed;

    [Tooltip("Image to display while dying. Do not leave empty!")]
    public Sprite dying;

    [Tooltip("Image to display while on coffee. Do not leave empty!")]
    public Sprite wired;

    [Tooltip("Image to display while catted. Do not leave empty!")]
    public Sprite cat;

    [Header("Unity Set Up")]
    [Tooltip("Image of the mood holder. Don't change.")]
    public Image moodHolder;

    [Tooltip("Actual image component to change. Don't change.")]
    public Image image;

    [Tooltip("Human's state component. Do not change.")]
    public HumanStates states;

    private bool isVisible;

    public void SetVisible(bool v){
        isVisible = v;
    }

    void Update(){
        if(states.isCoffeed){
            ChangeSprite(wired);
        }
        else if(states.isCatted){
            ChangeSprite(cat);
        }
        else if(states.IsHappy){
            ChangeSprite(happy);
        }
        else if(states.IsDying){
            ChangeSprite(dying);
        }
        else if(states.IsStressed){
            ChangeSprite(stressed);
        }else{
            ChangeSprite(content);
        }

        moodHolder.enabled = isVisible;
        image.enabled = isVisible;

        isVisible = false;
    }

    private void ChangeSprite(Sprite sprite){
        moodHolder.enabled = true;
        image.sprite = sprite;
        image.enabled = true;
    }
}