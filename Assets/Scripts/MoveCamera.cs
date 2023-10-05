using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;


//�̰Ŵ� ī�޶�ڷδ��� �׽�Ʈ��
    [SerializeField]
    private float moveSpeed = 100f; // �̵� �ӵ� �̸� ����
    private void Start()
    { StartCoroutine(CameraUp()); }
    private IEnumerator CameraUp()
    {
        while (true) // ���� �ݺ�
        { // ���� �̵�
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;    //������ ��� ��������.
            yield return null; // �� �������� ��ٸ�
        }
    }
//�̰Ŵ� ī�޶� �ڷ� ���� �׽�Ʈ��


void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y-3f, transform.position.z);
    }
}
