using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public float zRatio = -25f; //눈 반지름 : 카메라 z축 비율
    public float playerScale;
    public float initialZ;
    public float newZ;
    [SerializeField]
    private void Start()
    {
        initialZ=transform.position.z; //카메라 초기 z축
        StartCoroutine(CameraUp());
    }
    private IEnumerator CameraUp()
    {
        //눈이 정중앙으로 가까워지는 것 오프셋 조정으로 수정 필요
        while (true) // 무한 반복
        { // 위로 이동
            playerScale = GameObject.FindWithTag("Player").GetComponent<Player>().newScale;
            //눈 반지름과 카메라 z축이 정비례할 때, 눈 크기가 일정하게 보인다
            newZ = playerScale * zRatio;
            transform.position = new Vector3(target.position.x, target.position.y - 4f, newZ);
            yield return null; // 한 프레임을 기다림
        }
    }

}
