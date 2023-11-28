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
        trailRenderer.widthMultiplier =  (2.5f);
        playerScale = player.GetComponent<Player>().newScale;
        if (playerScale < 1)
        {
            trailRenderer.widthMultiplier = (1f);
        }
        else if (playerScale < 1.6)                                                         //이 1.6, 1.8, 2, 2.2 이런값들이 난이도임. snowscale을 언제 다음레벨로 올려줄지 정하게 저 입구컷을 낮춰주거나 올리면됨
        {
            trailRenderer.widthMultiplier = (2f);
        }
        else if (playerScale < 1.8)
        {
            trailRenderer.widthMultiplier = (3f);
        }
        else if (playerScale < 2)
        {
            trailRenderer.widthMultiplier = (4f);
        }
        else if (playerScale < 2.2)
        {
            trailRenderer.widthMultiplier = (5f);
        }
        else if (playerScale < 2.4)
        {
            trailRenderer.widthMultiplier = (6f);
        }
        else if (playerScale < 2.6)
        {
            trailRenderer.widthMultiplier = (7f);
        }
        else
        {
            trailRenderer.widthMultiplier = (8f);
        }
    }
}
