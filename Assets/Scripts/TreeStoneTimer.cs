using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStoneTimer : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);        //�ִϸ��̼� ������ 1�� �� ������� ����
    }

    

    void Update()
    {   //Ű���� �¿츸 ��� �� �����ְ� ��ȴ�
        Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);     //Vector3�������� x�� ���ŷ� ����, y��z�� 0,0
        if (Input.GetKey(KeyCode.LeftArrow))
        {                            //GetKey��Ű���忡������ KeyCode��LeftArrow����ȭ��ǥ�ν��ϸ�
            transform.position -= moveTo;                               //�� ���ȿ� moveTo��ŭ ��������
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += moveTo;
        }
    }

}
