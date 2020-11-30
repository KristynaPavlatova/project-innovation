using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectPlaceOptions : MonoBehaviour
{
    public GameObject targetSelector;//what part of character should look for?
    public List<Sprite> options;
    
    private int _option;//!! has to be the exact same as the Character's lists for each part
    private bool _isSpriteSet = false;

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = options[options.Count - 1];//last empty option
        GameManager.DisplayProjectionEvent += displayCorrectSprite;
    }

    private void OnDestroy()
    {
        GameManager.DisplayProjectionEvent -= displayCorrectSprite;
    }

    private void displayCorrectSprite()
    {
        if (!_isSpriteSet)
        {
            _option = targetSelector.GetComponent<CharacterChanger>().currentOption;
            this.GetComponent<SpriteRenderer>().sprite = options[_option];

            _isSpriteSet = true;
        }
        
    }
}
