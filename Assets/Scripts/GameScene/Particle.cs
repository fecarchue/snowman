using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject snowPrefab;  // �� ���� ������
    public GameObject fogPrefab;
    public float snowSpawnInterval = 0.2f;      // ���� ���� ���� (��)
    public float snowSpeedRange = 5f;   // ���� �ӷ� ����
    public float snowTimer = 0.5f;     // ���� ���� �ð� (��)

    private float spawnTimer = 0f;

    private int playerSize = 0;
    private int currentSize;
    public int fogAmount = 15;
    public float fogDistance = 0.7f;
    public float fogScale = 0.5f;

    void Update()
    {
        // ���� �������� �� ���� ����
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
        // ���� �ӷ� 
        float randomSpeed = Random.Range(-snowSpeedRange, snowSpeedRange);

        // ���� ����
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f).normalized;

        // �� ���� ����
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 0.7f; // ������ ���̷� ����
        // �� ���� ����
        GameObject snowParticle = Instantiate(snowPrefab, spawnPosition, Quaternion.identity);

        // �� ���ڿ� ����� �ӷ� ����
        snowParticle.GetComponent<Rigidbody2D>().velocity = randomDirection * randomSpeed;

        // ���� �ð� �Ŀ� �� ���� ����
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
