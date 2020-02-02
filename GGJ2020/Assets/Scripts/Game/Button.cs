using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public bool buttonState = false;
    public UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Slime>())
        {
            OnTrigger?.Invoke();
        }
    }
}
