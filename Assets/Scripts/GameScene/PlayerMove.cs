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
    public float rawSwipe; //�ҷ�����
    public float maxSwipeConst = 250f; //�ִ� �������� ��� ����
    public float xSpeedRate = 0.004f; //���� �ӵ� ����
    public float x1Speed; //���� ���� ��
    public float x2Speed; //�����̵� ��
    public float x3Speed; //���� �ӵ�
    public float xAccel = 10f; //����
    public float xDecel = 5f; //����
    private float diagTemp;
    public float yDefaultSpeed = 1f; // �̵� �ӵ� �̸� ����
    public float y1Speed; //�����̵� ��
    public float y2Speed; //���� �ӵ�

    public bool isAlternative = true;
    public bool isSlide = false;
    public float slideSpeedRate = 1.5f;
    public float slideMultiplier = 1f;

    private void Start()
    {
        StartCoroutine(MovePlayer());
        //swipeManager = FindObjectOfType<SwipeControlManager>();
    }

    private IEnumerator MovePlayer()
    {
        while (true) // ���� �ݺ�
        {

            //rawXSpeed�� (-1 * xSpeedRate * maxSwipeConst) ~ (xSpeedRate * maxSwipeConst)�� ����
            rawSwipe = swipeControl.GetComponent<SwipeControlManager>().rawswipe;
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

            //xSpeed�� x, yDefaultSpeed�� y�� ����. �밢�� diagTemp�� sqrt(x����+y����)
            diagTemp = Mathf.Pow(Mathf.Pow(x1Speed, 2) + Mathf.Pow(yDefaultSpeed, 2), 0.5f);
            y1Speed = Mathf.Pow(yDefaultSpeed, 2) / diagTemp; //������ ���� ���� ���� �����Ѵ�

            y1Speed = isAlternative ? yDefaultSpeed : y1Speed;

            if (isSlide)
            {
                if (slideMultiplier >= slideSpeedRate) slideMultiplier = slideSpeedRate;
                else slideMultiplier += Time.deltaTime * 0.75f;
            }
            else
            {
                if (slideMultiplier <= 1) slideMultiplier = 1;
                else slideMultiplier -= Time.deltaTime * 0.75f;
            }

            x3Speed = x2Speed * slideMultiplier;
            y2Speed = y1Speed * slideMultiplier;

            transform.position += Vector3.right * x3Speed * Time.deltaTime;
            transform.position += Vector3.down * y2Speed * Time.deltaTime;


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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Slide") isSlide = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Slide") isSlide = false;
    }
}