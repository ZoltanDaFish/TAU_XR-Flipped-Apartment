using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ButtonColliderType { Press, Hover }

// switch to work with unity events
public class ToucherDetector : MonoBehaviour
{
    public UnityEvent<Transform> TriggerEnter;
    public UnityEvent<Transform> TriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Toucher")) return;

        TriggerEnter.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Toucher")) return;

        TriggerExit.Invoke(other.transform);
    }
}
