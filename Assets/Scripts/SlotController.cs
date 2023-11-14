using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private GameObject ThisObj;
    // ��ư�� �Ҵ��� �Լ�
    public void ButtonClick()
    {
        SceneController.Instance.SwitchScene();
    }

    // Start �Լ����� ��ư�� �ڵ����� Ŭ�� �̺�Ʈ �Լ��� ����
    void Awake()
    {
        ThisObj = this.gameObject;
        // �� ��ũ��Ʈ�� ������ ���� ������Ʈ�� Button ������Ʈ ��������
        Button button = GetComponent<Button>();

        // ��ư�� null�� �ƴ϶��
        if (button != null && ThisObj.tag != "Slot")
        {
            // ��ư�� Ŭ�� �̺�Ʈ �Լ�(ButtonClick) ����
            button.onClick.AddListener(ButtonClick);
        }
        else
        {
            Debug.LogError("�� ��ũ��Ʈ�� ������ ���� ������Ʈ�� Button ������Ʈ�� �����ϴ�.");
        }
    }
}