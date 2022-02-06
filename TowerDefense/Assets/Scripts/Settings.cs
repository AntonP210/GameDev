using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    //public AudioMixer volMixer;

    private int val;
    public Slider musicSlider, sfxSlider;
    private void Update()
    {
        MusicControl(musicSlider.value);
        SFXControl(sfxSlider.value);
    }
    private void Start()
    {
        SetSlidersValue();
    }
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }



    public void SetSlidersValue()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
    }
    public void MusicControl(float value)
    {
        Debug.Log("the value: " + value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public void SFXControl(float value)
    {
        Debug.Log("the value: " + value);
        PlayerPrefs.SetFloat("SfxVolume", value);
    }

}
