using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject[] particlePrefab = new GameObject[6];  // ���� ������
    public GameObject[] fogPrefab = new GameObject[3];
    public float particleInterval = 0.2f;      // ���� ���� ���� (��)
    public float particleSpeedRange = 5f;   // ���� �ӷ� ����
    public float particleSurviveTime = 0.5f;     // ���� ���� �ð� (��)

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
            // ���� �������� �� ���� ����
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
        // ���� �ӷ� 
        float randomSpeed = Random.Range(-particleSpeedRange, particleSpeedRange);

        // ���� ����
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f).normalized;

        // ���� ����
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 0.7f; // ������ ���̷� ����

        if(GetComponentInChildren<PlayerTrigger>().isGround) spriteNumber = Random.Range(4, 6);
        else spriteNumber = Random.Range(1, 3);
        GameObject particle = Instantiate(particlePrefab[spriteNumber], spawnPosition, Quaternion.identity);

        // ���ڿ� ����� �ӷ� ����
        particle.GetComponent<Rigidbody2D>().velocity = randomDirection * randomSpeed;

        // ���� �ð� �Ŀ� ���� ����
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
