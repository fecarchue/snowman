using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStoneTimer : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    public float playerScale;

    void Start()
    {
        //Find�� �� ���� ������...
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        Destroy(gameObject, 1f);        //1�� �� ����
        StartCoroutine(MovePrefab());
    }

    private IEnumerator MovePrefab()
    {
        while (true) // ���� �ݺ�
        {
            transform.position = playerTransform.position; //�� ��ġ�� ��ġ
            playerScale = player.GetComponent<Player>().newScale;
            //�� ũ��͵� ��ġ
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);

            yield return null; // �� �������� ��ٸ�
        }
    }


}
