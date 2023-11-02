using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public float zRatio = -25f; //�� ������ : ī�޶� z�� ����
    public float playerScale;
    public float initialZ;
    public float newZ;
    [SerializeField]
    private void Start()
    {
        initialZ=transform.position.z; //ī�޶� �ʱ� z��
        StartCoroutine(CameraUp());
    }
    private IEnumerator CameraUp()
    {
        //���� ���߾����� ��������� �� ������ �������� ���� �ʿ�
        while (true) // ���� �ݺ�
        { // ���� �̵�
            playerScale = GameObject.FindWithTag("Player").GetComponent<Player>().newScale;
            //�� �������� ī�޶� z���� ������� ��, �� ũ�Ⱑ �����ϰ� ���δ�
            newZ = playerScale * zRatio;
            transform.position = new Vector3(target.position.x, target.position.y - 4f, newZ);
            yield return null; // �� �������� ��ٸ�
        }
    }

}
