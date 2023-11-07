using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour //UI ���θ� �ٷ�� ��ũ��Ʈ
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
        //0�� min, 1�� max
        healthBar.value = playerProperties.health / playerProperties.maxHealth;
        //0�� �ǵ� ���� ���� ������, �ƿ� ��Ȱ��ȭ
        if (healthBar.value <= 0)
            healthBar.transform.Find("Fill Area").gameObject.SetActive(false);
        else
            healthBar.transform.Find("Fill Area").gameObject.SetActive(true);

        //float�� ���� �Ҽ��� �ڸ���
        maxHealthText.text = ((int)playerProperties.maxHealth).ToString();
        healthText.text = ((int)playerProperties.health).ToString();
        massText.text = ((int)playerProperties.mass).ToString();
        weightText.text = ((int)playerProperties.weight).ToString();
    }
}
