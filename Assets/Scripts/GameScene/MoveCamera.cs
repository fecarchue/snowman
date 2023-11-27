using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    public float zRatio = -25f; //�� ������ : ī�޶� z�� ����
    private float playerScale;
    public float initialZ; //ī�޶� �ʱ� z��
    public float newZ; //zRatio�� �ݿ��� ���� z��, �� ũ�⿡ ���
    [SerializeField]
    private void Start()
    {
        initialZ = transform.position.z;
        StartCoroutine(CameraUp());
    }
    private IEnumerator CameraUp()
    {
        while (true) // ���� �ݺ�
        { // ���� �̵�
            playerScale = player.GetComponent<Player>().newScale;
            //�� �������� ī�޶� z���� ������� ��, �� ũ�Ⱑ �����ϰ� ���δ�
            newZ = playerScale * zRatio+30;                                                                                 //�ȼ������۾Ƽ� 30���� �ϴ��߰��غ� (11-27 ����)
            //�� ũ���� ���� ������ �߾����� ��������� �� ����, ���� �� ũ�⸸ŭ -y
            transform.position = new Vector3(target.position.x, target.position.y+5 - (3.3f * playerScale), newZ);          //�������̻��� 5���� �ϴ��߰��غ� (11-27 ����)
            yield return null; // �� �������� ��ٸ�
        }
    }

}
