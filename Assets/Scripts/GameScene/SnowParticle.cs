using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowParticle : MonoBehaviour
{
    public GameObject snowParticlePrefab;  // �� ���� ������
    public float spawnInterval = 0.2f;      // ���� ���� ���� (��)
    public float particleSpeedRange = 5f;   // ���� �ӷ� ����
    public float particleLifetime = 100f;     // ���� ���� �ð� (��)

    private float spawnTimer = 0f;

    void Update()
    {
        // ���� �������� �� ���� ����
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnSnowParticle();
            spawnTimer = 0f;
        }
    }

    void SpawnSnowParticle()
    {
        // ���� �ӷ� 
        float randomSpeed = Random.Range(-particleSpeedRange, particleSpeedRange);

        // ���� ����
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f).normalized;



        // �� ���� ����
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 0.7f; // ������ ���̷� ����
        // �� ���� ����
        GameObject snowParticle = Instantiate(snowParticlePrefab, spawnPosition, Quaternion.identity);




        // �� ���ڿ� ����� �ӷ� ����
        snowParticle.GetComponent<Rigidbody2D>().velocity = randomDirection * randomSpeed;

        // ���� �ð� �Ŀ� �� ���� ����
        Destroy(snowParticle, 0.5f);
    }
}
