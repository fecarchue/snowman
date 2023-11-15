using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject scrollbar;
    private Scrollbar VolumeScrollbar;
    private static BackgroundMusic instance;

    // �ٸ� ��ũ��Ʈ���� �ν��Ͻ��� ������ �� �ִ� ������Ƽ
    public static BackgroundMusic Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��� ���� ����
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    // �ٸ� Ŭ�������� ����� ������ �޼��� ���� ������ �� ����
    public int myVariable;

    private void Awake()
    {
        // �ν��Ͻ��� �̹� �ִ� ��� �ߺ� ������ ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //�ʱ⼳��
            VolumeScrollbar = scrollbar.GetComponent<Scrollbar>();
            audioSource = GetComponent<AudioSource>();

            //������� �ʱ� ����
            audioSource.loop = true;
            audioSource.volume = VolumeScrollbar.value;

            audioSource.Play();
        }
        
        
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetVolume() // ��ũ�ѹ� ���� ����Ͽ� ������ ����
    {
        audioSource.volume = VolumeScrollbar.value;
    }
}