using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private string SceneName;

    //�̱���
    private static SceneController instance = null;
    public static SceneController Instance
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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Win()
    {
        StartCoroutine(WinCoroutine());
    }

    public void Lose()
    {
        StartCoroutine(LoseCoroutine());
    }

    static IEnumerator WinCoroutine()
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