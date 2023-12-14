using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    private int playerSize;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSize = GetComponent<PlayerData>().playerSize;
        anim.SetInteger("playerSize", playerSize); //0ÀÌ default
    }
}
