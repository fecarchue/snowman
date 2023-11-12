using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject scrollbar;
    private Scrollbar VolumeScrollbar;
    private static BackgroundMusic _instance;

    // �ٸ� ��ũ��Ʈ���� �ν��Ͻ��� ������ �� �ִ� ������Ƽ
    public static BackgroundMusic Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��� ���� ����
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("BackgroundMusic");
                _instance = singletonObject.AddComponent<BackgroundMusic>();
            }

            return _instance;
        }
    }

    // �ٸ� Ŭ�������� ����� ������ �޼��� ���� ������ �� ����
    public int myVariable;

    private void Awake()
    {
        // �ν��Ͻ��� �̹� �ִ� ��� �ߺ� ������ ����
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        // ����Ƽ������ �̱����� ���� �����ص� �ı����� �ʵ��� ����
        DontDestroyOnLoad(this.gameObject);
        VolumeScrollbar = scrollbar.GetComponent<Scrollbar>();
        audioSource = GetComponent<AudioSource>();

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