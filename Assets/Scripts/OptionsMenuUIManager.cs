using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUIManager : MonoBehaviour
{
    [SerializeField] Button mainMenuButton;

    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider voiceVolumeSlider;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(OnClickMainMenu);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderValueChange);
        voiceVolumeSlider.onValueChanged.AddListener(OnVoiceVolumeSliderValueChange);

        SetSliderPositions();
    }

    private void SetSliderPositions()
    {
        musicVolumeSlider.value = GameManager.instance.musicVolume;
        voiceVolumeSlider.value = GameManager.instance.voiceVolume;
    }

    private void OnClickMainMenu()
    {
        GameManager.instance.LoadScene("MainMenu");
    }

    private void OnMusicVolumeSliderValueChange(float value) 
    {
        GameManager.instance.musicVolume = value;
        GameManager.instance.UpdateAudioLevel();
    }

     private void OnVoiceVolumeSliderValueChange(float value) 
     {
        GameManager.instance.voiceVolume = value;
        GameManager.instance.UpdateAudioLevel();
     }

}
