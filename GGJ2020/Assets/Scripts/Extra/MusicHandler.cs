using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicHandler : MonoBehaviour
{
    StudioEventEmitter studioEventEmitter;

    private void Awake()
    {
        studioEventEmitter = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        ChangeMusic(SceneRegulator.Instance.GetCurrentSceneIndex());
        studioEventEmitter.Play();
    }

    public void ChangeMusic(int _index)
    {
        studioEventEmitter.Params[0].Value = _index;
    }
}
