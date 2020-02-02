using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GodVoice : MonoBehaviour
{
    StudioEventEmitter studioEventEmitter;

    private void Awake()
    {
        studioEventEmitter = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        studioEventEmitter.Play();
    }

    public void GoingToGod()
    {
        Debug.Log("TEST");
        studioEventEmitter.Stop();
        studioEventEmitter.Params[0].Value = 1;
        studioEventEmitter.Play();
    }
}
