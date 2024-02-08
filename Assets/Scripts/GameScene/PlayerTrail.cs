using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    // Start is called before the first frame update
    public TrailRenderer trailRenderer;
    private float playerScale;
    public float trailSize = 8f;

    private bool isShrinked = false;
    private float shrinkedScale;

    void Start()
    {
        // Trail Renderer 컴포넌트 참조
        trailRenderer = GetComponent<TrailRenderer>();
        StartCoroutine(MakeTrail());
    }

    private IEnumerator MakeTrail()
    {
        while (true)
        {
            isShrinked = GetComponent<PlayerData>().isShrinked;
            shrinkedScale = GetComponent<PlayerData>().shrinkedScale;
            if (isShrinked)  // 캐릭터가 축소되면 Trail의 너비를 줄임
            {
                trailRenderer.widthMultiplier = shrinkedScale * trailSize;
                yield return null;
            }
            else
            {
                // 캐릭터의 크기에 따라 Trail의 너비 동적으로 조절
                playerScale = GetComponent<PlayerData>().snowScale;
                trailRenderer.widthMultiplier = playerScale * trailSize;
                yield return null;
            }
        }
    }
}
