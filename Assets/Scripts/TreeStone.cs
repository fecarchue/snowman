using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStone : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;          //초기속력이다


    //public void SetMoveSpeed(float moveSpeed)
    //{         //public해서 다른 class에서도 쓸 수 있게 해줌
    //    this.moveSpeed = moveSpeed;              //moveSpeed를 업데이트 해주는 메서드를 만듬.
    //}


    private void Start()
    { StartCoroutine(MoveTreeStone()); }

    private IEnumerator MoveTreeStone()
    {
        while (true) // 무한 반복
        { // 위로 이동
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;    //밑으로 계속 내려간다.
            yield return null; // 한 프레임을 기다림
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   //이건 충돌일어날때 트리거로 게임오버라던가 죽던가 그런 효과발동시키는거
        if (other.gameObject.tag == "Player")//충돌한 other놈이 뭔지 tag을 통해 확인, player 맞으면 발동!
        { Destroy(gameObject); }
           
    }
}


