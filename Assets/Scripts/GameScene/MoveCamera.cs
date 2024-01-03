using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public float zRatio = -25f; //눈 반지름 : 카메라 z축 비율
    public float ySync = 5f;
    private float playerScale;
    private float newZ; //zRatio를 반영한 최종 z축, 눈 크기에 비례
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
            //눈 반지름과 카메라 z축이 정비례할 때, 눈 크기가 일정하게 보인다
            newZ = playerScale * zRatio;
            //눈 크기의 차이 때문에 중앙으로 가까워지는 듯 보임, 따라서 눈 크기만큼 -y
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y - (ySync * playerScale), newZ);
            yield return null;
        }
    }

}
