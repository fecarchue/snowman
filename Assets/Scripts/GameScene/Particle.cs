using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject[] particlePrefab = new GameObject[6];  // 입자 프리팹
    public GameObject[] fogPrefab = new GameObject[3];
    public float particleInterval = 0.2f;      // 입자 생성 간격 (초)
    public float particleSpeedRange = 5f;   // 입자 속력 범위
    public float particleSurviveTime = 0.5f;     // 입자 생존 시간 (초)

    private float particleTimer = 0f;
    int spriteNumber;

    private int playerSize = 0;
    private int currentSize;
    public int fogAmount = 15;
    public float fogDistance = 0.7f;
    public float fogScale = 0.5f;

    void Start()
    {
        StartCoroutine(MakeParticle());
    }

    private IEnumerator MakeParticle()
    {
        while (true)
        {
            // 일정 간격으로 눈 입자 생성
            particleTimer += Time.deltaTime;
            if (particleTimer >= particleInterval)
            {
                SpawnParticle();
                particleTimer = 0f;
            }

            currentSize = GetComponent<PlayerData>().playerSize;
            if (playerSize != currentSize)
            {
                playerSize = currentSize;
                SpawnFog();
            }
            yield return null;
        }
    }

    void SpawnParticle()
    {
        // 랜덤 속력 
        float randomSpeed = Random.Range(-particleSpeedRange, particleSpeedRange);

        // 랜덤 방향
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f).normalized;

        // 입자 생성
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 0.7f; // 적절한 높이로 조절

        if(GetComponentInChildren<PlayerTrigger>().isGround) spriteNumber = Random.Range(4, 6);
        else spriteNumber = Random.Range(1, 3);
        GameObject particle = Instantiate(particlePrefab[spriteNumber], spawnPosition, Quaternion.identity);

        // 입자에 방향과 속력 적용
        particle.GetComponent<Rigidbody2D>().velocity = randomDirection * randomSpeed;

        // 일정 시간 후에 입자 삭제
        Destroy(particle, particleSurviveTime);
    }

    void SpawnFog()
    {
        for(int i = 0; i < fogAmount; i++)
        {
            //float randomAngle = Random.Range(0f, 360f);
            float randomAngle = i * (360f / fogAmount);
            float snowScale = GetComponent<PlayerData>().snowScale;

            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * snowScale * fogDistance;

            spriteNumber = Random.Range(1, 3);
            GameObject fog = Instantiate(fogPrefab[spriteNumber], spawnPosition, Quaternion.identity);
            fog.transform.localScale = new Vector3(snowScale * fogScale, snowScale * fogScale, 0);
            
            Destroy(fog, 0.7f);
        }
    }
}
