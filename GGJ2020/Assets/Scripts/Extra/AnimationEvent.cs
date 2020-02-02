using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] UnityEvent[] unityEvent;

    public void InvokeAnimationEvent(int _index)
    {
        unityEvent[_index]?.Invoke();
    }
}
