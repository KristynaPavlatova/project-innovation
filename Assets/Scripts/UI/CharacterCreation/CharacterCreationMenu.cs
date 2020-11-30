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

    private void Start()
    {
        RandomizeCharacter();
    }
    public void RandomizeCharacter()
    {
        foreach (CharacterChanger changer in changerOfCharacerPart)//go through the list of changers and call their Randomize method
        {
            changer.Randomize();
        }
    }

    public void SubmitCharacter()
    {
        //PrefabUtility.SaveAsPrefabAsset(Character, "Assets/Prefabs/Character.prefab");
        //Debug.Log("Character prefab changed based on current costumization.");
        Debug.Log("Character submitte >> GameManager");
        CharacterSubmited();//for gameManager
    }
}