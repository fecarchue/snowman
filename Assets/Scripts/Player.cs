using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float mass;
    public float newScale; //눈 크기
    [SerializeField]
    private float moveSpeed = 3f; // 이동 속도 미리 정의
    public static float growthRate = 0.1f; // 초당 커지는 비율
    private float initialScale; // 초기 스케일

    private void Start()
    {
        initialScale = transform.localScale.x; // 초기 스케일 저장
        StartCoroutine(MovePlayer());
    }

    private IEnumerator MovePlayer()
    {
        while (true) // 무한 반복
        { // 아래로 이동
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;    //밑으로 계속 내려간다.

            mass = GetComponent<PlayerProperties>().mass;
            //부피의 3분의 1이 반지름, 눈 크기는 그 제곱의 비례
            newScale = initialScale + growthRate * Mathf.Pow(mass, 0.66666f);
            transform.localScale = new Vector3(newScale, newScale, newScale);  // 스케일 적용

            //키보드 좌우만 명령은 쓸수있게 살렸다
            Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);     //Vector3개값에서 x만 저거로 설정, y랑z는 0,0
            if (Input.GetKey(KeyCode.LeftArrow))
            {                            //GetKey로키보드에서받은 KeyCode중LeftArrow왼쪽화살표인식하면
                transform.position -= moveTo;                               //그 동안에 moveTo만큼 움직이자
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += moveTo;
            }
            yield return null; // 한 프레임을 기다림
        }
    }
}