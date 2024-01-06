using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    // Start is called before the first frame update
    public TrailRenderer trailRenderer;
    private float playerScale;
    public float trailSize = 8f;

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
            // 캐릭터의 크기에 따라 Trail의 너비 동적으로 조절
            playerScale = GetComponent<PlayerData>().snowScale;
            trailRenderer.widthMultiplier = playerScale * trailSize;
            yield return null;
        }
    }
}
