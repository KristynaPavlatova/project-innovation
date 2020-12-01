using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//for events
using UnityEngine.UI;


public class TutorialClickHandlers : MonoBehaviour
{
    //events for GameManager
    private int CurrentPage = 0;
    public GameObject[] tutorialScreens = new GameObject[3];
    public static event Action toggleInteraction; //toggles the controls of the player
    //--------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// browse to the next page in the tutorial
    /// </summary>
    public void NextButton()
    {
        if (CurrentPage < 2)
        {
            tutorialScreens[CurrentPage].SetActive(false);
            CurrentPage++;
            tutorialScreens[CurrentPage].SetActive(true);
        }
        else
        {
            gameObject.SetActive(false) ;
            toggleInteraction();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
