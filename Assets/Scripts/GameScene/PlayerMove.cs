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
    public float yDefaultSpeed = 3f; // �̵� �ӵ� �̸� ����
    public float ySpeed;
    
    private SwipeControlManager swipeManager;

    public GameObject swipeControl;
    public float rawSwipe; //�ҷ�����
    public float maxSwipeConst = 100f; //�ִ� �������� ��� ����
    public float xSpeedRate = 0.03f; //���� �ӵ� ����
    public float rawXSpeed; //���� �ӵ� ��ǥ
    public float xAccel = 0.2f; //����
    public float xDecel = 0.05f; //����
    public float xSpeed; //���� �ӵ�


    private void Start()
    {
        StartCoroutine(MovePlayer());
        //swipeManager = FindObjectOfType<SwipeControlManager>();
    }

    private IEnumerator MovePlayer()
    {
        while (true) // ���� �ݺ�
        { // �Ʒ��� �̵�
            //rawXSpeed�� (-1 * xSpeedRate * maxSwipeConst) ~ (xSpeedRate * maxSwipeConst)�� ����
            rawSwipe = swipeControl.GetComponent<SwipeControlManager>().rawswipe;
            if (rawSwipe >= 0) rawXSpeed = xSpeedRate * Mathf.Min(maxSwipeConst, rawSwipe);
            else rawXSpeed = xSpeedRate * Mathf.Max(-maxSwipeConst, rawSwipe);

            if (rawXSpeed != 0) //���ӿ�
            {
                if (xSpeed < rawXSpeed) xSpeed += xAccel * Time.deltaTime * 120f;
                else if (xSpeed > rawXSpeed) xSpeed -= xAccel * Time.deltaTime * 120f;
                if (Mathf.Abs(rawXSpeed - xSpeed) < xAccel) xSpeed = rawXSpeed; //rawXSpeed�� �Ѿ������ +-+- �Դٰ��� ����
            }
            else //���ӿ�
            {
                if (xSpeed < 0) xSpeed += xDecel * Time.deltaTime * 120f;
                else if (xSpeed > 0) xSpeed -= xDecel * Time.deltaTime * 120f;
                if (Mathf.Abs(rawXSpeed - xSpeed) < xDecel) xSpeed = 0; //0 ��ó���� +-+- �Դٰ��� ����
            }

            transform.position += Vector3.right * xSpeed * Time.deltaTime;

            //xSpeed�� x, yDefaultSpeed�� y�� ����. �밢�� diagTemp�� sqrt(x����+y����)
            diagTemp = Mathf.Pow(Mathf.Pow(rawXSpeed, 2) + Mathf.Pow(yDefaultSpeed, 2), 0.5f);
            ySpeed = Mathf.Pow(yDefaultSpeed, 2) / diagTemp; //������ ���� ���� ���� �����Ѵ�
            transform.position += Vector3.down * ySpeed * Time.deltaTime;    //������ ��� ��������.
            

            

            //(������)�¿��̵��� ���������� ������, �ӵ��� horiSpeed�� swipe�� ���, �������Ͽ��� ���� moveSpeed�� horiSpeed�� ����
            /*SwipeControlManager swipeControl = FindObjectOfType<SwipeControlManager>();
            float touch = swipeControl.GetRealTouch();              // touch���� �߽ɱ�����ǥ(���밪)�� 0~540��. 
            float swipe = swipeControl.GetRealSwipe();              //swipe���� ������ ���� 0~540������. �ٵ� L���Ѷ����� �ִ�(touch�� 3/4����, swipe�� ������ �϶�) 270��. �⺻��Ÿ����Ʈ�� 270/540������ (0~1/2)���ִ� �̰� 90���� �ǵ��� ���̸�������
            float standardize = 0.25f, pi = 180;                              //�� 0~72900�� 0~1�̵ǰ� �ؾߵǹǷ� �ϴ� 72900���� ������(���߿� ǥ��ȭ�� �׷��� �����غ����ɵ�)      standardize�� 1/72900�ؼ� �Ҽ���3 ���� �����������Ѱ��� 1 �ȳѾ��. pi�� ������.
            float thetalimit = (swipe / touch) * standardize * pi;           //�������Ѽ� ��Ÿ�� ���ϰ�, �� ��Ÿ���� �Ǳ���� ���ӵ� a��ŭ �� �������� ���� �����ؾߵ� �װŷ� x��ǥy��ǥ �ﰢ�Լ��� ó���ؼ� ����.... ��������................................................
            float a = swipe / Mathf.Abs(swipe);                     //�ϴ� a�� ����ġ���� 1�� �ΰ� ���� **********���߿� ����ġ �ٲ������************

            if (swipe > 0)
            {
                for (float theta = 0; theta <= thetalimit; theta += a)           //��Ÿ�� a��ŭ ���� �����ϴ³��� x�� y �̷��Ե�
                {
                    float angleInRadians = Mathf.Deg2Rad * theta;             //��/180 �����༭ �������ιٲ�
                    float sinValue = Mathf.Sin(angleInRadians);             //�̰� x����
                    float cosValue = Mathf.Cos(angleInRadians);             //�̰� y�������� �ٲ�ӵ�. �̹� moveSpeed�ζ�������������  �װǻ���ϰ� �̰ɳ���
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
                for (float theta = 0; theta <= Mathf.Abs(thetalimit); theta += a)           //��Ÿ�� a��ŭ ���� �����ϴ³��� x�� y �̷��Ե�
                {
                    float angleInRadians = Mathf.Deg2Rad * theta;             //��/180 �����༭ �������ιٲ�
                    float sinValue = Mathf.Sin(angleInRadians);             //�̰� x����
                    float cosValue = Mathf.Cos(angleInRadians);             //�̰� y�������� �ٲ�ӵ�. �̹� moveSpeed�ζ�������������  �װǻ���ϰ� �̰ɳ���
                    transform.position += Vector3.up * (moveSpeed - cosValue) * Time.deltaTime;
                    transform.position += Vector3.left * sinValue * 100 * Time.deltaTime;
                }
            }
            */





            //(�׽�Ʈ��)Ű���� �¿츸 ����� �����ְ� ��ȴ�. ���߿��� �ڵ�
            Vector3 moveTo = new Vector3(yDefaultSpeed * Time.deltaTime, 0, 0);     //Vector3�������� x�� ���ŷ� ����, y��z�� 0,0
            if (Input.GetKey(KeyCode.LeftArrow))
            {                            //GetKey��Ű���忡������ KeyCode��LeftArrow����ȭ��ǥ�ν��ϸ�
                transform.position -= moveTo;                               //�� ���ȿ� moveTo��ŭ ��������
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += moveTo;
            }

            yield return null; // �� �������� ��ٸ�
        }
    }
}