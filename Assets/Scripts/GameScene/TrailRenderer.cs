using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TrailRenderer trailRenderer;
    public float playerScale;
    public float trailSize = 0.8f;

    void Start()
    {
        // Trail Renderer ������Ʈ ����
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        // ĳ������ ũ�⿡ ���� Trail�� �ʺ� �������� ����
        playerScale = GetComponent<PlayerData>().cameraScale;
        trailRenderer.widthMultiplier = playerScale * trailSize;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            trailRenderer.endColor = new Color(0.5f, 0.4f, 0.25f, 0.6f);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            trailRenderer.endColor = new Color(0.5f, 0.5f, 0.5f, 0.6f);
        }
    }
}
