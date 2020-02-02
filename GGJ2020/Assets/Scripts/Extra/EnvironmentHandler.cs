using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EnvironmentHandler : MonoBehaviour
{
    StudioEventEmitter studioEventEmitter;

    private void Awake()
    {
        studioEventEmitter = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        if(SceneRegulator.Instance.GetCurrentSceneIndex() == 0) { studioEventEmitter.Params[0].Value = 0; }
        else { studioEventEmitter.Params[0].Value = 1; }

        studioEventEmitter.Play();
    }

}
