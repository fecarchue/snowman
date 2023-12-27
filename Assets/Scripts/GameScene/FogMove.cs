using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMove : MonoBehaviour
{
    public GameObject player;
    private float randomAngle;
    private float duration = 0.7f;
    private float time = 0f;
    private float moveSpeed = 0.5f;
    Color from = new Color(1, 1, 1, 1);
    Color to = new Color(1, 1, 1, 0);
    void Start()
    {
        randomAngle = Random.Range(0f, 360f);
    }

    
    void Update()
    {
        transform.position += 
            new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * moveSpeed * Time.deltaTime;

        transform.GetComponent<SpriteRenderer>().material.color = 
            Color.Lerp(from, to, time / duration);
        time += Time.deltaTime;
    }
}
