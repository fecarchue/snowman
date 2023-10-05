using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    public GameObject particle1, particle2;

    /*    [SerializeField]                //�� field�� ���� �� �ڵ忡���� private��������, ����Ƽ������ ������ �� �� �ִ� ������ ����� �ִ� ���̴�
        private GameObject[] weapons;      //weapon�����
        private int weaponIndex = 0;        ///���⼱�������� �ε����� ���� �����

        [SerializeField]
        private Transform shootTransform;   //shootTransform�̶�°� ���� ĳ���͸Ӹ����� �� ���������� Transform��ǥ�� �����ͼ� weapon�� �߻� �������� ����Ϸ��� ����
        [SerializeField]
        private float shootInterval = 0.1f;        //�Ѿ˿� ���͹����־ �߻翡 ���� �־��ش�.
        private float lastShotTime = 0f;
    */


//�̰͵� ī�޶� �ڷ� ���� �׽�Ʈ��
    public float growthRate = 0.1f; // �ʴ� Ŀ���� ����
    private float initialScale; // �ʱ� ������

    private void Start()
    {
        initialScale = transform.localScale.x; // �ʱ� ������ ����
     }

     private void LateUpdate()
     {
        float newScale = initialScale + growthRate * Time.time;// �ð��� ���� �������� ����
        transform.localScale = new Vector3(newScale, newScale, newScale);  // ������ ����
     }

    //�̰͵� ī�޶�ڷδ����׽�Ʈ�� ���� ��������



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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Stone")
        {
            //            Debug.Log("Game Over");
            Instantiate(particle1, transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "Tree")
        {
            //            Debug.Log("Game Over");
            Instantiate(particle2, transform.position, Quaternion.identity);
        }

    }
}
