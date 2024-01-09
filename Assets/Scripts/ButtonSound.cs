using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioSource ClickSound, NonClickSound;
    private bool Press,NonPress;
    
    public void Awake()
    {
        Press = true;
        NonPress = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!NonPress)
        {
            NonClickSound.Stop();
            NonPress = true;
        }

        if (Press) ClickSound.Play();


        Press = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!Press)
        {
            ClickSound.Stop();
            Press = true;
        }

        if (NonPress) NonClickSound.Play();

        NonPress = false;
    }
}