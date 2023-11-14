using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    //Inspector���� ���� ����
    public float maxHealth = 10f; //�⺻
    public float health = 10f;
    public float mass = 10f;
    public float weight = 10f;
    
    public float maxHealthRate = 1f; //����ġ
    public float healthRate = 1f;
    public float massRate = 1f;
    public float weightRate = 1f;
    public float collisionRate = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //����ġ�� �ý��۱�ȹ�� ȭ��ǥ ���� ����
            //Ground�� SnowGround ���ÿ� ��� ������ �ν�, ��� ����?
            //�ϴ� Snowground���� ������ ��ŭ �� �༭ ���
            maxHealth -= 2f * maxHealthRate * Time.deltaTime;
            health -= 4f * healthRate * Time.deltaTime;
        }
        //Snowground�� �׻� ��� ����
        if (other.gameObject.tag == "Snowground")
        {
            maxHealth += 2f * maxHealthRate * Time.deltaTime;
            if (health < maxHealth) health += 3f * healthRate * Time.deltaTime;
            mass += 1f * massRate * Time.deltaTime;
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
                mass += 2f * collisionRate;
                weight += 3f * collisionRate;
                //�ش� �ڸ��κ��� ��ƼŬ ����, TreeStoneTimer ����
                Instantiate(particle1, transform.position, transform.rotation);
                Destroy(other.gameObject); //��ֹ��� ����
            }
            if (other.gameObject.tag == "Tree")
            {
                health -= 2f * collisionRate;
                mass += 3f * collisionRate;
                weight += 2f * collisionRate;
                Instantiate(particle2, transform.position, transform.rotation);
                Destroy(other.gameObject);
            }

        }
    }
}
