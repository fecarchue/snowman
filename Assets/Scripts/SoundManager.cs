using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject scrollbar;
    private Scrollbar VolumeScrollbar;
    private void Awake()
    {
        DontDestroyOnLoad(this); //�� ��ȯ�ǵ� �ı����� �ʰ�
        VolumeScrollbar = scrollbar.GetComponent<Scrollbar>();
        audioSource = GetComponent < AudioSource>();

        //������� �ʱ� ����
        audioSource.loop = true;
        audioSource.volume = VolumeScrollbar.value;

        audioSource.Play();
    }
    public void SetVolume() // ��ũ�ѹ� ���� ����Ͽ� ������ ����
    {
        audioSource.volume = VolumeScrollbar.value;
    }
}