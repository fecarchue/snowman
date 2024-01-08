using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [HideInInspector] public bool isGround, isDevil, isSlide, isIce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground") isGround = true;
        if (other.gameObject.tag == "Devil") isDevil = true;
        if (other.gameObject.tag == "Slide") isSlide = true;
        if (other.gameObject.tag == "Ice") isIce = true;
        if (other.gameObject.tag == "Goal") gameObject.GetComponentInParent<PlayerData>().Goal();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground") isGround = false;
        if (other.gameObject.tag == "Devil") isDevil = false;
        if (other.gameObject.tag == "Slide") isSlide = false;
        if (other.gameObject.tag == "Ice") isIce = false;
    }
}
