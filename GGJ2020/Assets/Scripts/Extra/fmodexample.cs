using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fmodexample : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string LanaGra;
    FMOD.Studio.EventInstance audioClip;

    void Awake()
    {
        audioClip = FMODUnity.RuntimeManager.CreateInstance(LanaGra);
        audioClip.start();

        StartCoroutine(IETest());
    }

    void Update()
    {


        FMODUnity.RuntimeManager.AttachInstanceToGameObject(audioClip, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    private IEnumerator IETest()
    {
        yield return new WaitForSeconds(3);

        audioClip.start();

        StartCoroutine(IETest());

        yield return null;
    }
}

