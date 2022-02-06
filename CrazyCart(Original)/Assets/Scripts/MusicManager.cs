using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource musicSource;
    //public static SoundManger instance;
    float vol, musicVolume = 0.5f;

    private void Start()
    {


    }
    private void Update()
    {
        LoadSettings();
    }
    void Awake()
    {
        musicSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadSettings();
    }
    
    public void LoadSettings()
    {
        float valueSave = PlayerPrefs.GetFloat("MusicVolume");
        Debug.Log("Saved value "+valueSave);
            musicSource.volume = valueSave;


    }
}
