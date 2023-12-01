using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TrailRenderer trailRenderer;
    public GameObject player;
    public float playerSize;

    void Start()
    {
        // Trail Renderer 컴포넌트 참조
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        // 캐릭터의 크기에 따라 Trail의 너비 동적으로 조절
        playerSize = player.GetComponent<PlayerProperties>().playerSize;
        trailRenderer.widthMultiplier = playerSize + 1;
    }
}
