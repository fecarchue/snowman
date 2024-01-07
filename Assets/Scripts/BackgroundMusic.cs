using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject slider;
    private static BackgroundMusic instance;

    // �ٸ� ��ũ��Ʈ���� �ν��Ͻ��� ����
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

    private void Awake()
    {
        // �ν��Ͻ��� �̹� �ִ� ��� �ߺ� ������ ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //�ʱ⼳��
            audioSource = GetComponent<AudioSource>();

            //������� �ʱ� ����
            audioSource.loop = true;

            audioSource.Play();
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

}