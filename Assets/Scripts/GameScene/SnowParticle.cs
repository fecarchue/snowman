using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowParticle : MonoBehaviour
{
    public GameObject snowParticlePrefab;  // 눈 입자 프리팹
    public float spawnInterval = 0.2f;      // 입자 생성 간격 (초)
    public float particleSpeedRange = 5f;   // 입자 속력 범위
    public float particleLifetime = 100f;     // 입자 생존 시간 (초)

    private float spawnTimer = 0f;

    void Update()
    {
        // 일정 간격으로 눈 입자 생성
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnSnowParticle();
            spawnTimer = 0f;
        }
    }

    void SpawnSnowParticle()
    {
        // 랜덤 속력 
        float randomSpeed = Random.Range(-particleSpeedRange, particleSpeedRange);

        // 랜덤 방향
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f).normalized;



        // 눈 입자 생성
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 0.7f; // 적절한 높이로 조절
        // 눈 입자 생성
        GameObject snowParticle = Instantiate(snowParticlePrefab, spawnPosition, Quaternion.identity);




        // 눈 입자에 방향과 속력 적용
        snowParticle.GetComponent<Rigidbody2D>().velocity = randomDirection * randomSpeed;

        // 일정 시간 후에 눈 입자 삭제
        Destroy(snowParticle, 0.5f);
    }
}
