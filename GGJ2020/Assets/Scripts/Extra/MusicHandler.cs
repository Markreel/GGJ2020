using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicHandler : MonoBehaviour
{
    StudioEventEmitter studioEventEmitter;
    [SerializeField] int musicIndex;

    private void Awake()
    {
        studioEventEmitter = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        ChangeMusic();
        studioEventEmitter.Play();
    }

    public void ChangeMusic()
    {
        studioEventEmitter.Params[0].Value = musicIndex;
    }
}
