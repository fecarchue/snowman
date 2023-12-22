using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour //UI 전부를 다루는 스크립트
{
    public GameObject player;
    private PlayerData playerData;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI powerText;
    public Image maxHealthBar;
    public Image healthBar;
    public Image damageHealthBar;
    public GameObject pauseMenu;
    public GameObject startMenu;
    public GameObject finishScreen;
    public GameObject failScreen;

    void Start()
    {
        maxHealthBar = maxHealthBar.GetComponent<Image>();
        healthBar = healthBar.GetComponent<Image>();
        damageHealthBar = damageHealthBar.GetComponent<Image>();
        playerData = player.GetComponent<PlayerData>();
        maxHealthText = maxHealthText.GetComponent<TextMeshProUGUI>();
        healthText = healthText.GetComponent<TextMeshProUGUI>();
        volumeText = volumeText.GetComponent<TextMeshProUGUI>();
        powerText = powerText.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0;
    }

    
    void Update()
    {
        if (startMenu.activeSelf && Input.GetMouseButtonDown(0)) //게임 시작
        {
            startMenu.SetActive(false);
            Time.timeScale = 1;
        }

        maxHealthBar.rectTransform.sizeDelta = new Vector2(playerData.playerData[0] * 3, 13);
        healthBar.rectTransform.sizeDelta = new Vector2(playerData.playerData[1] * 3, 13);
        damageHealthBar.rectTransform.anchoredPosition = new Vector2(playerData.playerData[1] * 15 + 10, -20);
        damageHealthBar.rectTransform.sizeDelta = new Vector2(playerData.damage * 3, 13);


        //float값 뒤의 소수점 자르기
        maxHealthText.text = ((int)playerData.playerData[0]).ToString();
        healthText.text = ((int)playerData.playerData[1]).ToString();
        volumeText.text = ((int)playerData.playerData[2]).ToString();
        powerText.text = ((int)playerData.playerData[3]).ToString();
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
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
