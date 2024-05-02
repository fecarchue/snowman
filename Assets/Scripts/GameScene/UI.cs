using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class UI : MonoBehaviour //UI ���θ� �ٷ�� ��ũ��Ʈ
{
    public GameObject player;
    private PlayerData playerData;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI powerText;
    public RectTransform MaxSize;
    public Image healthBar;
    public Image maxHealthBar;
    public Image damageHealthBar;
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject startMenu;
    public GameObject[] goalScreen = new GameObject[4];
    public GameObject[] failScreen = new GameObject[4];
    private float maxHP, HP, damage;
    public float barLength;

    public RectTransform maxSize;
    private float maxSize_x;

    void Start()
    {
        maxSize_x = MaxSize.sizeDelta.x / 20f;
        barLength = MaxSize.sizeDelta.x / 50f;
        playerData = player.GetComponent<PlayerData>();
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
            if (Input.GetMouseButtonDown(0)) //���� ����
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
        bool damageCoroutine = false;
        maxHealthBar.rectTransform.sizeDelta = new Vector2(playerData.playerData[0] * maxSize_x, 100);

        Time.timeScale = 1;
        while (true)
        {
            maxHP = playerData.playerData[0];
            HP = playerData.playerData[1];
            damage = playerData.damage;
            
            if (maxHP < maxSize_x)
            {
                maxHealthBar.rectTransform.sizeDelta = new Vector2(maxHP * barLength, 100);
                healthBar.rectTransform.sizeDelta = new Vector2(HP * barLength, 100);
            }
            else if(HP <= maxSize_x)
            {
                healthBar.rectTransform.sizeDelta = new Vector2(HP * barLength, 100);
            }

            damageHealthBar.rectTransform.anchoredPosition = new Vector2(playerData.playerData[1] * barLength - 42.8571f, 0);
            damageHealthBar.rectTransform.sizeDelta = new Vector2(damage * barLength, 100);

            //float�� ���� �Ҽ��� �ڸ���
            healthText.text = "HP: " + ((int)playerData.playerData[1]).ToString() + " / " + ((int)playerData.playerData[0]).ToString();
            volumeText.text = "Volume: " + ((int)playerData.playerData[2]).ToString();
            powerText.text = "Power: " + ((int)playerData.playerData[3]).ToString();
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
}
