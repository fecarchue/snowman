using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [HideInInspector] public bool isGround, isSlide, isIce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground") isGround = true;
        if (other.gameObject.tag == "Slide") isSlide = true;
        if (other.gameObject.tag == "Ice") isIce = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground") isGround = false;
        if (other.gameObject.tag == "Slide") isSlide = false;
        if (other.gameObject.tag == "Ice") isIce = false;
    }
}
