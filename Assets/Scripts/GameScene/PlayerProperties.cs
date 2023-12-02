using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    //Inspector���� ���� ����
    public float maxHealth = 10f; //�⺻
    public float health = 10f;
    public float volume = 1f;
    public float weight = 1f;
    
    public float maxHealthRate = 1f; //����ġ
    public float healthRate = 1f;
    public float volumeRate = 1f;
    public float weightRate = 1f;
    public float collisionRate = 5f;

    public float growthRate = 1f; // Ŀ���� ����
    public float cameraScale; // degul12 ���� ��ũ��; ī�޶�, Ʈ���Ͽ�
    public float snowScale; // �׷��� ��ȯ ����� ���� Scale

    public int playerSize = 0;
    public float[] sizeCut = { 12, 16, 20, 26, 36, 48, 64, 86, 116, 202, 9999};

    public UI ui;

    void Start()
    {

    }

    void Update()
    {

        if (snowScale > sizeCut[playerSize + 1] / sizeCut[playerSize]) playerSize++;

        cameraScale = growthRate * Mathf.Pow(volume, 0.333333f); //������ 3���� 1�� ������
        snowScale = cameraScale * 12 / sizeCut[playerSize]; //12���� pixel��

        transform.localScale = new Vector3(snowScale, snowScale, 1);  //������ ����

        if (health <= 0) //���ӿ���
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
