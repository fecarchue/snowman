using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public Transform player;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer shadowSpriteRenderer;

    void Start()
    {
        shadowSpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(UpdateShadow());
    }

    private IEnumerator UpdateShadow()
    {
        while (true)
        {
            playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

            shadowSpriteRenderer.sprite = playerSpriteRenderer.sprite;
            yield return null;
        }
    }
}
