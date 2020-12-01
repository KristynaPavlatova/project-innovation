using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeciesButtons : MonoBehaviour
{
    //events for GameManager
    public static event Action HumanSelected;
    public static event Action AlienSelected;
    public static event Action CyborgSelected;

    public static event Action MaleSelected;
    public static event Action FemaleSelected;
    //--------------------------------------------------------------------------------------------------------------------------------

    /*Fire events so GameManager can change its species & gender variables
      So, the CharacterChanger can chose the correct art set*/
    public void HumanButton()
    {
        HumanSelected();
    }
    public void AlienButton()
    {
        AlienSelected();
    }
    public void CyborgButton()
    {
        CyborgSelected();
    }

    public void MaleButton()
    {
        MaleSelected();
    }
    public void FemaleButton()
    {
        FemaleSelected();
    }
}
