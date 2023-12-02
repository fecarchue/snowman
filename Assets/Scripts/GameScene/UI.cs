using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour //UI 전부를 다루는 스크립트
{
    public GameObject player;
    public PlayerProperties playerProperties;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI weightText;
    public Image maxHealthBar;
    public Image healthBar;
    public GameObject pauseMenu;
    public GameObject startMenu;
    public GameObject finishScreen;
    public GameObject failScreen;

    void Start()
    {
        maxHealthBar = maxHealthBar.GetComponent<Image>();
        healthBar = healthBar.GetComponent<Image>();
        playerProperties = player.GetComponent<PlayerProperties>();
        maxHealthText = maxHealthText.GetComponent<TextMeshProUGUI>();
        healthText = healthText.GetComponent<TextMeshProUGUI>();
        volumeText = volumeText.GetComponent<TextMeshProUGUI>();
        weightText = weightText.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0;
    }

    
    void Update()
    {
        if (startMenu.activeSelf && Input.GetMouseButtonDown(0)) //게임 시작
        {
            startMenu.SetActive(false);
            Time.timeScale = 1;
        }

        maxHealthBar.rectTransform.sizeDelta = new Vector2(playerProperties.maxHealth * 3, 13);
        healthBar.rectTransform.sizeDelta = new Vector2(playerProperties.health * 3, 13);

        //float값 뒤의 소수점 자르기
        maxHealthText.text = ((int)playerProperties.maxHealth).ToString();
        healthText.text = ((int)playerProperties.health).ToString();
        volumeText.text = ((int)playerProperties.volume).ToString();
        weightText.text = ((int)playerProperties.weight).ToString();
    }

    public void pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void finish()
    {
        Time.timeScale = 0;
        finishScreen.SetActive(true);
    }
    public void fail()
    {
        Time.timeScale = 0;
        failScreen.SetActive(true);
    }
    public void retry()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
