using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private string SceneName;

    private static SceneController instance = null;

    public static SceneController Instance
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
        // 인스턴스가 여러 개 생성되지 않도록 함
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

    public void SwitchScene()
    {
        // 버튼 이름 가져오기
        GameObject clickButton = EventSystem.current.currentSelectedGameObject;
        SceneName = clickButton.name;

        // 버튼이름과 같은 이름의 씬으로 전환
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