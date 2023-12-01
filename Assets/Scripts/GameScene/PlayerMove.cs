using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    private float diagTemp;
    public float yDefaultSpeed = 3f; // 이동 속도 미리 정의
    public float ySpeed;
    
    private SwipeControlManager swipeManager;

    public GameObject swipeControl;
    public float rawSwipe; //불러오기
    public float maxSwipeConst = 100f; //최대 스와이프 허용 범위
    public float xSpeedRate = 0.03f; //최종 속도 배율
    public float rawXSpeed; //최종 속도 목표
    public float xAccel = 0.2f; //가속
    public float xDecel = 0.05f; //감속
    public float xSpeed; //최종 속도


    private void Start()
    {
        StartCoroutine(MovePlayer());
        //swipeManager = FindObjectOfType<SwipeControlManager>();
    }

    private IEnumerator MovePlayer()
    {
        while (true) // 무한 반복
        { // 아래로 이동
            //rawXSpeed는 (-1 * xSpeedRate * maxSwipeConst) ~ (xSpeedRate * maxSwipeConst)로 제한
            rawSwipe = swipeControl.GetComponent<SwipeControlManager>().rawswipe;
            if (rawSwipe >= 0) rawXSpeed = xSpeedRate * Mathf.Min(maxSwipeConst, rawSwipe);
            else rawXSpeed = xSpeedRate * Mathf.Max(-maxSwipeConst, rawSwipe);

            if (rawXSpeed != 0) //가속용
            {
                if (xSpeed < rawXSpeed) xSpeed += xAccel * Time.deltaTime * 120f;
                else if (xSpeed > rawXSpeed) xSpeed -= xAccel * Time.deltaTime * 120f;
                if (Mathf.Abs(rawXSpeed - xSpeed) < xAccel) xSpeed = rawXSpeed; //rawXSpeed를 넘어버릴때 +-+- 왔다갔다 방지
            }
            else //감속용
            {
                if (xSpeed < 0) xSpeed += xDecel * Time.deltaTime * 120f;
                else if (xSpeed > 0) xSpeed -= xDecel * Time.deltaTime * 120f;
                if (Mathf.Abs(rawXSpeed - xSpeed) < xDecel) xSpeed = 0; //0 근처에서 +-+- 왔다갔다 방지
            }

            transform.position += Vector3.right * xSpeed * Time.deltaTime;

            //xSpeed를 x, yDefaultSpeed를 y라 하자. 대각선 diagTemp는 sqrt(x제곱+y제곱)
            diagTemp = Mathf.Pow(Mathf.Pow(rawXSpeed, 2) + Mathf.Pow(yDefaultSpeed, 2), 0.5f);
            ySpeed = Mathf.Pow(yDefaultSpeed, 2) / diagTemp; //닮음비에 따라 다음 식이 성립한다
            transform.position += Vector3.down * ySpeed * Time.deltaTime;    //밑으로 계속 내려간다.
            

            

            //(실전용)좌우이동을 스와이프로 구현함, 속도인 horiSpeed는 swipe에 비례, 수직낙하에만 쓰는 moveSpeed는 horiSpeed랑 연동
            /*SwipeControlManager swipeControl = FindObjectOfType<SwipeControlManager>();
            float touch = swipeControl.GetRealTouch();              // touch값은 중심기준좌표(절대값)라 0~540임. 
            float swipe = swipeControl.GetRealSwipe();              //swipe값은 변위라서 대충 0~540정도임. 근데 L제한때문에 최대(touch는 3/4지점, swipe는 끝까지 일때) 270임. 기본세타리미트는 270/540정도로 (0~1/2)임최대 이게 90도가 되도록 파이를곱하자
            float standardize = 0.25f, pi = 180;                              //이 0~72900이 0~1이되게 해야되므로 일단 72900으로 나눠봄(나중에 표준화나 그런거 적용해봐도될듯)      standardize는 1/72900해서 소수점3 이후 나머지버림한거임 1 안넘어가게. pi는 파이임.
            float thetalimit = (swipe / touch) * standardize * pi;           //각도상한선 세타를 정하고, 그 세타값이 되기까지 각속도 a만큼 그 방향으로 가게 구현해야됨 그거로 x좌표y좌표 삼각함수로 처리해서 구현.... 내일하자................................................
            float a = swipe / Mathf.Abs(swipe);                     //일단 a는 가중치없는 1로 두고 진행 **********나중에 가중치 바뀔수있음************

            if (swipe > 0)
            {
                for (float theta = 0; theta <= thetalimit; theta += a)           //세타는 a만큼 증가 증가하는내내 x랑 y 이렇게됨
                {
                    float angleInRadians = Mathf.Deg2Rad * theta;             //ㅠ/180 곱해줘서 라디안으로바꿈
                    float sinValue = Mathf.Sin(angleInRadians);             //이게 x성분
                    float cosValue = Mathf.Cos(angleInRadians);             //이게 y성분으로 바뀔속도. 이미 moveSpeed로떨어지고있으니  그건상쇄하고 이걸넣자
                    transform.position += Vector3.down * moveSpeed * cosValue * Time.deltaTime;
                    transform.position += Vector3.right * moveSpeed * sinValue * Time.deltaTime;
                    Debug.Log("thetalimit: " + thetalimit);
                    Debug.Log("sinValue:" + sinValue);
                    Debug.Log("angleinrad " + theta);
                    yield return null;

                }

            }*/

            /* else if (swipe < 0)
            {
                for (float theta = 0; theta <= Mathf.Abs(thetalimit); theta += a)           //세타는 a만큼 증가 증가하는내내 x랑 y 이렇게됨
                {
                    float angleInRadians = Mathf.Deg2Rad * theta;             //ㅠ/180 곱해줘서 라디안으로바꿈
                    float sinValue = Mathf.Sin(angleInRadians);             //이게 x성분
                    float cosValue = Mathf.Cos(angleInRadians);             //이게 y성분으로 바뀔속도. 이미 moveSpeed로떨어지고있으니  그건상쇄하고 이걸넣자
                    transform.position += Vector3.up * (moveSpeed - cosValue) * Time.deltaTime;
                    transform.position += Vector3.left * sinValue * 100 * Time.deltaTime;
                }
            }
            */





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
}