using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStoneTimer : MonoBehaviour
{

    public Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 1f);        //애니메이션 나오고 1초 후 사라지게 하자
        StartCoroutine(MovePrefab());
    }

    private IEnumerator MovePrefab()
    {
        while (true) // 무한 반복
        {
            transform.position = playerTransform.position;

            yield return null; // 한 프레임을 기다림
        }
    }


}
