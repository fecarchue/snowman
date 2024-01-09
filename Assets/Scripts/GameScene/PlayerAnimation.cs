using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    public float angryHealth = 7f;
    private int playerSize = 0;
    private int currentSize;
    private float hp;
    private int starCount;
    private int state = 0;
    private float clipTime;
    private string[,] animName = new string[11,3] { { "normal12", "angry12", "" }, { "normal16", "angry16", "" }, { "normal20", "angry20", "" }, { "normal26", "angry26", "" }, { "normal36", "angry36", "" }, { "normal48", "angry48", "" }, { "normal64", "angry64", "" }, { "normal86", "angry86", "" }, { "normal116", "angry116", "star116" }, { "normal156", "angry156", "star156" }, { "normal202", "angry202", "star202" } };
    
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(InGameAnimation());
    }

    private IEnumerator InGameAnimation()
    {
        while (true)
        {
            currentSize = GetComponent<PlayerData>().playerSize;
            hp = GetComponent<PlayerData>().playerData[1];
            starCount = GetComponent<PlayerData>().starCount;

            anim.speed = anim.GetCurrentAnimatorStateInfo(0).length;
            clipTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f;

            if (playerSize != currentSize)
            {
                playerSize = currentSize;
                anim.Play(animName[playerSize, state], -1, clipTime);
            }
            else if (state == 0 && hp < angryHealth)
            {
                state = 1;
                anim.Play(animName[playerSize, 1], -1, clipTime);
            }
            else if (state == 1 && hp > angryHealth)
            {
                state = 0;
                anim.Play(animName[playerSize, 0], -1, clipTime);
            }
            else if (state != 2 && starCount == 7)
            {
                state = 2;
                anim.Play(animName[playerSize <= 8 ? 8 : playerSize, 2], -1, clipTime);
            }
            yield return null;
        }
    }

    private IEnumerator TurnNormalFace()
    {
        while (true)
        {
            clipTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f;

            if (0.3f < clipTime && clipTime < 0.7f)
            {
                anim.Play(animName[playerSize, 0], -1, clipTime);
                break;
            }

            yield return null;
        }
    }

    public void SubGoal()
    {
        StopAllCoroutines();
        StartCoroutine(TurnNormalFace());
    }

    public void Goal()
    {
        anim.speed = 0;
        StopAllCoroutines();
        //StartCoroutine(GoalAnimation());
    }

    public void Fail()
    {
        anim.speed = 0;
        StopAllCoroutines();
        //StartCoroutine(FailAnimation());
    }
}
