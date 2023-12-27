using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject snowPrefab;  // 눈 입자 프리팹
    public GameObject fogPrefab;
    public float snowSpawnInterval = 0.2f;      // 입자 생성 간격 (초)
    public float snowSpeedRange = 5f;   // 입자 속력 범위
    public float snowTimer = 0.5f;     // 입자 생존 시간 (초)

    private float spawnTimer = 0f;

    private int playerSize = 0;
    private int currentSize;
    public int fogAmount = 15;
    public float fogDistance = 0.7f;
    public float fogScale = 0.5f;

    void Update()
    {
        // 일정 간격으로 눈 입자 생성
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= snowSpawnInterval)
        {
            SpawnSnowParticle();
            spawnTimer = 0f;
        }

        currentSize = GetComponent<PlayerData>().playerSize;
        if (playerSize != currentSize)
        {
            playerSize = currentSize;
            SpawnFog();
        }
    }

    void SpawnSnowParticle()
    {
        // 랜덤 속력 
        float randomSpeed = Random.Range(-snowSpeedRange, snowSpeedRange);

        // 랜덤 방향
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f).normalized;

        // 눈 입자 생성
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 0.7f; // 적절한 높이로 조절
        // 눈 입자 생성
        GameObject snowParticle = Instantiate(snowPrefab, spawnPosition, Quaternion.identity);

        // 눈 입자에 방향과 속력 적용
        snowParticle.GetComponent<Rigidbody2D>().velocity = randomDirection * randomSpeed;

        // 일정 시간 후에 눈 입자 삭제
        Destroy(snowParticle, snowTimer);
    }

    void SpawnFog()
    {
        for(int i = 0; i < fogAmount; i++)
        {
            //float randomAngle = Random.Range(0f, 360f);
            float randomAngle = i * (360f / fogAmount);
            float snowScale = GetComponent<PlayerData>().snowScale;

            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * snowScale * fogDistance;
            GameObject fogParticle = Instantiate(fogPrefab, spawnPosition, Quaternion.identity);
            fogParticle.transform.localScale = new Vector3(snowScale * fogScale, snowScale * fogScale, 0);
            
            Destroy(fogParticle, 0.7f);
        }
    }
}
