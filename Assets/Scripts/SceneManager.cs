using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwicher: MonoBehaviour
{
    public string SceneName;

    private void Awake()
    {
        // 해당 게임 오브젝트가 씬 전환 시에 파괴되지 않도록 설정
        DontDestroyOnLoad(this.gameObject);
    }

    public void SwitchToNextScene()
    {
        // 버튼 이름 가져오기
        GameObject clickBotton = EventSystem.current.currentSelectedGameObject;
        SceneName = clickBotton.name;


        // 버튼이름과 같은 이름의 씬으로 전환
        SceneManager.LoadScene(SceneName);
    }
}
