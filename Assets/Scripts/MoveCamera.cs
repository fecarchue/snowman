using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;


//이거는 카메라뒤로당기기 테스트임
    [SerializeField]
    private float moveSpeed = 100f; // 이동 속도 미리 정의
    private void Start()
    { StartCoroutine(CameraUp()); }
    private IEnumerator CameraUp()
    {
        while (true) // 무한 반복
        { // 위로 이동
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;    //밑으로 계속 내려간다.
            yield return null; // 한 프레임을 기다림
        }
    }
//이거는 카메라 뒤로 당기기 테스트임


void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y-3f, transform.position.z);
    }
}
