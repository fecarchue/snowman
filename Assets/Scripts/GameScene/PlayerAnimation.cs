using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    private float playerScale;
    private int playerSize;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSize = player.GetComponent<PlayerProperties>().playerSize;
        anim.SetFloat("snowScale", playerSize); //0ÀÌ default
    }
}
