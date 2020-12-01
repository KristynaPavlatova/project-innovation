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
    //--------------------------------------------------------------------------------------------------------------------------------

    public void Continue()//continue to CharacterCreation part
    {
        StoreFieldsInfo();
        ContinuePressed();//EVENT
        Debug.Log("InputFieldTransfer: ContinuePressed event");
    }
    private void StoreFieldsInfo()
    {
        StoreName();
        StoreAge();
    }
    public void StoreName()
    {
        Name = inputFieldName.GetComponent<Text>().text;//what ever is written in the inputFieldName >> put it into the Name variable
        textDisplayName.GetComponent<Text>().text = Name;//put the string Name into the text that gets displayed
        //Debug.Log("StoreName");
    }
    public void StoreAge()
    {
        Age = inputFieldAge.GetComponent<Text>().text;//what ever is written in the inputFieldName >> put it into the Name variable
        textDisplayAge.GetComponent<Text>().text = Age;//put the string Name into the text that gets displayed
        //Debug.Log("StoreAge");
    }
}
