using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f; // �̵� �ӵ� �̸� ����
    private void Start()
    {StartCoroutine(MoveBackground());}
    private IEnumerator MoveBackground()
    {
        while (true) // ���� �ݺ�
        { // ���� �̵�
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;    //������ ��� ��������.
            yield return null; // �� �������� ��ٸ�
        }
    }
}
