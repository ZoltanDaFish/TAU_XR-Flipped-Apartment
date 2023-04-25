using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchPoint : MonoBehaviour
{
    public HandType HandT;

    OVRSkeleton ovrSkeleton;

    float pinchSignLerpSpeed = 12f;

    Material pinchMat;
    public Color colorFull;
    public Color colorNone;
    float targetAlpha = 0;

    int indexI = 20, thumbI = 19;
    float pinchStrength;

    public void Init(OVRSkeleton skeleton)
    {
        ovrSkeleton = skeleton;
        pinchMat = GetComponent<MeshRenderer>().material;
        colorFull = pinchMat.color;
    }

    public void UpdatePinchPoint(float pinchStrength)
    {
        this.pinchStrength = pinchStrength;
        transform.position = GetPinchPointPosition();

        SignPinchStrength(pinchStrength);

    }

    private Vector3 GetPinchPointPosition()
    {
        Vector3 indexPos = ovrSkeleton.Bones[indexI].Transform.position;
        Vector3 thumbPos = ovrSkeleton.Bones[thumbI].Transform.position;
        Vector3 thumbToIndexVec = thumbPos + (indexPos - thumbPos);

        return Vector3.Lerp(thumbPos, indexPos, .5f);
    }

    private void SignPinchStrength(float pinchStr)
    {
        targetAlpha = Mathf.Lerp(targetAlpha, pinchStr, pinchSignLerpSpeed * Time.deltaTime);
        
        pinchMat.color = Color.Lerp(colorNone, colorFull, targetAlpha);
    }

    public bool IsPinching()
    {
        return pinchStrength == 1f;
    }
}
