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
    private float rawSwipe; //�ҷ�����
    public float maxSwipeConst = 250f; //�ִ� �������� ��� ����
    public float xSpeedRate = 0.02f; //���� �ӵ� ����
    private float x1Speed; //�Ѱ� �� �����ӵ�
    private float x2Speed; //���� ����
    private float x3Speed; //���
    private float x4Speed; //������ ����
    private float x5Speed;
    public float xAccel = 30f; //����
    public float xDecel = 10f; //����
    private float diagTemp;
    public float yDefaultSpeed = 5f; // �̵� �ӵ� �̸� ����
    private float y1Speed; //���
    private float y2Speed; //������ ����
    private float y3Speed;

    private bool isSlide;
    private bool isIce;
    public float slideSpeedRate = 1.5f;
    private float slideMultiplier = 1f;
    public float slideAccel = 0.75f;

    private void Start()
    {
        StartCoroutine(MovePlayer(false));
    }

    private IEnumerator MovePlayer(bool isGoal)
    {
        while (true) // ���� �ݺ�
        {
            isSlide = GetComponentInChildren<PlayerTrigger>().isSlide;
            isIce = GetComponentInChildren<PlayerTrigger>().isIce;

            if (!isIce) {

                //rawXSpeed�� (-1 * xSpeedRate * maxSwipeConst) ~ (xSpeedRate * maxSwipeConst)�� ����
                rawSwipe = swipe.GetComponent<Swipe>().dist;
                if (rawSwipe >= 0) x1Speed = xSpeedRate * Mathf.Min(maxSwipeConst, rawSwipe);
                else x1Speed = xSpeedRate * Mathf.Max(-maxSwipeConst, rawSwipe);

                if (x1Speed != 0) //���ӿ�
                {
                    if (x2Speed < x1Speed) x2Speed += xAccel * Time.deltaTime;
                    else if (x2Speed > x1Speed) x2Speed -= xAccel * Time.deltaTime;
                    if (Mathf.Abs(x1Speed - x2Speed) < xAccel * Time.deltaTime) x2Speed = x1Speed; //x1Speed�� �Ѿ������ +-+- �Դٰ��� ����
                }
                else //���ӿ�
                {
                    if (x2Speed < 0) x2Speed += xDecel * Time.deltaTime;
                    else if (x2Speed > 0) x2Speed -= xDecel * Time.deltaTime;
                    if (Mathf.Abs(x1Speed - x2Speed) < xDecel * Time.deltaTime) x2Speed = 0; //0 ��ó���� +-+- �Դٰ��� ����
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

            if (isGoal) x4Speed = 0;
            transform.position += Vector3.right * x4Speed * Time.deltaTime;
            transform.position += Vector3.down * y2Speed * Time.deltaTime;


            //(�׽�Ʈ��)Ű���� �¿츸 ����� �����ְ� ��ȴ�. ���߿��� �ڵ�
            Vector3 moveTo = new Vector3(yDefaultSpeed * Time.deltaTime, 0, 0);     //Vector3�������� x�� ���ŷ� ����, y��z�� 0,0
            if (Input.GetKey(KeyCode.LeftArrow))
            {                            //GetKey��Ű���忡������ KeyCode��LeftArrow����ȭ��ǥ�ν��ϸ�
                transform.position -= moveTo * 5;                               //�� ���ȿ� moveTo��ŭ ��������
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += moveTo * 5;
            }

            yield return null; // �� �������� ��ٸ�
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


}