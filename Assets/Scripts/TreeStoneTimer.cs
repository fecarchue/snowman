using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStoneTimer : MonoBehaviour
{

    public Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 1f);        //�ִϸ��̼� ������ 1�� �� ������� ����
        StartCoroutine(MovePrefab());
    }

    private IEnumerator MovePrefab()
    {
        while (true) // ���� �ݺ�
        {
            transform.position = playerTransform.position;

            yield return null; // �� �������� ��ٸ�
        }
    }


}
