using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{

    public GameObject swipe;
    public PlayerData playerData;
    private float snowScale;
    private float rawSwipe; //불러오기
    public float maxSwipeConst = 250f; //최대 스와이프 허용 범위
    public float xSpeedRate = 0.02f; //최종 속도 배율
    private float x1Speed; //한계 내 순수속도
    private float x2Speed; //가속 감속
    private float x3Speed; //경사
    private float x4Speed; //스케일 보정
    private float x5Speed;
    public float xAccel = 30f; //가속
    public float xDecel = 10f; //감속
    private float diagTemp;
    public float yDefaultSpeed = 5f; // 이동 속도 미리 정의
    private float y1Speed; //경사
    private float y2Speed; //스케일 보정
    private float y3Speed;

    private bool isSlide;
    private bool isIce;
    private bool isRush;
    private bool isDash;
    public float slideSpeedRate = 1.5f;
    private float slideMultiplier = 1f;
    public float slideAccel = 0.75f;

    public float rushDuration, dashDuration;
    

    private void Start()
    {
        StartCoroutine(MovePlayer(false));
    }

    private IEnumerator MovePlayer(bool isGoal)
    {
        while (true) // 무한 반복
        {
            isSlide = GetComponentInChildren<PlayerTrigger>().isSlide;
            isIce = GetComponentInChildren<PlayerTrigger>().isIce;
            isRush = GetComponentInChildren<PlayerData>().isRushing;
            isDash = GetComponentInChildren<PlayerData>().isDashing;

            if (!isIce && !isRush) {        //ice가 아니고 rush가 아닐때
                //rawXSpeed는 (-1 * xSpeedRate * maxSwipeConst) ~ (xSpeedRate * maxSwipeConst)로 제한
                rawSwipe = swipe.GetComponent<Swipe>().dist;
                if (rawSwipe >= 0) x1Speed = xSpeedRate * Mathf.Min(maxSwipeConst, rawSwipe);
                else x1Speed = xSpeedRate * Mathf.Max(-maxSwipeConst, rawSwipe);

                if (x1Speed != 0) //가속용
                {
                    if (x2Speed < x1Speed) x2Speed += xAccel * Time.deltaTime;
                    else if (x2Speed > x1Speed) x2Speed -= xAccel * Time.deltaTime;
                    if (Mathf.Abs(x1Speed - x2Speed) < xAccel * Time.deltaTime) x2Speed = x1Speed; //x1Speed를 넘어버릴때 +-+- 왔다갔다 방지
                }
                else //감속용
                {
                    if (x2Speed < 0) x2Speed += xDecel * Time.deltaTime;
                    else if (x2Speed > 0) x2Speed -= xDecel * Time.deltaTime;
                    if (Mathf.Abs(x1Speed - x2Speed) < xDecel * Time.deltaTime) x2Speed = 0; //0 근처에서 +-+- 왔다갔다 방지
                }
                if (isSlide)
                {
                    if (slideMultiplier >= slideSpeedRate) slideMultiplier = slideSpeedRate;
                    else slideMultiplier += Time.deltaTime * slideAccel;
                }
                else
                {
                    if (slideMultiplier <= 1) slideMultiplier = 1;
                    else slideMultiplier -= Time.deltaTime * slideAccel;
                }


                x3Speed = x2Speed * slideMultiplier;
                y1Speed = yDefaultSpeed * slideMultiplier;

                snowScale = GetComponent<PlayerData>().snowScale;
                x4Speed = x3Speed * snowScale;
                y2Speed = y1Speed * snowScale;
            }
            else if (isRush) {
                rushDuration = GetComponentInChildren<PlayerData>().rushDuration;
                StartCoroutine(rushSpeedUp(rushDuration));
            }


            if (isGoal) x4Speed = 0;

            transform.position += Vector3.right * x4Speed * Time.deltaTime;
            transform.position += Vector3.down * y2Speed * Time.deltaTime;
            yield return null; // 한 프레임을 기다림
        }
    }


    public void Goal()
    {
        StopAllCoroutines();
        StartCoroutine(MovePlayer(true));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public IEnumerator rushSpeedUp(float duration) { //rush시 속도 증가
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            y2Speed = y1Speed * snowScale*2;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}