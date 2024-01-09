using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public float zRatio = -25f; //�� ������ : ī�޶� z�� ����
    public float ySync = 5f;
    private float playerScale;
    private float posX, posY, posZ; //zRatio�� �ݿ��� ���� z��, �� ũ�⿡ ���
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
            posX = player.transform.position.x;
            posY = player.transform.position.y - (ySync * playerScale);
            posZ = playerScale * zRatio; //�� �������� ī�޶� z���� ������� ��, �� ũ�Ⱑ �����ϰ� ���δ�

            //�� ũ���� ���� ������ �߾����� ��������� �� ����, ���� �� ũ�⸸ŭ -y
            transform.position = new Vector3(posX, posY, posZ);

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
            transform.position = new Vector3(posX, Mathf.Lerp(posY, playerPosY, time / duration), posZ); 
            time += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator GoalCamera()
    {
        while (true)
        {
            playerPosY = player.transform.position.y;
            transform.position = new Vector3(posX, playerPosY < posY ? playerPosY : posY, posZ);
            yield return null;
        }
    }

    public void Goal()
    {
        StopAllCoroutines();
        StartCoroutine(GoalCamera());
    }

    public void Fail()
    {
        StopAllCoroutines();
        StartCoroutine(FailCamera());
    }

}
