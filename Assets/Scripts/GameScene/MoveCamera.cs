using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public float zRatio = -25f; //눈 반지름 : 카메라 z축 비율
    public float ySync = 5f;
    private float playerScale;
    private float posX, posY, posZ; //zRatio를 반영한 최종 z축, 눈 크기에 비례
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
            posZ = playerScale * zRatio; //눈 반지름과 카메라 z축이 정비례할 때, 눈 크기가 일정하게 보인다

            //눈 크기의 차이 때문에 중앙으로 가까워지는 듯 보임, 따라서 눈 크기만큼 -y
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
