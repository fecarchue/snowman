using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour //UI 전부를 다루는 스크립트
{
    public GameObject player;
    public GameObject healthBarObject;
    public GameObject maxHealthTextObject;
    public GameObject healthTextObject;
    public GameObject massTextObject;
    public GameObject weightTextObject;
    private PlayerProperties playerProperties;
    private Slider healthBar;
    private TextMeshProUGUI maxHealthText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI massText;
    private TextMeshProUGUI weightText;

    void Start()
    {
        playerProperties = player.GetComponent<PlayerProperties>();
        healthBar = healthBarObject.GetComponent<Slider>();
        maxHealthText = maxHealthTextObject.GetComponent<TextMeshProUGUI>();
        healthText = healthTextObject.GetComponent<TextMeshProUGUI>();
        massText = massTextObject.GetComponent<TextMeshProUGUI>();
        weightText = weightTextObject.GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        //0이 min, 1이 max
        healthBar.value = playerProperties.health / playerProperties.maxHealth;
        //0이 되도 조금 남기 때문에, 아예 비활성화
        if (healthBar.value <= 0)
            healthBar.transform.Find("Fill Area").gameObject.SetActive(false);
        else
            healthBar.transform.Find("Fill Area").gameObject.SetActive(true);

        //float값 뒤의 소수점 자르기
        maxHealthText.text = ((int)playerProperties.maxHealth).ToString();
        healthText.text = ((int)playerProperties.health).ToString();
        massText.text = ((int)playerProperties.mass).ToString();
        weightText.text = ((int)playerProperties.weight).ToString();
    }
}
