using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    //Inspector에서 수정 가능
    public float maxHealth = 10f; //기본
    public float health = 10f;
    public float volume = 1f;
    public float weight = 1f;
    
    public float maxHealthRate = 1f; //가중치
    public float healthRate = 1f;
    public float volumeRate = 1f;
    public float weightRate = 1f;
    public float collisionRate = 5f;

    public float growthRate = 1f; // 커지는 비율
    public float cameraScale; // degul12 기준 눈크기; 카메라, 트레일용
    public float snowScale; // 그래픽 변환 고려한 실제 Scale

    public int playerSize = 0;
    public float[] sizeCut = { 12, 16, 20, 26, 36, 48, 64, 86, 116, 202, 9999};

    public UI ui;

    void Start()
    {

    }

    void Update()
    {

        if (snowScale > sizeCut[playerSize + 1] / sizeCut[playerSize]) playerSize++;

        cameraScale = growthRate * Mathf.Pow(volume, 0.333333f); //부피의 3분의 1이 반지름
        snowScale = cameraScale * 12 / sizeCut[playerSize]; //12분의 pixel수

        transform.localScale = new Vector3(snowScale, snowScale, 1);  //스케일 적용

        if (health <= 0) //게임오버
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
