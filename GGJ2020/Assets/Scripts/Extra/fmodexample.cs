using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fmodexample : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string LanaGra2;
    FMOD.Studio.EventInstance ShootAudio;

    [FMODUnity.EventRef]
    public string LanaGra;
    FMOD.Studio.EventInstance PickupAudio;

    void Awake()
    {
        PickupAudio = FMODUnity.RuntimeManager.CreateInstance(LanaGra);
        ShootAudio = FMODUnity.RuntimeManager.CreateInstance(LanaGra2);

        //StartCoroutine(IETest());
    }

    public void PlayPickupClip()
    {
        PickupAudio.start();
    }

    public void PlayShootClip()
    {
        ShootAudio.start();
    }

    //void Update()
    //{
    //    FMODUnity.RuntimeManager.AttachInstanceToGameObject(audioClip, GetComponent<Transform>(), GetComponent<Rigidbody>());
    //}

    //private IEnumerator IETest()
    //{
    //    yield return new WaitForSeconds(3);

    //    audioClip.start();

    //    StartCoroutine(IETest());

    //    yield return null;
    //}
}

