using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TrailRenderer trailRenderer;
    public GameObject player;
    public float playerScale;

    void Start()
    {
        // Trail Renderer 컴포넌트 참조
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        // 캐릭터의 크기에 따라 Trail의 너비 동적으로 조절
        playerScale = player.GetComponent<Player>().newScale;
        trailRenderer.widthMultiplier = playerScale * (1.5f);
    }

}
