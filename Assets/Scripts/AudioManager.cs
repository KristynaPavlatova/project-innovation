using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource continueSound;//variable for the audio source you put in inspector

    void Start()
    {
        continueSound = GetComponent<AudioSource>();//defining the audio source
        CharacterCreationMenu.CharacterSubmited += playContinueSound;//once character is created and submitted
    }
    private void OnDestroy()
    {
        CharacterCreationMenu.CharacterSubmited -= playContinueSound;//unsubscibing the event you are listening to
    }

    private void playContinueSound()
    {
        continueSound.Play();//play the audio source
    }
}
