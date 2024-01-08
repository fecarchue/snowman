using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCorrection : MonoBehaviour
{
    private float playerScale;
    void Start()
    {
        StartCoroutine(correctCollider());
    }

    private IEnumerator correctCollider()
    {
        while (true)
        {
            playerScale = transform.parent.localScale.x;
            GetComponent<BoxCollider2D>().size = new Vector2(80f / playerScale, 80f / playerScale);

            yield return null;
        }
    }
}
