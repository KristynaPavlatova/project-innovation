using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //game objects to access their AudioSources:
    public GameObject ContinueEmpty;//specific one time sound
    public List<GameObject> BackgroundMusic;

    //item 1,2 will play on awake (from start of the demo)
    //stop 1,2 after CharacterSubmited
    //item 3 - start after CharacterSubmited

    void Start()
    {
        CharacterCreationMenu.CharacterSubmited += stopBackgroundMusic;
        CharacterCreationMenu.CharacterSubmited += playContinueSound;//once character is created and submitted
        CharacterCreationMenu.CharacterSubmited += startBackgroundMusic;
    }
    private void OnDestroy()
    {
        CharacterCreationMenu.CharacterSubmited -= stopBackgroundMusic;
        CharacterCreationMenu.CharacterSubmited -= playContinueSound;//unsubscibing the event you are listening to
        CharacterCreationMenu.CharacterSubmited -= startBackgroundMusic;
    }

    private void playContinueSound()
    {
        ContinueEmpty.GetComponentInChildren<AudioSource>().Play();
        //continueSound.Play();//play the audio source
    }
    private void stopBackgroundMusic()
    {
        BackgroundMusic[0].GetComponentInChildren<AudioSource>().Stop();
        BackgroundMusic[1].GetComponentInChildren<AudioSource>().Stop();
    }
    private void startBackgroundMusic()
    {
        BackgroundMusic[2].GetComponentInChildren<AudioSource>().Play();
    }
}
