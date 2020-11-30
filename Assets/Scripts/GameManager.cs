using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//for events
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isDebugOn = true;//should write out debug messages?

    public GameObject ScanQRCodeText;
    public GameObject WebsiteForm;
    public GameObject Player;
    public GameObject CharacterCreationPart;
    public GameObject ProjectionPlace;
    public GameObject QuitText;
    public GameObject hintText;
    
    public Text nameInputFieldText;//Getting info from InputField's text

    [HideInInspector]
    public string userName;

    private bool _scanCodeUIVisible = false;
    private bool _websiteFormVisible = false;
    private bool _characterCreatorVisible = false;
    private bool _characterSubmitted = false;
    private bool _playerInteractionEnabled = true;
    private bool _quitAppIsPossible = false;

    public static event Action DisplayProjectionEvent;

    //for species, gender selection in website from
    private int _species = 0;//default: human
    private int _gender = 0;//default: male


    // Start is called before the first frame update
    void Start()
    {
        WebsiteForm.SetActive(_websiteFormVisible);//make sure website image is not visible until we want

        //-------------------------------------
        //WEBSITE FORM:
        //species
        SpeciesButtons.HumanSelected += humanSelected;
        SpeciesButtons.AlienSelected += alienSelected;
        SpeciesButtons.CyborgSelected += cyborgSelected;
        //gender
        SpeciesButtons.MaleSelected += maleSelected;
        SpeciesButtons.FemaleSelected += femaleSelected;

        DistanceSphere.PlayerTriggerEnter += scanCodeUITrigger;//Show website form part
        DistanceSphere.PlayerTriggerExit += scanCodeUITrigger;

        //-------------------------------------
        //CHARACTER CREATOR:
        //hide website from, show character creation part
        InputFieldTransfer.ContinuePressed += characterCreationShow;
        //once character is created and submitted
        CharacterCreationMenu.CharacterSubmited += characterDone;
        //preparation for walkthrough
        
    }
    private void OnDestroy()
    {
        //-------------------------------------
        //QR CODE:
        DistanceSphere.PlayerTriggerEnter -= scanCodeUITrigger;
        DistanceSphere.PlayerTriggerExit -= scanCodeUITrigger;

        //-------------------------------------
        //WEBSITE FORM:
        InputFieldTransfer.ContinuePressed -= characterCreationShow;

        //-------------------------------------
        //CHARACTER CREATOR:
        CharacterCreationMenu.CharacterSubmited -= characterDone;
    }

    // Update is called once per frame
    void Update()
    {
        displayScanCodeUI();
        showWebsiteForm();
        pressToQuitApp();
    }

    //---------------------------------------------------------------------------------------
    // WEBSITE FORM part 2
    //---------------------------------------------------------------------------------------
    private void showWebsiteForm()
    {
        if (_scanCodeUIVisible && Input.GetKeyDown(KeyCode.Q))//to open website (form part 1)
        {
            //Display the website UI where you can start creating your character
            _websiteFormVisible = true;
            WebsiteForm.SetActive(_websiteFormVisible);
            hintText.SetActive(true);//show the hint text

            _scanCodeUIVisible = false;//hide the "press button" suggestion

            interactionSetup();//disable player movement, lock mouse
        }//>> part 2 by event
    }

    //---------------------------------------------------------------------------------------
    // SCAN QR CODE UI part 1
    //---------------------------------------------------------------------------------------
    private void scanCodeUITrigger()
    {
        //tell UI to show/hide "scan code button UI"
        if (!_scanCodeUIVisible && !_websiteFormVisible && !_characterSubmitted)//if this "button suggestion" not visible yet && the website image not visible either
        {
            _scanCodeUIVisible = true;
        }
        else
        {
            _scanCodeUIVisible = false;
        }
    }
    private void displayScanCodeUI()
    {
        ScanQRCodeText.SetActive(_scanCodeUIVisible);
    }

    //---------------------------------------------------------------------------------------
    // CHARACTER CREATOR part 3
    //---------------------------------------------------------------------------------------
    private void characterCreationShow()
    {
        getInputFieldInfo();//get info from the website form (name)

        _websiteFormVisible = false;
        WebsiteForm.SetActive(_websiteFormVisible);//hide the website form (part 1)
        hintText.SetActive(false);

        if (!_characterCreatorVisible)
        {
            _characterCreatorVisible = true;
            CharacterCreationPart.SetActive(_characterCreatorVisible);
            //TO DO: SEND EVENT FOR CharacterChanger TO CHECK THE SPECIES+GENDER TO CHOSE CORRECT ART SET !!!**********************
        }//otherwise, it should be always not active at first >> manually disable in inspetor
    }
    private void getInputFieldInfo()
    {
        userName = nameInputFieldText.text;
    }
    /// <summary>
    /// Hide characterCreator. Change playerInteractionSetup to moving again. Show the created character.
    /// Fire event for ProjectionPlace to instantiate the prefab with the correct sprites.
    /// </summary>
    private void characterDone()
    {
        _characterCreatorVisible = false;
        CharacterCreationPart.SetActive(_characterCreatorVisible);
        interactionSetup();//unlock the player and the cursor

        _characterSubmitted = true;
        ProjectionPlace.SetActive(true);//make the character visible

        DisplayProjectionEvent();//fire event so ProjectionPlace instantiates Projection prefab with the correct sprites >> how user created it

        enableQuitApplication();
    }

    //---------------------------------------------------------------------------------------
    // AFTER CHARACTER CREATED
    //---------------------------------------------------------------------------------------
    private void enableQuitApplication()
    {
        //after the character is projected let the user quit the application
        QuitText.SetActive(true);
        _quitAppIsPossible = true;
    }
    private void pressToQuitApp()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _quitAppIsPossible)
        {
            Application.Quit();
        }
    }
    //---------------------------------------------------------------------------------------
    // DISABLE PLAYER
    //---------------------------------------------------------------------------------------
    private void interactionSetup()
    {
        if (_playerInteractionEnabled)
        {
            //disable player movement
            Player.GetComponentInChildren<PlayerController>().canLookAround = false;
            //Player.GetComponentInChildren<PlayerController>().canJump = false;//Not able to jump by default
            Player.GetComponentInChildren<PlayerController>().canWalk = false;
            
            Cursor.lockState = CursorLockMode.Confined;//lock the mouse cursor >> navigation on within the Game window
            _playerInteractionEnabled = false;
            if (isDebugOn) { Debug.Log("_playerInteractionEnabled = " + _playerInteractionEnabled); }
        }
        else
        {
            //enable player movement
            Player.GetComponentInChildren<PlayerController>().canLookAround = true;
            //Player.GetComponentInChildren<PlayerController>().canJump = true;//Not able to jump by default
            Player.GetComponentInChildren<PlayerController>().canWalk = true;
            
            Cursor.lockState = CursorLockMode.Locked;//unlock the mouse cursor
            _playerInteractionEnabled = true;
            if (isDebugOn) { Debug.Log("_playerInteractionEnabled = " + _playerInteractionEnabled); }
        }
    }

    //---------------------------------------------------------------------------------------
    // WEBSITE FORM SPECIES & GENDER BUTTONS
    //---------------------------------------------------------------------------------------
    private void humanSelected()
    {
        _species = 0;
        if (isDebugOn) { Debug.Log("Species = " + _species); }
    }
    private void alienSelected()
    {
        _species = 1;
        if (isDebugOn) { Debug.Log("Species = " + _species); }
    }
    private void cyborgSelected()
    {
        _species = 2;
        if (isDebugOn) { Debug.Log("Species = " + _species); }
    }
    private void maleSelected()
    {
        _gender = 0;
        if (isDebugOn) { Debug.Log("Gender = " + _gender); }
    }
    private void femaleSelected()
    {
        _gender = 1;
        if (isDebugOn) { Debug.Log("Gender = " + _gender); }
    }
}
