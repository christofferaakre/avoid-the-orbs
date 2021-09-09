using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    AudioSource audioSource;

    float volume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        volume = GameManager.instance.musicVolume;
        SetAudioLevel(volume);
        PlayMusic();
    }

    private void PlayMusic() 
    {
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetAudioLevel(float newVolume) 
    {
        audioSource.volume = newVolume; 
    }

}
