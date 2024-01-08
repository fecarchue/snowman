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

            if (playerSize != currentSize)
            {
                playerSize = currentSize;
                anim.Play(animName[playerSize, state], -1, anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else if (state == 0 && hp < angryHealth)
            {
                state = 1;
                anim.Play(animName[playerSize, 1], -1, anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else if (state == 1 && hp > angryHealth)
            {
                state = 0;
                anim.Play(animName[playerSize, 0], -1, anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else if (state != 2 && starCount == 7)
            {
                state = 2;
                anim.Play(animName[playerSize <= 8 ? 8 : playerSize, 2], -1, anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            yield return null;
        }
    }
}
