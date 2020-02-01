using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnFadeEnd : MonoBehaviour
{
    public UnityEvent OnAnimationEnd;

    public void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}
