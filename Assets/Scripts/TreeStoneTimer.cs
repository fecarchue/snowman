using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStoneTimer : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);        //애니메이션 나오고 1초 후 사라지게 하자
    }

    

    void Update()
    {   //키보드 좌우만 명령 은 쓸수있게 살렸다
        Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);     //Vector3개값에서 x만 저거로 설정, y랑z는 0,0
        if (Input.GetKey(KeyCode.LeftArrow))
        {                            //GetKey로키보드에서받은 KeyCode중LeftArrow왼쪽화살표인식하면
            transform.position -= moveTo;                               //그 동안에 moveTo만큼 움직이자
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += moveTo;
        }
    }

}
