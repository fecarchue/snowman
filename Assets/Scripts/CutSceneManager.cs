using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Controls;

public class CutSceneManager : MonoBehaviour
{
    private int playnumber, power;
    private Image CutScenes;
    public Sprite[] DevilScenes;
    public Sprite[] DevilScenes23;
    public SpriteRenderer[] Devil;
    public Sprite[] FightScenes;

    private void Awake()
    {
        power = DataManager.Instance.CurrentPower;
        CutScenes = GetComponent<Image>();
        if (PlayerPrefs.HasKey("PlayNumber"))
        {
            // ������ ����Ǿ� �ִٸ� ���� �ҷ���
            playnumber = PlayerPrefs.GetInt("PlayNumber");
            
            if(playnumber >= 1 && playnumber <=3)
            {
                StartCoroutine(Play2to4());
            }
            else
            {
                StartCoroutine(Playmore5());
            }

            playnumber++;
            PlayerPrefs.SetInt("PlayNumber", playnumber);
            PlayerPrefs.Save();
        }
        else
        {
            // ������ ����Ǿ� ���� �ʴٸ� ó�� ����
            playnumber = 1;

            StartCoroutine(FirstPlay());
            PlayerPrefs.SetInt("PlayNumber", playnumber);
            PlayerPrefs.Save();
        }
    }

    IEnumerator FirstPlay()
    {
        for (int i = 0; i < 7; i++)
        {
            CutScenes.sprite = FightScenes[i];
            yield return new WaitForSeconds(2.0f);
        }

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

    IEnumerator Play2to4()
    {
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

    IEnumerator Playmore5()
    {
        for(int i = 21; i < 31; i++)
        {
            StartCoroutine(FadeImage(1.5f,0.5f, DevilScenes[i]));
            //CutScenes.sprite = DevilScenes[i];
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

    IEnumerator FadeImage(float fadeInTime, float fadeOutTime, Sprite TargetImage)
    {
        if (fadeInTime > 0)
        {
            Color color = CutScenes.color;

            // ���̵� �ƿ�
            while (color.a > 0)
            {
                color.a -= Time.deltaTime / fadeInTime;
                color.a = Mathf.Clamp01(color.a);
                CutScenes.color = color;
                yield return null;
            }

            // �̹��� ����
            CutScenes.sprite = TargetImage;

            // ���̵� ��
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
            // ���̵� ��/�ƿ� �ð��� 0 ������ ��� ���̵� ���� �̹��� ����
            CutScenes.sprite = TargetImage;
        }
    }
}
