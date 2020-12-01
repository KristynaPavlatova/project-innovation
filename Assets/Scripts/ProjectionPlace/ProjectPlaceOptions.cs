using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectPlaceOptions : MonoBehaviour
{
    public GameObject targetSelector;//what part of character should look for?
    
    private int _option;//my option number is the same as the currentOption from the target list
    private bool _isTheSpriteSet = false;

    public Sprite chosenSprite;//to get the concrete sprite that we chose in the targetSlector from it's list with sprites (just get the one sprite)
    //--------------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;//don't display anything before creating your character
        GameManager.DisplayProjectionEvent += displayCorrectSprite;//wait for the command to display the correct Sprites -> character
    }
    private void OnDestroy()
    {
        GameManager.DisplayProjectionEvent -= displayCorrectSprite;
    }

    //----------------------------------------------------------------
    // DISPLAY CORRECT SPRITE
    //----------------------------------------------------------------
    private void displayCorrectSprite()
    {
        if (!_isTheSpriteSet)
        {
            //what is the current options:
            _option = targetSelector.GetComponent<CharacterChanger>().currentOption;//********************** get the sprite or the option from the target
            //get the sprite from the target list of the correct number from the list:
            chosenSprite = targetSelector.GetComponent<CharacterChanger>().options[_option];

            //this sprite renderer is going to render the chosenSprite:
            this.GetComponent<SpriteRenderer>().sprite = chosenSprite;

            _isTheSpriteSet = true;//there is the correct sprite "projected"
        }
    }
}
