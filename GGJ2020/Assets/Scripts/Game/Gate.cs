using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenGate()
    {
        AudioManager.Instance.CreateAudioPart(transform.position, AudioManager.Instance.DoorFallClip, 0.25f);
        anim.SetTrigger("Open");
    }
}
