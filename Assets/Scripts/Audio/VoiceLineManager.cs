using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoiceLineManager : MonoBehaviour
{
    public static VoiceLineManager instance;

    AudioSource audioSource;
    float volume;

    [SerializeField] public List<AudioClip> gameStartClips = new List<AudioClip>();
    [SerializeField] public List<AudioClip> gameLoseClips = new List<AudioClip>();
    [SerializeField] public List<AudioClip> healthUpClips = new List<AudioClip>();
    [SerializeField] public List<AudioClip> coinClips = new List<AudioClip>();

    [SerializeField] public AudioClip healthUp;


    System.Random random;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;

        random = new System.Random();

    }

    private void Start()
    {
        volume = GameManager.instance.voiceVolume;
        SetAudioLevel(volume);
    }

    public void SetAudioLevel(float newVolume) 
    {
        audioSource.volume = newVolume;
    }

    public void PlayClip(AudioClip clip) 
    {
        audioSource.PlayOneShot(clip);
    }

    public AudioClip GetRandomClip(List<AudioClip> audioClips) 
    {
        return audioClips[random.Next(0, audioClips.Count)];
    }

}
