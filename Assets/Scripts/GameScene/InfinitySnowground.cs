using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitySnowground : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        transform.position += player.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "Map Collider") return;
        Vector3 playerP = collision.transform.parent.position;
        Vector3 tileP = transform.position;

        float diffX = playerP.x - tileP.x;
        float diffY = playerP.y - tileP.y;
        float dirX = diffX > 0 ? 1 : -1;
        float dirY = diffY > 0 ? 1 : -1;

        if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
        {
            transform.Translate(Vector3.right * dirX * 160);
        }
        else transform.Translate(Vector3.up * dirY * 160);
    }
}
