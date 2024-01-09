using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartCutScene : MonoBehaviour
{
    public GameObject CutSceneObj, BackgroundObj,TouchToStartObj;
    private Image CutScene, Background, TouchToStart;
    public Sprite[] CutScenes;
    public Sprite SnowmanVsDevil;

    public void Awake()
    {
        Background = BackgroundObj.GetComponent<Image>();
        TouchToStart = TouchToStartObj.GetComponent<Image>();

        PlayerPrefs.DeleteAll();
        CutScene = CutSceneObj.GetComponent<Image>();

        if (!PlayerPrefs.HasKey("IsFirst"))
        {
            StartCoroutine(ShowCutScene());
            PlayerPrefs.SetString("IsFirst", "NotFirst");
            PlayerPrefs.Save();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator ShowCutScene()
    {
        CutSceneObj.SetActive(true);
        CutScene.sprite = CutScenes[0];
        StartCoroutine(FadeIn(2.0f));
        yield return new WaitForSeconds(2.0f);

        for (int i = 1; i < CutScenes.Length; i++)
        {
            StartCoroutine(FadeOut(2.0f));
            yield return new WaitForSeconds(2.0f);

            CutScene.sprite = CutScenes[i];

            StartCoroutine(FadeIn(2.0f));
            yield return new WaitForSeconds(2.0f);
        }

        StartCoroutine(FadeOut(2.0f));
        yield return new WaitForSeconds(2.0f);

        ClickScene();
    }

    public void ClickScene()
    {
        CutSceneObj.SetActive(false);
        Background.sprite = SnowmanVsDevil;
        Background.color = Color.white;
        TouchToStartObj.SetActive(true);

        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        Color color = new Color(1f, 1f, 1f,0);
        color.a = 0;
        while (true)
        {
            while (color.a < 1)
            {
                color.a += Time.deltaTime * 2;
                color.a = Mathf.Clamp01(color.a);
                TouchToStart.color = color;
                yield return null;
            }

            while (color.a > 0)
            {
                color.a -= Time.deltaTime * 2;
                color.a = Mathf.Clamp01(color.a);
                TouchToStart.color = color;
                yield return null;
            }
            yield return null;
        }
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
