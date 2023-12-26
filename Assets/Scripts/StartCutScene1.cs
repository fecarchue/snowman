using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartCutScene1 : MonoBehaviour
{
    public GameObject CutSceneObj;
    private Image CutScene;
    public Sprite[] CutScenes;

    public void Awake()
    {
        PlayerPrefs.DeleteAll();
        CutScene = CutSceneObj.GetComponent<Image>();

        if(!PlayerPrefs.HasKey("IsFirst"))
        {
            Debug.Log("실행");
            StartCoroutine(ShowCutScene());
            PlayerPrefs.SetString("IsFirst", "NotFirst");
            PlayerPrefs.Save();
        }
        else
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    IEnumerator ShowCutScene()
    {
        CutSceneObj.SetActive(true);
        CutScene.sprite = CutScenes[0];
        StartCoroutine(FadeIn(2.0f));
        yield return new WaitForSeconds(2.0f);

        for(int i = 1; i < CutScenes.Length; i++)
        {
            StartCoroutine(FadeOut(2.0f));
            yield return new WaitForSeconds(2.0f);

            CutScene.sprite = CutScenes[i];

            StartCoroutine(FadeIn(2.0f));
            yield return new WaitForSeconds(2.0f);
        }

        StartCoroutine(FadeOut(2.0f));
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("StartScene");
    }

    IEnumerator FadeIn(float fadeInTime)
    {
        Color color = CutScene.color;
        color.a = 0;
        CutScene.color = color;

        // 페이드 인
        while (color.a < 1)
        {
            color.a += Time.deltaTime / fadeInTime;
            color.a = Mathf.Clamp01(color.a);
            CutScene.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut(float fadeOutTime)
    {
        Color color = CutScene.color;
        color.a = 1;
        CutScene.color = color;

        // 페이드 아웃
        while (color.a > 0)
        {
            color.a -= Time.deltaTime / fadeOutTime;
            color.a = Mathf.Clamp01(color.a);
            CutScene.color = color;
            yield return null;
        }
    }
}
