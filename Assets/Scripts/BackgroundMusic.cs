using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject scrollbar;
    private Scrollbar VolumeScrollbar;
    private static BackgroundMusic instance;

    // 다른 스크립트에서 인스턴스에 접근
    public static BackgroundMusic Instance
    {
        get
        {
            // 인스턴스가 없는 경우 새로 생성
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        // 인스턴스가 이미 있는 경우 중복 생성을 방지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //초기설정
            VolumeScrollbar = scrollbar.GetComponent<Scrollbar>();
            audioSource = GetComponent<AudioSource>();

            //배경음악 초기 설정
            audioSource.loop = true;
            audioSource.volume = VolumeScrollbar.value;

            audioSource.Play();
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
}