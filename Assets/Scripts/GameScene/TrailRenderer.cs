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
        trailRenderer.widthMultiplier =  (2.5f);
        playerScale = player.GetComponent<Player>().newScale;
        if (playerScale < 1)
        {
            trailRenderer.widthMultiplier = (1f);
        }
        else if (playerScale < 1.6)                                                         //�� 1.6, 1.8, 2, 2.2 �̷������� ���̵���. snowscale�� ���� ���������� �÷����� ���ϰ� �� �Ա����� �����ְų� �ø����
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
