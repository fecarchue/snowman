using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public float zRatio = -25f; //�� ������ : ī�޶� z�� ����
    public float ySync = 5f;
    private float playerScale;
    private float newZ; //zRatio�� �ݿ��� ���� z��, �� ũ�⿡ ���
    [SerializeField]
    private void Start()
    {
        StartCoroutine(CameraUp());
    }
    private IEnumerator CameraUp()
    {
        while (true)
        {
            playerScale = player.GetComponent<PlayerData>().snowScale;
            //�� �������� ī�޶� z���� ������� ��, �� ũ�Ⱑ �����ϰ� ���δ�
            newZ = playerScale * zRatio;
            //�� ũ���� ���� ������ �߾����� ��������� �� ����, ���� �� ũ�⸸ŭ -y
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y - (ySync * playerScale), newZ);
            yield return null;
        }
    }

}
