using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public enum ClipType { Effect, Music, UI, Ambience }

    [SerializeField] private AudioSource effectSource;

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

    public void PlayClip(AudioClip _clip)
    {
        effectSource.PlayOneShot(_clip);
    }

    public void PlayClip(AudioClip _clip, float _volume = 1, ClipType _type = ClipType.Effect)
    {
        switch (_type)
        {
            default:
            case ClipType.Effect:
                effectSource.PlayOneShot(_clip, _volume);
                break;
        }
    }

    public void PlayRandomClip(AudioClip[] _clips, float _volume = 1, ClipType _type = ClipType.Effect)
    {
        if (_clips == null || _clips.Length == 0) return;
        AudioClip _rClip = _clips[Random.Range(0, _clips.Length)];

        switch (_type)
        {
            default:
            case ClipType.Effect:
                effectSource.PlayOneShot(_rClip, _volume);
                break;
        }
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
