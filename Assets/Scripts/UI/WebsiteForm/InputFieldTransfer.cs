using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//for using Events

public class InputFieldTransfer : MonoBehaviour
{
    [HideInInspector]
    public string Name;
    [SerializeField]
    public GameObject inputFieldName;
    public GameObject textDisplayName;
    
    [HideInInspector]
    public string Age;
    [SerializeField]
    public GameObject inputFieldAge;
    public GameObject textDisplayAge;

    [HideInInspector]
    public string Race;
    [SerializeField]
    public GameObject inputFieldRace;
    public GameObject textDisplayRace;

    [HideInInspector]
    public string Gender;
    [SerializeField]
    public GameObject inputFieldGender;
    public GameObject textDisplayGender;

    public static event Action ContinuePressed;

    public void Continue()
    {
        StoreFieldsInfo();
        //TO DO:
        //store the name or send it somewhere so then we can project it >> for all cathegories !!! TUTORIAL different way
        //then hide this website part >> event to GameManager
        ContinuePressed();//EVENT
    }
    private void StoreFieldsInfo()
    {
        StoreName();
        StoreAge();
        StoreRace();
        StoreGender();
    }
    public void StoreName()
    {
        Name = inputFieldName.GetComponent<Text>().text;//what ever is written in the inputFieldName >> put it into the Name variable
        textDisplayName.GetComponent<Text>().text = Name;//put the string Name into the text that gets displayed
        Debug.Log("StoreName");
    }
    public void StoreAge()
    {
        Age = inputFieldAge.GetComponent<Text>().text;//what ever is written in the inputFieldName >> put it into the Name variable
        textDisplayAge.GetComponent<Text>().text = Age;//put the string Name into the text that gets displayed
        Debug.Log("StoreAge");
    }
    public void StoreRace()
    {
        Race = inputFieldRace.GetComponent<Text>().text;//what ever is written in the inputFieldName >> put it into the Name variable
        textDisplayRace.GetComponent<Text>().text = Race;//put the string Name into the text that gets displayed
        Debug.Log("StoreRace");
    }
    public void StoreGender()
    {
        Gender = inputFieldGender.GetComponent<Text>().text;//what ever is written in the inputFieldName >> put it into the Name variable
        textDisplayGender.GetComponent<Text>().text = Gender;//put the string Name into the text that gets displayed
        Debug.Log("StoreGender");
    }
}
