using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    public float zRatio = -3f; //�� ������ : ī�޶� z�� ����
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
        while (true)
        {
            playerScale = player.GetComponent<PlayerData>().cameraScale;
            //�� �������� ī�޶� z���� ������� ��, �� ũ�Ⱑ �����ϰ� ���δ�
            newZ = playerScale * zRatio;
            //�� ũ���� ���� ������ �߾����� ��������� �� ����, ���� �� ũ�⸸ŭ -y
            transform.position = new Vector3(target.position.x, target.position.y - (0.4f * playerScale), newZ);
            yield return null;
        }
    }

}
