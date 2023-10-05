using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    public GameObject particle1, particle2;

    /*    [SerializeField]                //이 field를 통해 이 코드에서는 private변수지만, 유니티에서는 접근을 할 수 있는 변수로 만들어 주는 것이다
        private GameObject[] weapons;      //weapon만들기
        private int weaponIndex = 0;        ///무기선택을위한 인덱스를 지금 만든것

        [SerializeField]
        private Transform shootTransform;   //shootTransform이라는거 만들어서 캐릭터머리위의 그 지점에서의 Transform좌표를 가져와서 weapon의 발사 지점으로 사용하려고 만듬
        [SerializeField]
        private float shootInterval = 0.1f;        //총알에 인터벌을넣어서 발사에 텀을 넣어준다.
        private float lastShotTime = 0f;
    */


//이것도 카메라 뒤로 당기기 테스트임
    public float growthRate = 0.1f; // 초당 커지는 비율
    private float initialScale; // 초기 스케일

    private void Start()
    {
        initialScale = transform.localScale.x; // 초기 스케일 저장
     }

     private void LateUpdate()
     {
        float newScale = initialScale + growthRate * Time.time;// 시간에 따라 스케일을 증가
        transform.localScale = new Vector3(newScale, newScale, newScale);  // 스케일 적용
     }

    //이것도 카메라뒤로당기기테스트임 언제 지울지모름



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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Stone")
        {
            //            Debug.Log("Game Over");
            Instantiate(particle1, transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "Tree")
        {
            //            Debug.Log("Game Over");
            Instantiate(particle2, transform.position, Quaternion.identity);
        }

    }
}
