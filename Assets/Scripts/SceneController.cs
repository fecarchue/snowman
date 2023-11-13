using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private string SceneName;

    private static SceneController _instance;

    // �ٸ� ��ũ��Ʈ���� ���� ������ ������Ƽ
    public static SceneController Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��� ���� ����
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneController>();

                // ���� �ν��Ͻ��� ������ ���� ����
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SceneController).Name);
                    _instance = singletonObject.AddComponent<SceneController>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        // �ν��Ͻ��� ���� �� �������� �ʵ��� ��
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SwitchScene()
    {
        // ��ư �̸� ��������
        GameObject clickButton = EventSystem.current.currentSelectedGameObject;
        SceneName = clickButton.name;

        // ��ư�̸��� ���� �̸��� ������ ��ȯ
        SceneManager.LoadScene(SceneName);
    }

    public void Win()
    {
        StartCoroutine(WinCoroutine());
    }

    public void Lose()
    {
        StartCoroutine(LoseCoroutine());
    }

        IEnumerator WinCoroutine()
    {
        SceneManager.LoadScene("FightScene");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator LoseCoroutine()
    {
        SceneManager.LoadScene("FightScene");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartScene");
    }
}