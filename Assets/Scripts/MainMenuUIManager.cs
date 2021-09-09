using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button helpButton;
    [SerializeField] Button quitButton;

    [SerializeField] RectTransform buttonsContainer;

    [SerializeField] Button backtoMainMenubutton;

    [SerializeField] Canvas helpCanvas;


    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(OnClickPlay);
        optionsButton.onClick.AddListener(OnClickOptions);
        helpButton.onClick.AddListener(OnClickHelp);
        quitButton.onClick.AddListener(OnClickQuit);
        backtoMainMenubutton.onClick.AddListener(OnClickMainMenu);
    }

    private void OnClickMainMenu()
    {
        HideHelp();
    }

    private void OnClickHelp()
    {
        ShowHelp();
    }

    private void ShowHelp()
    {
        buttonsContainer.gameObject.SetActive(false);
        helpCanvas.gameObject.SetActive(true);
    }

    private void HideHelp() 
    {
        buttonsContainer.gameObject.SetActive(true);
        helpCanvas.gameObject.SetActive(false);
    }

    public void OnClickPlay() 
    {
        GameManager.instance.StartGame(); 
    }

    private void OnClickQuit()
    {
        GameManager.instance.QuitGame();
    }

    private void OnClickOptions()
    {
        GameManager.instance.LoadScene("OptionsMenu");
    }


}

