using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour
{
    private AudioSource soundEffect;
    //-------------------------------------------------------------------------------------
    void Start()
    {
        soundEffect = this.GetComponent<AudioSource>();//defining the audio source
    }
    //-----------------------------------------------------------
    // BUTTON METHODS
    //-----------------------------------------------------------
    public void PlayAudioOnClick()
    {
        soundEffect.Play();
    }
}
