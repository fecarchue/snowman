using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject SettingWindow;
    private bool isActive = false; // Ȱ��/��Ȱ�� ���¸� ����

    public void OnSetting()  // ���� â Ȱ��ȭ,��Ȱ��ȭ
    {
        isActive = !isActive;
        SettingWindow.SetActive(isActive);
    }
}