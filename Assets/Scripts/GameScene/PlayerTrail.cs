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
        // Trail Renderer ������Ʈ ����
        trailRenderer = GetComponent<TrailRenderer>();
        StartCoroutine(MakeTrail());
    }

    private IEnumerator MakeTrail()
    {
        while (true)
        {
            // ĳ������ ũ�⿡ ���� Trail�� �ʺ� �������� ����
            playerScale = GetComponent<PlayerData>().snowScale;
            trailRenderer.widthMultiplier = playerScale * trailSize;
            yield return null;
        }
    }
}
