using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f; // 이동 속도 미리 정의
    private void Start()
    {StartCoroutine(MoveBackground());}
    private IEnumerator MoveBackground()
    {
        while (true) // 무한 반복
        { // 위로 이동
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;    //밑으로 계속 내려간다.
            yield return null; // 한 프레임을 기다림
        }
    }
}
