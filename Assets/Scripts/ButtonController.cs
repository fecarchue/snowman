using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private GameObject ThisObj;
    public void Scenechange()
    {
        SceneController.Instance.SwitchScene();
    }

    // Start �Լ����� ��ư�� �ڵ����� Ŭ�� �̺�Ʈ �Լ��� ����
    void Awake()
    {
        ThisObj = this.gameObject;
        Button button = GetComponent<Button>();

        // ��ư�� null�� �ƴ϶��
        if (button != null)
        {
            // ��ư�� Ŭ�� �̺�Ʈ �Լ�(Scenechange) ����
            button.onClick.AddListener(Scenechange);
        }
    }
}
