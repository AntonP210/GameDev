using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //public static SoundManger instance;

    public AudioClip collisionSound, pickUpSound, steerSound, spinSound, gameOverSound, winSound;
    AudioSource collisionSource, pickUpSource, steerSource, spinSource, gameOverSource, winSource;
    public float loadedVolume;

    private void Start()
    {
        
        collisionSource = AddAudio(collisionSound, false, false, loadedVolume);
        pickUpSource = AddAudio(pickUpSound, false, false, loadedVolume);
        steerSource = AddAudio(steerSound, false, false, loadedVolume);
        spinSource = AddAudio(spinSound, false, false, loadedVolume);
        gameOverSource = AddAudio(gameOverSound, false, false, loadedVolume);
        winSource = AddAudio(winSound, false, false, loadedVolume);
    }
    private void Update() {
        loadedVolume=LoadSettings();
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    public void PlayCollisionSound()
    {
        collisionSource.PlayOneShot(collisionSound, loadedVolume);
    }
    public void PlayPickUpSound()
    {
        pickUpSource.PlayOneShot(pickUpSound, loadedVolume);
    }

    public void PlayGameOverSound()
    {
        gameOverSource.PlayOneShot(gameOverSound, loadedVolume);
    }
    public void PlaySpinSound()
    {
        spinSource.PlayOneShot(spinSound, loadedVolume);
    }
    public void PlaySteerSound()
    {
        steerSource.PlayOneShot(steerSound, loadedVolume);
    }
    public void PlayWinSound()
    {
        winSource.PlayOneShot(winSound, loadedVolume);
    }

    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
       
        loadedVolume=LoadSettings();
        collisionSource = AddAudio(collisionSound, false, false, loadedVolume);
        pickUpSource = AddAudio(pickUpSound, false, false, loadedVolume);
        steerSource = AddAudio(steerSound, false, false, loadedVolume);
        spinSource = AddAudio(spinSound, false, false, loadedVolume);
        gameOverSource = AddAudio(gameOverSound, false, false, loadedVolume);
        winSource = AddAudio(winSound, false, false, loadedVolume);
    }


    public float LoadSettings()
    {
        float valueSave;
        return valueSave = PlayerPrefs.GetFloat("SfxVolume");
        Debug.Log("Saved value " + valueSave);
        
    }

}
