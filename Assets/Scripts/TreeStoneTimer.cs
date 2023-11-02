using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStoneTimer : MonoBehaviour
{

    public Transform playerTransform;
    public float playerScale;

    void Start()
    {
        //transform은 불러오면 실시간 반영
        playerTransform = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 1f);        //애니메이션 나오고 1초 후 사라지게 하자
        StartCoroutine(MovePrefab());
    }

    private IEnumerator MovePrefab()
    {
        while (true) // 무한 반복
        {
            transform.position = playerTransform.position; //눈 위치와 일치
            //다른 변수는 transform과 달리 실시간 X, 따라서 반복문에서 눈 크기 불러옴
            playerScale = GameObject.FindWithTag("Player").GetComponent<Player>().newScale;
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);

            yield return null; // 한 프레임을 기다림
        }
    }


}
