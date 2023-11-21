using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour //UI 전부를 다루는 스크립트
{
    public Player player;
    public PlayerProperties playerProperties;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI massText;
    public TextMeshProUGUI weightText;
    public Image maxHealthBar;
    public Image healthBar;

    void Start()
    {
        maxHealthBar = maxHealthBar.GetComponent<Image>();
        healthBar = healthBar.GetComponent<Image>();
        playerProperties = player.GetComponent<PlayerProperties>();
        maxHealthText = maxHealthText.GetComponent<TextMeshProUGUI>();
        healthText = healthText.GetComponent<TextMeshProUGUI>();
        massText = massText.GetComponent<TextMeshProUGUI>();
        weightText = weightText.GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {

        maxHealthBar.rectTransform.sizeDelta = new Vector2(playerProperties.maxHealth, 13);
        healthBar.rectTransform.sizeDelta = new Vector2(playerProperties.health, 13);

        //float값 뒤의 소수점 자르기
        maxHealthText.text = ((int)playerProperties.maxHealth).ToString();
        healthText.text = ((int)playerProperties.health).ToString();
        massText.text = ((int)playerProperties.mass).ToString();
        weightText.text = ((int)playerProperties.weight).ToString();
    }
}
