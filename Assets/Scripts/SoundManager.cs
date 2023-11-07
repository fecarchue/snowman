using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject scrollbar;
    private Scrollbar VolumeScrollbar;
    private void Awake()
    {
        DontDestroyOnLoad(this); //씬 전환되도 파괴되지 않게
        VolumeScrollbar = scrollbar.GetComponent<Scrollbar>();
        audioSource = GetComponent < AudioSource>();

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