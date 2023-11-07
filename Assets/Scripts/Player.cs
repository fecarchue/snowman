using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float mass;
    public float newScale; //�� ũ��
    [SerializeField]
    private float moveSpeed = 3f; // �̵� �ӵ� �̸� ����
    public static float growthRate = 0.1f; // �ʴ� Ŀ���� ����
    private float initialScale; // �ʱ� ������

    private void Start()
    {
        initialScale = transform.localScale.x; // �ʱ� ������ ����
        StartCoroutine(MovePlayer());
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

            //Ű���� �¿츸 ����� �����ְ� ��ȴ�
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