using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;

    private float a; // 처음 클릭한 x 좌표
    private float b; // 처음 클릭 후 마우스의 수평 변위
    private float q; // 처음 클릭 시 좌표의 가중치
    private float w; // 수평 변위에 가중치를 적용한 값

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
            a = swipeListener.GetSwipeStartPoint().x; // 처음 클릭한 x 좌표
            b = 0f; // 처음 클릭 후 마우스의 수평 변위 초기화
            q = Mathf.Abs(a - 500); // 가중치 계산
            w = q * b; // 수평 변위에 가중치를 적용한 값

            // 디버깅 정보 출력
            Debug.Log("a: " + a);
            Debug.Log("b: " + b);
            Debug.Log("q: " + q);
            Debug.Log("w: " + w);
        }

        // 수평 변위 감지
        if (Input.GetMouseButton(0))
        {
            b = Input.mousePosition.x - a; // 수평 변위 감지
            w = q * b; // 가중치 적용

            // 디버깅 정보 출력
            Debug.Log("a: " + a);
            Debug.Log("b: " + b);
            Debug.Log("q: " + q);
            Debug.Log("w: " + w);
        }
    }

    // 다른 스크립트에서 a 값을 참조할 수 있도록 반환하는 메서드
    public float GetA()
    {
        return a;
    }

    // 다른 스크립트에서 b 값을 참조할 수 있도록 반환하는 메서드를 추가할 수 있습니다.
    public float GetB()
    {
        return b;
    }

    // 다른 스크립트에서 q 값을 참조할 수 있도록 반환하는 메서드를 추가할 수 있습니다.
    public float GetQ()
    {
        return q;
    }

    // 다른 스크립트에서 w 값을 참조할 수 있도록 반환하는 메서드를 추가할 수 있습니다.
    public float GetW()
    {
        return w;
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
}
