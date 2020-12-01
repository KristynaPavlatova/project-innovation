using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChanger : MonoBehaviour
{
    public bool isDebugOn = true;//should write out debug messages?
    [Header("Characte (part) to change")]
    public Image character;//what part of our character are we changing >> sprite renderer

    [HideInInspector]
    public List<Sprite> options = new List<Sprite>();//List of options to change between them

    [Header("Sprites for Human:")]
    public List<Sprite> HumanOptionsM = new List<Sprite>();
    public List<Sprite> HumanOptionsF = new List<Sprite>();
    [Header("Sprites for Alien:")]
    public List<Sprite> AlienOptions = new List<Sprite>();
    [Header("Sprites for Cyborg:")]
    public List<Sprite> CyborgOptionsM = new List<Sprite>();
    public List<Sprite> CyborgOptionsF = new List<Sprite>();
    //switching between art sets
    public GameManager gameManager;//for accessing variables

    [HideInInspector]
    public int currentOption = 0;//public to access it from projection place character parts
    //--------------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        //GameManager.CheckSpeciesGenderSettings += switchArtSets;
        InputFieldTransfer.ContinuePressed += switchArtSets;
    }
    private void OnDestroy()
    {
        //GameManager.CheckSpeciesGenderSettings -= switchArtSets;
        InputFieldTransfer.ContinuePressed += switchArtSets;
    }

    //---------------------------------------------------------------------------------------------------
    // BUTTONS methods
    //---------------------------------------------------------------------------------------------------
    public void NextOption()
    {
        currentOption++;
        if (currentOption >= options.Count)
        {
            currentOption = 0;//go back to the start of the list
        }
        character.sprite = options[currentOption];//change the characterPart sprite to a different sprite from the options list
    }
    public void PreviousOption()
    {
        currentOption--;
        if (currentOption < 0)
        {
            currentOption = (options.Count - 1);//go back to the end of the list
        }
        character.sprite = options[currentOption];//change the characterPart sprite to a different sprite from the options list
    }
    //---------------------------------------------------------------------------------
    // SET TO FIRST SPRITE
    //---------------------------------------------------------------------------------
    public void SetToFirstSprite()
    {
        currentOption = (options.Count - 1);//the default (first option from the art set)
        character.sprite = options[currentOption];
    }

    //---------------------------------------------------------------------------------
    // SWITCH ART SETS
    //---------------------------------------------------------------------------------
    public void switchArtSets()
    { 
        if (isDebugOn) { Debug.Log("Switching Art sets."); }
        switch (gameManager.species)
        {
            //species:
            case 0://human
                //gender:
                if (gameManager.gender == 0)//male
                {
                    options = HumanOptionsM;//all the aptions we take the art sets from is the human male options list
                    if(isDebugOn){ Debug.Log("Art set = HM"); }
                    
                }
                else//female
                {
                    options = HumanOptionsF;
                    if (isDebugOn){ Debug.Log("Art set = HF"); }                        
                }
                break;
            case 1://alien
                options = AlienOptions;
                Debug.Log("Art set = A");
                break;
            case 2://cyborg
                if (gameManager.gender == 0)//male
                {
                    options = CyborgOptionsM;
                    if (isDebugOn) { Debug.Log("Art set = CM"); }
                }
                else//female
                {
                    options = CyborgOptionsF;
                    if (isDebugOn) { Debug.Log("Art set = CF"); }
                }
                break;
        }
    }
}
