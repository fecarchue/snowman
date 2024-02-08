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
        // Trail Renderer ������Ʈ ����
        trailRenderer = GetComponent<TrailRenderer>();
        StartCoroutine(MakeTrail());
    }

    private IEnumerator MakeTrail()
    {
        while (true)
        {
            isShrinked = GetComponent<PlayerData>().isShrinked;
            shrinkedScale = GetComponent<PlayerData>().shrinkedScale;
            if (isShrinked)  // ĳ���Ͱ� ��ҵǸ� Trail�� �ʺ� ����
            {
                trailRenderer.widthMultiplier = shrinkedScale * trailSize;
                yield return null;
            }
            else
            {
                // ĳ������ ũ�⿡ ���� Trail�� �ʺ� �������� ����
                playerScale = GetComponent<PlayerData>().snowScale;
                trailRenderer.widthMultiplier = playerScale * trailSize;
                yield return null;
            }
        }
    }
}
