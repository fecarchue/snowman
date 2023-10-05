using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStone : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;          //�ʱ�ӷ��̴�


    //public void SetMoveSpeed(float moveSpeed)
    //{         //public�ؼ� �ٸ� class������ �� �� �ְ� ����
    //    this.moveSpeed = moveSpeed;              //moveSpeed�� ������Ʈ ���ִ� �޼��带 ����.
    //}


    private void Start()
    { StartCoroutine(MoveTreeStone()); }

    private IEnumerator MoveTreeStone()
    {
        while (true) // ���� �ݺ�
        { // ���� �̵�
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;    //������ ��� ��������.
            yield return null; // �� �������� ��ٸ�
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   //�̰� �浹�Ͼ�� Ʈ���ŷ� ���ӿ�������� �״��� �׷� ȿ���ߵ���Ű�°�
        if (other.gameObject.tag == "Player")//�浹�� other���� ���� tag�� ���� Ȯ��, player ������ �ߵ�!
        { Destroy(gameObject); }
           
    }
}


