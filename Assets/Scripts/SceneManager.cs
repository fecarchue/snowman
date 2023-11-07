using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwicher: MonoBehaviour
{
    public string SceneName;

    private void Awake()
    {
        // �ش� ���� ������Ʈ�� �� ��ȯ �ÿ� �ı����� �ʵ��� ����
        DontDestroyOnLoad(this.gameObject);
    }

    public void SwitchToNextScene()
    {
        // ��ư �̸� ��������
        GameObject clickBotton = EventSystem.current.currentSelectedGameObject;
        SceneName = clickBotton.name;


        // ��ư�̸��� ���� �̸��� ������ ��ȯ
        SceneManager.LoadScene(SceneName);
    }
}
