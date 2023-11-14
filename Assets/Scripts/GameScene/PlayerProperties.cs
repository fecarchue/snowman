using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    //Inspector에서 수정 가능
    public float maxHealth = 10f; //기본
    public float health = 10f;
    public float mass = 10f;
    public float weight = 10f;
    
    public float maxHealthRate = 1f; //가중치
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
            //가중치는 시스템기획서 화살표 개수 기준
            //Ground와 SnowGround 동시에 밟는 것으로 인식, 어떻게 따로?
            //일단 Snowground에서 더해준 만큼 빼 줘서 상쇄
            maxHealth -= 2f * maxHealthRate * Time.deltaTime;
            health -= 4f * healthRate * Time.deltaTime;
        }
        //Snowground는 항상 밟고 있음
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
            //rock(바위) 추가 필요
            if (other.gameObject.tag == "Stone")
            {
                health -= 2f * collisionRate;
                mass += 2f * collisionRate;
                weight += 3f * collisionRate;
                //해당 자리로부터 파티클 복제, TreeStoneTimer 실행
                Instantiate(particle1, transform.position, transform.rotation);
                Destroy(other.gameObject); //장애물은 제거
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
