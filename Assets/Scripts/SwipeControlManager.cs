using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class SwipeControlManager : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;

    private float rawtouch; // 처음 클릭한 x 좌표
    private float rawswipe; // 처음 클릭 후 마우스의 수평 변위
    private float realtouch; // 처음 클릭 시 좌표의 가중치
    private float realswipe; // 수평 변위에 가중치를 적용한 값

    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        Debug.Log(swipe);
    }

    private void Update()
    {
        // 클릭한 좌표를 SwipeListener 스크립트에서 받아와서 출력
        if (Input.GetMouseButtonDown(0))
        {
            rawtouch = swipeListener.GetSwipeStartPoint().x; // 처음 클릭한 x 좌표
            rawswipe = 0f; // 처음 클릭 후 마우스의 수평 변위 초기화(클릭후 유지->스와이프시 변위 기록)
            realtouch = Mathf.Abs(rawtouch - 540); // 중심으로부터 거리 확인(1080x1920이라서 540이 중심임ㅇㅇ)
            realswipe = rawswipe; // 수평 변위에 가중치를 적용한 값

            // 디버깅 정보 출력
            Debug.Log("rawtouch: " + rawtouch);
            Debug.Log("rawswipe: " + rawswipe);
            Debug.Log("realtouch: " + realtouch);
            Debug.Log("realswipe: " + realswipe);
        }

        // 수평 변위 감지
        if (Input.GetMouseButton(0))
        {
            rawswipe = Input.mousePosition.x - rawtouch; // 수평 변위 감지
            realswipe = rawswipe; // 가중치 적용

            // 디버깅 정보 출력
            Debug.Log("rawtouch: " + rawtouch);
            Debug.Log("rawswipe: " + rawswipe);
            Debug.Log("realtouch: " + realtouch);
            Debug.Log("realswipe: " + realswipe);
        }
    }

    // 다른 스크립트에서 rawtouch 값을 참조할 수 있도록 반환하는 메서드
    public float GetRawTouch()
    {
        return rawtouch;
    }

    // 다른 스크립트에서 rawswipe 값을 참조할 수 있도록 반환하는 메서드를 추가할 수 있습니다.
    public float GetRawSwipe()
    {
        return rawswipe;
    }

    // 다른 스크립트에서 realtouch 값을 참조할 수 있도록 반환하는 메서드를 추가할 수 있습니다.
    public float GetRealTouch()
    {
        return realtouch;
    }

    // 다른 스크립트에서 realswipe 값을 참조할 수 있도록 반환하는 메서드를 추가할 수 있습니다.
    public float GetRealSwipe()
    {
        return realswipe;
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
}
