using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    private float playerScale;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetFloat("snowscale", 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerScale = player.GetComponent<Player>().newScale;
        if (playerScale < 1)
        {
            anim.SetFloat("snowscale", 0);
        }
        else if (playerScale < 1.6)                                                         //이 1.6, 1.8, 2, 2.2 이런값들이 난이도임. snowscale을 언제 다음레벨로 올려줄지 정하게 저 입구컷을 낮춰주거나 올리면됨
        {
            anim.SetFloat("snowscale", 1);
        }
        else if (playerScale < 1.8)
        {
            anim.SetFloat("snowscale", 2);
        }
        else if (playerScale < 2)
        {
            anim.SetFloat("snowscale", 3);
        }
        else if (playerScale < 2.2)
        {
            anim.SetFloat("snowscale", 4);
        }
        else if (playerScale < 2.4)
        {
            anim.SetFloat("snowscale", 5);
        }
        else if (playerScale < 2.6)
        {
            anim.SetFloat("snowscale", 6);
        }
        else
        {
            anim.SetFloat("snowscale", 7);
        }
    }
}
