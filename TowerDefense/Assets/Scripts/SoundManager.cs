using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //public static SoundManger instance;

    public AudioClip balistaShoot, cannonShoot, fireballShoot, music;
    AudioSource balistaShootSource, cannonShootSource, fireballShootSource, musicSource;
    public float loadedVolume;

    private void Start()
    {

        balistaShootSource = AddAudio(balistaShoot, false, false, loadedVolume);
        cannonShootSource = AddAudio(cannonShoot, false, false, loadedVolume);
        fireballShootSource = AddAudio(fireballShoot, false, false, loadedVolume);
        musicSource = AddAudio(music, false, false, loadedVolume);

        PlayMusicSound();
    }
    private void Update()
    {
        loadedVolume = LoadSettings();
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

    public void PlayBalistaSound()
    {
        balistaShootSource.PlayOneShot(balistaShoot, loadedVolume);
    }
    public void PlayCannonSound()
    {
        cannonShootSource.PlayOneShot(cannonShoot, loadedVolume);
    }

    public void PlayFireBallSound()
    {
        fireballShootSource.PlayOneShot(fireballShoot, loadedVolume);
    }
    public void PlayMusicSound()
    {
        musicSource.PlayOneShot(music, loadedVolume);
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

        loadedVolume = LoadSettings();
        balistaShootSource = AddAudio(balistaShoot, false, false, loadedVolume);
        cannonShootSource = AddAudio(cannonShoot, false, false, loadedVolume);
        fireballShootSource = AddAudio(fireballShoot, false, false, loadedVolume);
        musicSource = AddAudio(music, false, false, loadedVolume);
    }


    public float LoadSettings()
    {
        float valueSave;
        return valueSave = PlayerPrefs.GetFloat("SfxVolume");
    }

}
