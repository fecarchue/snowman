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
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject startMenu;
    public GameObject[] goalScreen = new GameObject[4];
    public GameObject[] failScreen = new GameObject[4];

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
        StartCoroutine(ReadyUI());
    }

    private IEnumerator ReadyUI()
    {
        Time.timeScale = 0;
        while (true)
        {
            if (Input.GetMouseButtonDown(0)) //게임 시작
            {
                startMenu.SetActive(false);
                pauseButton.SetActive(true);
                StartCoroutine(PlayUI());
                break;
            }
            yield return null;
        }
    }
    
    private IEnumerator PlayUI()
    {
        Time.timeScale = 1;
        while (true)
        {
            maxHealthBar.rectTransform.sizeDelta = new Vector2(playerData.playerData[0] * barLength, 100);
            healthBar.rectTransform.sizeDelta = new Vector2(playerData.playerData[1] * barLength, 100);
            damageHealthBar.rectTransform.anchoredPosition = new Vector2(playerData.playerData[1] * barLength - 42.8571f, 0);
            damageHealthBar.rectTransform.sizeDelta = new Vector2(playerData.damage * barLength, 100);

            //float값 뒤의 소수점 자르기
            maxHealthText.text = ((int)playerData.playerData[0]).ToString();
            healthText.text = ((int)playerData.playerData[1]).ToString();
            volumeText.text = ((int)playerData.playerData[2]).ToString();
            powerText.text = ((int)playerData.playerData[3]).ToString();
            yield return null;
        }
    }

    private IEnumerator GoalUI()
    {
        pauseButton.SetActive(false);

        goalScreen[0].SetActive(true);
        goalScreen[1].SetActive(true);
        Color from = new Color(0, 0, 0, 0);
        Color to = new Color(0, 0, 0, 0.7f);
        float duration = 1f;
        float time = 0f;
        while (time < duration)
        {
            goalScreen[1].GetComponent<SpriteRenderer>().color =
                Color.Lerp(from, to, time / duration);
            time += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        goalScreen[2].SetActive(true);
        yield return new WaitForSeconds(1f);

        goalScreen[3].SetActive(true);
        goalScreen[4].SetActive(true);
    }

    private IEnumerator FailUI()
    {
        pauseButton.SetActive(false);

        failScreen[0].SetActive(true);
        yield return new WaitForSeconds(2.5f);

        failScreen[1].SetActive(true);
        yield return new WaitForSeconds(1f);

        failScreen[2].SetActive(true);
        failScreen[3].SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void Goal()
    {
        StartCoroutine(GoalUI());
    }
    public void Fail()
    {
        StartCoroutine(FailUI());
    }
    public void Retry()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
