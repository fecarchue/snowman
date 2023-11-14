using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStoneTimer : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    public float playerScale;

    void Start()
    {
        //Find를 안 쓰고 싶은데...
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        Destroy(gameObject, 1f);        //1초 후 삭제
        StartCoroutine(MovePrefab());
    }

    private IEnumerator MovePrefab()
    {
        while (true) // 무한 반복
        {
            transform.position = playerTransform.position; //눈 위치와 일치
            playerScale = player.GetComponent<Player>().newScale;
            //눈 크기와도 일치
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);

            yield return null; // 한 프레임을 기다림
        }
    }


}
