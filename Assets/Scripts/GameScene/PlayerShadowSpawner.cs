using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public Transform targetPlayer;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer shadowSpriteRenderer;

    void Start()
    {
        // 플레이어와 playershadow의 SpriteRenderer 컴포넌트를 가져옵니다.
        playerSpriteRenderer = targetPlayer.GetComponent<SpriteRenderer>();
        shadowSpriteRenderer = GetComponent<SpriteRenderer>();

        if (playerSpriteRenderer == null || shadowSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on player or playershadow.");
        }
    }

    void Update()
    {
        // 플레이어의 위치를 가져와서 쉐도우의 위치를 업데이트합니다.
        if (targetPlayer != null)
        {
            transform.position = targetPlayer.position;
            transform.rotation = targetPlayer.rotation;

            // 플레이어의 sprite를 가져와서 playershadow의 sprite를 업데이트합니다.
            shadowSpriteRenderer.sprite = playerSpriteRenderer.sprite;

        }
    }
}
