using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private float x1, x2;
    [HideInInspector] public float dist;
    public float distRate = 1f;

    void Start()
    {
        StartCoroutine(GetSwipe());
    }

    private IEnumerator GetSwipe()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                x1 = Input.mousePosition.x;
            }
            if (Input.GetMouseButton(0))
            {
                x2 = Input.mousePosition.x;
            }
            if (Input.GetMouseButtonUp(0))
            {
                x2 = x1;
            }
            dist = (x2 - x1) * distRate;
            yield return null;
        }
    }

}
