using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    
    private SwipeControlManager swipeManager;

    public GameObject swipeControl;
    public PlayerData playerData;
    private float snowScale;
    private float rawSwipe; //불러오기
    public float maxSwipeConst = 250f; //최대 스와이프 허용 범위
    public float xSpeedRate = 0.02f; //최종 속도 배율
    private float x1Speed; //가속 감속 전
    private float x2Speed; //슬라이드 전
    private float x3Speed; //최종 속도
    private float x4Speed; //이젠 나도 모르겠다
    public float xAccel = 30f; //가속
    public float xDecel = 10f; //감속
    private float diagTemp;
    public float yDefaultSpeed = 5f; // 이동 속도 미리 정의
    private float y1Speed; //슬라이드 전
    private float y2Speed; 
    public float y3Speed; //최종 속도

    private bool isSlide = false;
    public float slideSpeedRate = 1.5f;
    private float slideMultiplier = 1f;
    public float slideAccel = 0.75f;

    private void Start()
    {
        StartCoroutine(MovePlayer());
        //swipeManager = FindObjectOfType<SwipeControlManager>();
    }

    private IEnumerator MovePlayer()
    {
        while (true) // 무한 반복
        {

            //rawXSpeed는 (-1 * xSpeedRate * maxSwipeConst) ~ (xSpeedRate * maxSwipeConst)로 제한
            rawSwipe = swipeControl.GetComponent<SwipeControlManager>().rawswipe;
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

            y1Speed = yDefaultSpeed;

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
            y2Speed = y1Speed * slideMultiplier;

            snowScale = GetComponent<PlayerData>().snowScale;
            x4Speed = x3Speed * snowScale;
            y3Speed = y2Speed * snowScale;
            transform.position += Vector3.right * x4Speed * Time.deltaTime;
            transform.position += Vector3.down * y3Speed * Time.deltaTime;


            //(테스트용)키보드 좌우만 명령은 쓸수있게 살렸다. 안중요한 코드
            Vector3 moveTo = new Vector3(yDefaultSpeed * Time.deltaTime, 0, 0);     //Vector3개값에서 x만 저거로 설정, y랑z는 0,0
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Slide") isSlide = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Slide") isSlide = false;
    }
}