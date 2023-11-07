using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject SettingWindow;
    private bool isActive = false; // 활성/비활성 상태를 추적

    public void OnSetting()  // 설정 창 활성화,비활성화
    {
        isActive = !isActive;
        SettingWindow.SetActive(isActive);
    }
}