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

    public void SortButton()
    {
        DataManager.Instance.SortData();
    }

    // Start �Լ����� ��ư�� �ڵ����� Ŭ�� �̺�Ʈ �Լ��� ����
    void Awake()
    {
        ThisObj = this.gameObject;
        Button button = GetComponent<Button>();

        // ��ư�� null�� �ƴ϶��
        if (button != null)
        {
            if (ThisObj.name == "Sort")
            {
                button.onClick.AddListener(SortButton);
            }
            else// ��ư�� Ŭ�� �̺�Ʈ �Լ�(Scenechange) ����
            {
                button.onClick.AddListener(Scenechange);
            }
        }
    }
}
