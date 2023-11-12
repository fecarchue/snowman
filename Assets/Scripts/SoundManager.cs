using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject scrollbar;
    private Scrollbar VolumeScrollbar;
    private static BackgroundMusic _instance;

    // 다른 스크립트에서 인스턴스에 접근할 수 있는 프로퍼티
    public static BackgroundMusic Instance
    {
        get
        {
            // 인스턴스가 없는 경우 새로 생성
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("BackgroundMusic");
                _instance = singletonObject.AddComponent<BackgroundMusic>();
            }

            return _instance;
        }
    }

    // 다른 클래스에서 사용할 변수나 메서드 등을 정의할 수 있음
    public int myVariable;

    private void Awake()
    {
        // 인스턴스가 이미 있는 경우 중복 생성을 방지
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        // 유니티에서는 싱글톤이 씬을 변경해도 파괴되지 않도록 설정
        DontDestroyOnLoad(this.gameObject);
        VolumeScrollbar = scrollbar.GetComponent<Scrollbar>();
        audioSource = GetComponent<AudioSource>();

        //배경음악 초기 설정
        audioSource.loop = true;
        audioSource.volume = VolumeScrollbar.value;

        audioSource.Play();
    }

    public void SetVolume() // 스크롤바 값을 사용하여 볼륨을 조절
    {
        audioSource.volume = VolumeScrollbar.value;
    }
}