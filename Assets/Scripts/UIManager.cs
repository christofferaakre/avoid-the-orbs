using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] RectTransform healthContainer;

    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject emptyHeartPrefab;


    [SerializeField] int heartPadding;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI coinsText;

    [SerializeField] GameObject deathScreenCanvas;

    float heartWidth;


    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        heartWidth = heartPrefab.GetComponent<RectTransform>().sizeDelta.x;

    }

    public void RefreshEntireUI() 
    {
       UpdateHealthDisplay();
       UpdateScoreText();
       UpdateCoinsText();
    }

    

    public void ShowDeathScreen() 
    {
        finalScoreText.text = "Final score: " + GameManager.instance.score.ToString();
        highScoreText.text = "High score: " + GameManager.instance.highScore.ToString();
        deathScreenCanvas.SetActive(true);
    }

    public void HideDeathScreen() 
    {
        deathScreenCanvas.SetActive(false);
    }

    public void UpdateScoreText()
    {
        scoreText.text = GameManager.instance.score.ToString(); 
    }
    public void UpdateCoinsText()
    {
        coinsText.text = GameManager.instance.coins.ToString();
    }

    public void UpdateHealthDisplay()
    {
        int health = Player.instance.health;
        int maxHealth = Player.instance.maxHealth;

        // Clear health bar
        foreach (RectTransform child in healthContainer)
        {
            Destroy(child.gameObject);
        }

        // Draw right number of full hearts
        for (int i = 0; i < health; i++)
        {
            GameObject heart = Instantiate(heartPrefab);
            heart.transform.SetParent(healthContainer);
            heart.transform.position = healthContainer.transform.position + i * (heartWidth + heartPadding) * Vector3.right;

            RectTransform rectTransform = heart.GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0);
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0);
        }

        // Draw right number of empty hearts
        for (int i = health; i < maxHealth ; i++)
        {
            GameObject heart = Instantiate(emptyHeartPrefab);
            heart.transform.SetParent(healthContainer);
            heart.transform.position = healthContainer.transform.position + i * (heartWidth + heartPadding) * Vector3.right;

            RectTransform rectTransform = heart.GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0);
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0);
        }
    }

    public void OnClickPlayAgain() 
    {
        GameManager.instance.StartGame();
    }

    public void OnClickMainMenu() 
    {
        GameManager.instance.LoadScene("MainMenu");
    }

}
