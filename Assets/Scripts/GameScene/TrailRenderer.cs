using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TrailRenderer trailRenderer;
    public GameObject player;
    public float playerSize;

    void Start()
    {
        // Trail Renderer ������Ʈ ����
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        // ĳ������ ũ�⿡ ���� Trail�� �ʺ� �������� ����
        playerSize = player.GetComponent<PlayerProperties>().playerSize;
        trailRenderer.widthMultiplier = playerSize + 1;
    }
}
