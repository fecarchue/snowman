using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    //Inspector에서 수정 가능
    public float maxHealth = 12f; //기본
    public float health = 12f;
    public float volume = 12f;
    public float weight = 12f;
    
    public float maxHealthRate = 1f; //가중치
    public float healthRate = 1f;
    public float volumeRate = 1f;
    public float weightRate = 1f;
    public float collisionRate = 5f;

    public float growthRate = 0.1f; // 초당 커지는 비율
    private float initialScale; // 초기 스케일
    public float newScale; //눈 크기

    public int playerSize = 0;
    public int[] sizeCut = { 16, 20, 26, 36, 48, 64, 86, 116, 202, 9999};

    public UI ui;

    void Start()
    {
        initialScale = transform.localScale.x; // 초기 스케일 저장
    }

    void Update()
    {
        //부피의 3분의 1이 반지름, 눈 크기는 그 제곱의 비례
        newScale = initialScale + growthRate * Mathf.Pow(volume, 0.66666f);
        transform.localScale = new Vector3(newScale, newScale, 1);  // 스케일 적용

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
            //가중치는 시스템기획서 화살표 개수 기준
            //Snowground에서 더해준 만큼 빼 줘서 상쇄
            maxHealth -= 2f * maxHealthRate * Time.deltaTime;
            health -= 4f * healthRate * Time.deltaTime;
        }
        //Snowground는 항상 밟고 있음
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
            //rock(바위) 추가 필요
            if (other.gameObject.tag == "Stone")
            {
                health -= 2f * collisionRate;
                volume += 2f * collisionRate;
                weight += 3f * collisionRate;
                //해당 자리로부터 파티클 복제, TreeStoneTimer 실행
                //Instantiate(particle1, transform.position, transform.rotation);
                Destroy(other.gameObject); //장애물은 제거
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
