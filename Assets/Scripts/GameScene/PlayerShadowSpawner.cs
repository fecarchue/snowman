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
        // �÷��̾�� playershadow�� SpriteRenderer ������Ʈ�� �����ɴϴ�.
        playerSpriteRenderer = targetPlayer.GetComponent<SpriteRenderer>();
        shadowSpriteRenderer = GetComponent<SpriteRenderer>();

        if (playerSpriteRenderer == null || shadowSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on player or playershadow.");
        }
    }

    void Update()
    {
        // �÷��̾��� ��ġ�� �����ͼ� �������� ��ġ�� ������Ʈ�մϴ�.
        if (targetPlayer != null)
        {
            transform.position = targetPlayer.position;
            transform.rotation = targetPlayer.rotation;

            // �÷��̾��� sprite�� �����ͼ� playershadow�� sprite�� ������Ʈ�մϴ�.
            shadowSpriteRenderer.sprite = playerSpriteRenderer.sprite;

        }
    }
}
