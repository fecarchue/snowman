using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public float zRatio = -25f; //�� ������ : ī�޶� z�� ����
    public float ySync = 5f;
    private float playerScale;
    private float newX, newY, newZ; //zRatio�� �ݿ��� ���� z��, �� ũ�⿡ ���
    private float playerPosY;
    
    void Start()
    {
        StartCoroutine(InGameCamera());
    }
    private IEnumerator InGameCamera()
    {
        while (true)
        {
            playerScale = player.GetComponent<PlayerData>().snowScale;
            newX = player.transform.position.x;
            newY = player.transform.position.y - (ySync * playerScale);
            newZ = playerScale * zRatio; //�� �������� ī�޶� z���� ������� ��, �� ũ�Ⱑ �����ϰ� ���δ�

            //�� ũ���� ���� ������ �߾����� ��������� �� ����, ���� �� ũ�⸸ŭ -y
            transform.position = new Vector3(newX, newY, newZ);

            yield return null;
        }
    }

    private IEnumerator FailCamera()
    {
        float duration = 1f;
        float time = 0f;

        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            playerPosY = player.transform.position.y;
            transform.position = new Vector3(newX, Mathf.Lerp(newY, playerPosY, time / duration), newZ); 
            time += Time.deltaTime;

            yield return null;
        }
    }

    public void Fail()
    {
        StopAllCoroutines();
        StartCoroutine(FailCamera());
    }

}
