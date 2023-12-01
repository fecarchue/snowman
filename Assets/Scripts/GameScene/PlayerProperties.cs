using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    //Inspector���� ���� ����
    public float maxHealth = 12f; //�⺻
    public float health = 12f;
    public float volume = 12f;
    public float weight = 12f;
    
    public float maxHealthRate = 1f; //����ġ
    public float healthRate = 1f;
    public float volumeRate = 1f;
    public float weightRate = 1f;
    public float collisionRate = 5f;

    public float growthRate = 0.1f; // �ʴ� Ŀ���� ����
    private float initialScale; // �ʱ� ������
    public float newScale; //�� ũ��

    public int playerSize = 0;
    public int[] sizeCut = { 16, 20, 26, 36, 48, 64, 86, 116, 202, 9999};

    public UI ui;

    void Start()
    {
        initialScale = transform.localScale.x; // �ʱ� ������ ����
    }

    void Update()
    {
        //������ 3���� 1�� ������, �� ũ��� �� ������ ���
        newScale = initialScale + growthRate * Mathf.Pow(volume, 0.66666f);
        transform.localScale = new Vector3(newScale, newScale, 1);  // ������ ����

        if (volume > sizeCut[playerSize]) playerSize++;

        if (health <= 0)
        {
            health = 0;
            ui.fail();
        }
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //����ġ�� �ý��۱�ȹ�� ȭ��ǥ ���� ����
            //Snowground���� ������ ��ŭ �� �༭ ���
            maxHealth -= 2f * maxHealthRate * Time.deltaTime;
            health -= 4f * healthRate * Time.deltaTime;
        }
        //Snowground�� �׻� ��� ����
        if (other.gameObject.tag == "Snowground")
        {
            maxHealth += 2f * maxHealthRate * Time.deltaTime;
            if (health < maxHealth) health += 3f * healthRate * Time.deltaTime;
            volume += 1f * volumeRate * Time.deltaTime;
            weight += 1f * weightRate * Time.deltaTime;
        }
        
    }

    public GameObject particle1, particle2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            //rock(����) �߰� �ʿ�
            if (other.gameObject.tag == "Stone")
            {
                health -= 2f * collisionRate;
                volume += 2f * collisionRate;
                weight += 3f * collisionRate;
                //�ش� �ڸ��κ��� ��ƼŬ ����, TreeStoneTimer ����
                //Instantiate(particle1, transform.position, transform.rotation);
                Destroy(other.gameObject); //��ֹ��� ����
            }
            if (other.gameObject.tag == "Tree")
            {
                health -= 2f * collisionRate;
                volume += 3f * collisionRate;
                weight += 2f * collisionRate;
                //Instantiate(particle2, transform.position, transform.rotation);
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "Goal")
            {
                ui.finish();
            }
        }
    }
}
