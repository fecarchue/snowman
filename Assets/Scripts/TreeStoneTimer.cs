using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStoneTimer : MonoBehaviour
{

    public Transform playerTransform;
    public float playerScale;

    void Start()
    {
        //transform�� �ҷ����� �ǽð� �ݿ�
        playerTransform = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 1f);        //�ִϸ��̼� ������ 1�� �� ������� ����
        StartCoroutine(MovePrefab());
    }

    private IEnumerator MovePrefab()
    {
        while (true) // ���� �ݺ�
        {
            transform.position = playerTransform.position; //�� ��ġ�� ��ġ
            //�ٸ� ������ transform�� �޸� �ǽð� X, ���� �ݺ������� �� ũ�� �ҷ���
            playerScale = GameObject.FindWithTag("Player").GetComponent<Player>().newScale;
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);

            yield return null; // �� �������� ��ٸ�
        }
    }


}
