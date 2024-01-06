using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour //UI ���θ� �ٷ�� ��ũ��Ʈ
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

    public float barLength = 20f;

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
        StartCoroutine(Ready());
    }

    private IEnumerator Ready()
    {
        Time.timeScale = 0;
        while (true)
        {
            if (startMenu.activeSelf && Input.GetMouseButtonDown(0)) //���� ����
            {
                startMenu.SetActive(false);
                StartCoroutine(Play());
                break;
            }
            yield return null;
        }
    }
    
    private IEnumerator Play()
    {
        Time.timeScale = 1;
        while (true)
        {
            maxHealthBar.rectTransform.sizeDelta = new Vector2(playerData.playerData[0] * barLength, 100);
            healthBar.rectTransform.sizeDelta = new Vector2(playerData.playerData[1] * barLength, 100);
            damageHealthBar.rectTransform.anchoredPosition = new Vector2(playerData.playerData[1] * barLength - 42.8571f, 0);
            damageHealthBar.rectTransform.sizeDelta = new Vector2(playerData.damage * barLength, 100);

            //float�� ���� �Ҽ��� �ڸ���
            maxHealthText.text = ((int)playerData.playerData[0]).ToString();
            healthText.text = ((int)playerData.playerData[1]).ToString();
            volumeText.text = ((int)playerData.playerData[2]).ToString();
            powerText.text = ((int)playerData.playerData[3]).ToString();
            yield return null;
        }
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
