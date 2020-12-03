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
    public GameObject TutorialWindow;

    public Text nameInputFieldText;//Getting info from InputField's text

    [HideInInspector]
    public string userName;

    private bool _scanCodeUIVisible = false;
    private bool _websiteFormVisible = false;
    private bool _characterCreatorVisible = false;
    private bool _characterSubmitted = false;
    private bool _playerInteractionEnabled = false;//true
    private bool _tutoriaWindowVisible = true;
    private bool _quitAppIsPossible = false;

    public static event Action DisplayProjectionEvent;
    public static event Action CheckSpeciesGenderSettings;//for CharacterChanger to check settings from WebsiteForm

    //for species, gender selection in website from
    public int species = 0;//default: human
    public int gender = 0;//default: male
    //--------------------------------------------------------------------------------------------------------------------------------

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
        InputFieldTransfer.ContinuePressed += showCharacterCreation;//hide website from, show character creation part
        CharacterCreationMenu.CharacterSubmited += characterDone;//once character is created and submitted
        TutorialClickHandlers.toggleInteraction += interactionSetup; //to toggle player availablity in totorial script
        Cursor.lockState = CursorLockMode.Confined;//to enable availability on the tutorial window in the start
        interactionSetup();

    }
    private void OnDestroy()
    {
        //-------------------------------------
        //QR CODE:
        DistanceSphere.PlayerTriggerEnter -= scanCodeUITrigger;
        DistanceSphere.PlayerTriggerExit -= scanCodeUITrigger;

        //-------------------------------------
        //WEBSITE FORM:
        InputFieldTransfer.ContinuePressed -= showCharacterCreation;

        //-------------------------------------
        //CHARACTER CREATOR:
        CharacterCreationMenu.CharacterSubmited -= characterDone;

        TutorialClickHandlers.toggleInteraction -= interactionSetup;

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
        if (_scanCodeUIVisible && Input.GetKeyDown(KeyCode.Q))//to open website
        {
            //Display the websiteForm UI where you can start creating your character (name, age, species, gender)
            _websiteFormVisible = true;
            WebsiteForm.SetActive(_websiteFormVisible);
            hintText.SetActive(true);//show the hint text

            _scanCodeUIVisible = false;//hide the "press button" suggestion
            if (isDebugOn) { Debug.Log("GameManager: showWebsiteForm"); }
            interactionSetup();//disable player movement, lock mouse
        }//>> part 2 by event
    }

    /// <summary>
    /// Toggles the tutorial 
    /// </summary>
    private void showTutorialScreen()
    {
        if (_tutoriaWindowVisible)//checks if you may show the tutorial (only at the start of running the application.
        {
            //Display the first tutorialscreen
            TutorialWindow.SetActive(_tutoriaWindowVisible);

            _scanCodeUIVisible = false;//hide the "press button" suggestion
            if (isDebugOn) { Debug.Log("GameManager: showWebsiteForm"); }
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
    private void showCharacterCreation()
    {
        getInputFieldInfo();//get info from the website form (name, age)

        _websiteFormVisible = false;
        WebsiteForm.SetActive(_websiteFormVisible);//hide the websiteForm
        hintText.SetActive(false);

        if (!_characterCreatorVisible)
        {
            _characterCreatorVisible = true;
            CharacterCreationPart.SetActive(_characterCreatorVisible);//make it visible
            if (isDebugOn) { Debug.Log("GameManager: _characterCreatorVisible = " + _characterCreatorVisible); }
        }//otherwise, it should be always not active at first >> manually disable in inspetor
    }
    private void startCheckingSettings()
    {
        CheckSpeciesGenderSettings();//for CharacterChanger to check settings from WebsiteForm
    }
    private void getInputFieldInfo()
    {
        userName = nameInputFieldText.text;
    }
    //---------------------------------------------------------------------------------------
    // ONCE CHARACTER DONE
    //---------------------------------------------------------------------------------------
    /* Hide characterCreator. Change playerInteractionSetup to moving again. Show the created character.
       Fire event for ProjectionPlace to instantiate the prefab with the correct sprites.*/
    private void characterDone()
    {
        _characterSubmitted = true;
        ProjectionPlace.SetActive(true);//make the character visible
        interactionSetup();//unlock the player and the cursor
        DisplayProjectionEvent();//fire event so ProjectionPlace instantiates Projection prefab with the correct sprites >> how user created it
        
        enableQuitApplication();

        _characterCreatorVisible = false;
        CharacterCreationPart.SetActive(_characterCreatorVisible);
        
        if (isDebugOn) { Debug.Log("GameManager: Character done. Hide CharacterCreationPart, ProjectionPlace active, DisplayProjectionEvent"); }
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
            if (isDebugOn) { Debug.Log("GameManager: _playerInteractionEnabled = " + _playerInteractionEnabled); }
        }
        else
        {
            //enable player movement
            Player.GetComponentInChildren<PlayerController>().canLookAround = true;
            //Player.GetComponentInChildren<PlayerController>().canJump = true;//Not able to jump by default
            Player.GetComponentInChildren<PlayerController>().canWalk = true;
            
            Cursor.lockState = CursorLockMode.Locked;//unlock the mouse cursor
            _playerInteractionEnabled = true;
            if (isDebugOn) { Debug.Log("GameManager: _playerInteractionEnabled = " + _playerInteractionEnabled); }
        }
    }

    //---------------------------------------------------------------------------------------
    // WEBSITE FORM SPECIES & GENDER BUTTONS
    //---------------------------------------------------------------------------------------
    private void humanSelected()
    {
        species = 0;
        if (isDebugOn) { Debug.Log("GameManager: Species = " + species); }
    }
    private void alienSelected()
    {
        species = 1;
        if (isDebugOn) { Debug.Log("GameManager: Species = " + species); }
    }
    private void cyborgSelected()
    {
        species = 2;
        if (isDebugOn) { Debug.Log("GameManager: Species = " + species); }
    }
    private void maleSelected()
    {
        gender = 0;
        if (isDebugOn) { Debug.Log("GameManager: Gender = " + gender); }
    }
    private void femaleSelected()
    {
        gender = 1;
        if (isDebugOn) { Debug.Log("GameManager: Gender = " + gender); }
    }
}
