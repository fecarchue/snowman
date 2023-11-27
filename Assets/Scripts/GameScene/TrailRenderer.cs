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
        // Trail Renderer ������Ʈ ����
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        // ĳ������ ũ�⿡ ���� Trail�� �ʺ� �������� ����
        playerScale = player.GetComponent<Player>().newScale;
        trailRenderer.widthMultiplier = playerScale * (1.5f);
    }

}
