using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    VoiceLineManager voiceLineManager;

    public int score;
    public int highScore;

    public float musicVolume;
    public float voiceVolume;

    public bool firstTime = true;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadGame();

        if (firstTime) 
        {
            musicVolume = 1;
            voiceVolume = 1;
            firstTime = false;
        }

        LoadScene("MainMenu");
    }
    private void Start()
    {
        voiceLineManager = VoiceLineManager.instance;
    }

    public void StartGame() 
    {
        StartCoroutine(_StartGame());
    }

    public IEnumerator _StartGame() 
    {
        LoadScene("GameScreen");

        yield return new WaitUntil(() => FindObjectOfType<LevelGenerator>() != null);

        if (PlayerPrefs.GetInt("Controls") != 1)
            StartCoroutine(ShowControls(3.0f));


        score = 0;


        LoadGame();

        CountScore();

        InputManager.instance.UseActionMap("Player");
        UIManager.instance.RefreshEntireUI();

        voiceLineManager.PlayClip(voiceLineManager.GetRandomClip(voiceLineManager.gameStartClips));
    }

    public void LoadScene(string name)
    {
        SaveGame();
        SceneManager.LoadScene(name);
        LoadGame();
    }

    private void CountScore()
    {
        InvokeRepeating("IncrementScore", 0, LevelGenerator.instance.scoreIncrementInterval);
    }

    private void StopCountingScore() 
    {
        CancelInvoke();
        if (score > highScore) 
        {
            highScore = score;
        }
    }

    public void IncrementScore()
    {
        SetScore(score + LevelGenerator.instance.scoreIncrementAmount);
    }
    public void SetScore(int newScore) 
    {
        score = newScore;
        UIManager.instance.UpdateScoreText();
    }

    public void SaveGame() 
    {
        PlayerPrefs.SetInt("High Score", highScore);
        PlayerPrefs.SetFloat("Music Volume", musicVolume);
        PlayerPrefs.SetFloat("Voice Volume", voiceVolume);
    }

    public void LoadGame() 
    {
        highScore = PlayerPrefs.GetInt("High Score");
        musicVolume = PlayerPrefs.GetFloat("Music Volume");
        voiceVolume = PlayerPrefs.GetFloat("Voice Volume");
    }

    public void KillPlayer() 
    {
        StopCountingScore();
        SaveGame();
        UIManager.instance.ShowDeathScreen();
        InputManager.instance.UseActionMap("Menu");
        voiceLineManager.PlayClip(voiceLineManager.GetRandomClip(voiceLineManager.gameLoseClips));
    }

    public void UpdateAudioLevel() 
    {
        foreach (MusicPlayer musicPlayer in FindObjectsOfType<MusicPlayer>())
        {
            musicPlayer.SetAudioLevel(musicVolume);
        }

        VoiceLineManager.instance.SetAudioLevel(voiceVolume);
    }

    private IEnumerator ShowControls(float duration)
    {
        PlayerPrefs.SetInt("Controls", 1);

        HelpCanvas helpCanvas = FindObjectOfType<HelpCanvas>();
        CanvasGroup canvasGroup = helpCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        
        yield return new WaitForSeconds(duration);
        canvasGroup.alpha = 0;
    }

    public void QuitGame() {
        SaveGame();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

}
