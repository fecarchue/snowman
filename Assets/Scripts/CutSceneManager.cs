using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Controls;

public class CutSceneManager : MonoBehaviour
{
    private int playnumber, power, touch;
    private Image CutScenes;
    public GameObject SkipButton, ChallengePanel,CutSceneObject, TouchPanel, CutSceneObj;
    public Sprite[] DevilScenes;
    public Sprite[] DevilScenes23;
    public Sprite[] FightScenes;

    private void Start()
    {
        Debug.Log("실행");
        touch = 0;
        //데이터 초기화 (마지막 작업할 때 꼭 지우기)
        PlayerPrefs.DeleteAll();

        power = DataManager.Instance.CurrentPower;
        CutScenes = CutSceneObj.GetComponent<Image>();
        CutSceneObject.SetActive(false);
        ChallengePanel.SetActive(false);
        
        if(PlayerPrefs.GetString("IsFirst") != "false")
        {
            StartCoroutine(FirstFight());
            PlayerPrefs.SetString("IsFirst", "false");
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
            CutScenes.sprite = FightScenes[i];
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
            CutScenes.sprite = DevilScenes[i];
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
        CutScenes.sprite = DevilScenes[8];
        yield return new WaitForSeconds(2.0f);
        CutScenes.sprite = DevilScenes[14];
        for (int i = 21; i < 31; i++)
        {
            CutScenes.sprite = DevilScenes[i];
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
            StartCoroutine(FadeEffect(1.5f,0.5f, DevilScenes[i]));
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
        CutScenes.sprite = DevilScenes[31];
        yield return new WaitForSeconds(2.0f);
        CutScenes.sprite = DevilScenes[32];
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("StartScene");
    }
    IEnumerator DevilScene_Lose()
    {
        CutScenes.sprite = DevilScenes[31];
        yield return new WaitForSeconds(2.0f);
        CutScenes.sprite = DevilScenes[33];
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("StartScene");
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

    IEnumerator FadeEffect(float fadeInTime, float fadeOutTime, Sprite TargetImage)
    {
        if (fadeInTime > 0)
        {
            Color color = CutScenes.color;

            // 페이드 아웃
            while (color.a > 0)
            {
                color.a -= Time.deltaTime / fadeInTime;
                color.a = Mathf.Clamp01(color.a);
                CutScenes.color = color;
                yield return null;
            }

            // 이미지 변경
            CutScenes.sprite = TargetImage;

            // 페이드 인
            while (color.a < 1)
            {
                color.a += Time.deltaTime / fadeOutTime;
                color.a = Mathf.Clamp01(color.a);
                CutScenes.color = color;
                yield return null;
            }
        }
        else
        {
            // 페이드 인/아웃 시간이 0 이하인 경우 페이드 없이 이미지 변경
            CutScenes.sprite = TargetImage;
        }
    }
}
