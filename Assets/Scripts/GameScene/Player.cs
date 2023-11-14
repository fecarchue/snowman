using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    private float mass;
    public float newScale; //�� ũ��
    [SerializeField]
    private float moveSpeed = 3f; // �̵� �ӵ� �̸� ����
    public static float growthRate = 0.1f; // �ʴ� Ŀ���� ����
    private float initialScale; // �ʱ� ������
    private SwipeControlManager swipeManager;

    public GameObject swipeControl;
    public float rawSwipe; //�ҷ�����
    public float maxSwipeConst = 100f; //�ִ� �������� ��� ����
    public float xSpeedRate = 0.05f; //���� �ӵ� ����
    public float xSpeed; //���� �ӵ�



    private void Start()
    {
        initialScale = transform.localScale.x; // �ʱ� ������ ����
        StartCoroutine(MovePlayer());
        //swipeManager = FindObjectOfType<SwipeControlManager>();
    }

    private IEnumerator MovePlayer()
    {
        while (true) // ���� �ݺ�
        { // �Ʒ��� �̵�
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;    //������ ��� ��������.

            mass = GetComponent<PlayerProperties>().mass;
            //������ 3���� 1�� ������, �� ũ��� �� ������ ���
            newScale = initialScale + growthRate * Mathf.Pow(mass, 0.66666f);
            transform.localScale = new Vector3(newScale, newScale, newScale);  // ������ ����

            rawSwipe = swipeControl.GetComponent<SwipeControlManager>().rawswipe;
            if (rawSwipe >= 0) xSpeed = xSpeedRate * Mathf.Min(maxSwipeConst, rawSwipe);
            else if (rawSwipe < 0) xSpeed = xSpeedRate * Mathf.Max(-maxSwipeConst, rawSwipe);
            transform.position += Vector3.right * xSpeed * Time.deltaTime;

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
            Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);     //Vector3�������� x�� ���ŷ� ����, y��z�� 0,0
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