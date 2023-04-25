using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SliderSignifier{ HoverIn, HoverOut, ButtonPress, ButtonRelease, BeforeRating, AfterRating }
public class TAUXRSliderSignifier : MonoBehaviour
{
    [SerializeField] VRButtonTouch sliderButton;


    public UnityEvent IdlePreRating;
    public UnityEvent DuringRating;
    public UnityEvent IdlePostRating;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Sign(SliderSignifier signState)
    {
        switch (signState)
        {
            case SliderSignifier.HoverIn:
                break;
            case SliderSignifier.HoverOut:
                break;
            case SliderSignifier.ButtonPress:
                sliderButton.InvokeButtonEvent(ButtonEvent.Pressed, ButtonColliderResponse.Internal);
                break;
            case SliderSignifier.ButtonRelease:
                sliderButton.InvokeButtonEvent(ButtonEvent.Released, ButtonColliderResponse.Internal);
                break;
            case SliderSignifier.BeforeRating:
                break;
            case SliderSignifier.AfterRating:
                break;

            default: return;
        }
    }
}
