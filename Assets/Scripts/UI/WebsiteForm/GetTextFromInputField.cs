using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add the TextMesh Pro namespace to access the various functions.

public class GetTextFromInputField : MonoBehaviour
{
    //Get string from GameManager, insert it to this text >> display on projection

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.DisplayProjectionEvent += changeNameText;
    }
    private void OnDestroy()
    {
        GameManager.DisplayProjectionEvent -= changeNameText;
    }

    private void changeNameText()
    {
        this.GetComponent<TextMeshPro>().text = gameManager.userName;
    }
}
