using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip DoorUpClip;
    public AudioClip DoorFallClip;
    public AudioClip PresurePlateHitClip;

    public AudioClip[] SlimeHitClips;

    public AudioClip[] SpecialPickupClips;


    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;

    }

    public void CreateAudioPart(Transform _parent , AudioClip _clip)
    {
        GameObject _audioObj = new GameObject();
        _audioObj.transform.parent = _parent;
        _audioObj.transform.localPosition = Vector3.zero;

        AudioSource _source = _audioObj.AddComponent<AudioSource>();
        _source.clip = _clip;

        AudioSelfDestruct _bom =_audioObj.AddComponent<AudioSelfDestruct>();
        _bom.SelfDestruct(_clip.length);

        _source.Play();
    }

    public void CreateAudioPart(Vector3 _position, AudioClip _clip, float _volume = 1)
    {
        GameObject _audioObj = new GameObject();
        _audioObj.transform.position = _position;

        AudioSource _source = _audioObj.AddComponent<AudioSource>();
        _source.volume = _volume;
        _source.clip = _clip;

        AudioSelfDestruct _bom = _audioObj.AddComponent<AudioSelfDestruct>();
        _bom.SelfDestruct(_clip.length);

        _source.Play();
    }
}
