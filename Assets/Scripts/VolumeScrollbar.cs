using UnityEngine;
using UnityEngine.UI;

public class VolumeScrollbar : MonoBehaviour
{
    private Scrollbar scrollbar;
    private void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.value = BackgroundMusic.Instance.audioSource.volume;

        scrollbar.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnValueChanged(float value)
    {
        BackgroundMusic.Instance.audioSource.volume = value;
    }
}
