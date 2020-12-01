using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;//for using Events

public class CharacterCreationMenu : MonoBehaviour
{
    public GameObject Character;//to store the created character
    //insert the gameObjects that are responsible for switching the character parts, not the actual character parts
    public List<CharacterChanger> changerOfCharacerPart = new List<CharacterChanger>();
    public static event Action CharacterSubmited;
    //--------------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        Debug.Log("CharacterCreationMenu: RandomizeCharacter();");
        SetDefaultCharacter();
    }
    //---------------------------------------------------------------------------------
    // SET DEFAULT LOOK FOR CHARACTER IN CHAR. CREATION MENU
    //---------------------------------------------------------------------------------
    public void SetDefaultCharacter()//before changing it based on user input
    {
        foreach (CharacterChanger changer in changerOfCharacerPart)//go through the list of changers and call their Randomize method
        {
            changer.switchArtSets();//change the art set of the changer
            changer.SetToFirstSprite();
            Debug.Log("CharacterCreationMenu: changer.Randomize();");
        }
    }

    public void SubmitCharacter()
    {
        Debug.Log("CharacterCreationMenu: Character submited >> GameManager");
        CharacterSubmited();//for gameManager that character is done + AudioManager to play confirm sound
    }
}