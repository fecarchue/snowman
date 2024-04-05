using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Controls;

public class CutSceneManager : MonoBehaviour
{
    private int playnumber, touch;
    private float power;
    private Image CutScene;
    public GameObject SkipButton, ChallengePanel,CutSceneObject, TouchPanel, CutSceneObj;
    public Sprite[] DevilScenes;
    public Sprite[] FightScenes;

    private void Start()
    {
        Debug.Log("실행");
        touch = 0;

        power = DataManager.Instance.CurrentPower;
        CutScene = CutSceneObj.GetComponent<Image>();
        CutSceneObject.SetActive(false);
        ChallengePanel.SetActive(false);
        
        if(PlayerPrefs.GetString("IsFirst") != "NotFirst")
        {
            StartCoroutine(FirstFight());
            PlayerPrefs.SetString("IsFirst", "NotFirst");
            PlayerPrefs.Save();
        }
        else
        {
            ChallengePanel.SetActive(true);
        }
    }

    public void ChallengeButton()
    {
        ChallengePanel.SetActive(false);

        if (PlayerPrefs.HasKey("PlayNumber"))
        {
            // 변수가 저장되어 있다면 값을 불러옴
            playnumber = PlayerPrefs.GetInt("PlayNumber");

            if (playnumber >= 1 && playnumber <= 3)
            {
                StartCoroutine(Challenge2to4());
            }
            else
            {
                StartCoroutine(Challenge5more());
            }
            playnumber++;
        }
        else
        {
            // 변수가 저장되어 있지 않다면 처음 실행 간주
            playnumber = 1;

            StartCoroutine(FirstChallenge());
        }
        
        PlayerPrefs.SetInt("PlayNumber", playnumber);
        PlayerPrefs.Save();
    }

    IEnumerator FirstFight()
    {
        CutSceneObject.SetActive(true);
        for (int i = 0; i < 7; i++)
        {
            CutScene.sprite = FightScenes[i];
            yield return new WaitForSeconds(2.0f);
        }
        CutSceneObject.SetActive(false);
        ChallengePanel.SetActive(true);
    }

    IEnumerator FirstChallenge()
    {
        CutSceneObject.SetActive(true);
        for (int i = 0; i < 31; i++)
        {
            CutScene.sprite = DevilScenes[i];
            yield return new WaitForSeconds(2.0f);
        }

        if (power > 10000)
        {
            StartCoroutine(DevilScene_Win());
        }
        else
        {
            StartCoroutine(DevilScene_Lose());
        }
    }

    IEnumerator Challenge2to4()
    {
        CutSceneObject.SetActive(true);
        CutScene.sprite = DevilScenes[8];
        yield return new WaitForSeconds(2.0f);
        CutScene.sprite = DevilScenes[14];
        for (int i = 21; i < 31; i++)
        {
            CutScene.sprite = DevilScenes[i];
            yield return new WaitForSeconds(1.0f);
        }

        if(power > 10000)
        {
            StartCoroutine(DevilScene_Win());
        }
        else
        {
            StartCoroutine(DevilScene_Lose());
        }
        
    }

    IEnumerator Challenge5more()
    {
        CutSceneObject.SetActive(true);
        for (int i = 21; i < 31; i++)
        {
            CutScene.sprite = DevilScenes[i];
            yield return new WaitForSeconds(2.0f);
        }

        if (power > 10000)
        {
            StartCoroutine(DevilScene_Win());
        }
        else
        {
            StartCoroutine(DevilScene_Lose());
        }
    }

    IEnumerator DevilScene_Win()
    {
        CutScene.sprite = DevilScenes[31];
        yield return new WaitForSeconds(2.0f);
        CutScene.sprite = DevilScenes[32];
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator DevilScene_Lose()
    {
        CutScene.sprite = DevilScenes[31];
        yield return new WaitForSeconds(2.0f);
        CutScene.sprite = DevilScenes[33];
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Skip()
    {
        touch++;
        if(touch == 1)
        {
            TouchPanel.SetActive(false);
            SkipButton.SetActive(true);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(DevilScene_Win());
        }
    }

    IEnumerator FadeIn(float fadeInTime)
    {
        Color color = CutScene.color;
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
