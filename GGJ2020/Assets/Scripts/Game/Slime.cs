using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Slime : MonoBehaviour
{

    [SerializeField] LayerMask slimeMask;
    [SerializeField] float maxSlimeDetectionRange = 3;

    [SerializeField] float scaleFactor;
    public bool PrimeSlime = false;
    public bool PickupSlime = false;

    public int SlimeSize = 1;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        UpdateSlimeScale();
    }

    private void Update()
    {
        //CheckForNearbySlimes();
    }

    public void AddBlobToSlime()
    {
        SlimeSize++;
        UpdateSlimeScale();
        AnimateSpawn();
        SlimeManager.Instance.CheckWhichSlimeIsTheBiggest();
    }

    public void ShootBlob()
    {
        SlimeManager.Instance.ShootBlob();

        SlimeSize--;
        UpdateSlimeScale();
        SlimeManager.Instance.CheckWhichSlimeIsTheBiggest();
    }

    public void UpdateSlimeScale()
    {
        transform.localScale = Vector3.one * SlimeSize * scaleFactor;
    }

    public void MergeWithOtherSlime(Slime _otherSlime)
    {
        SlimeSize += _otherSlime.SlimeSize;
        SlimeManager.Instance.RemoveSlime(_otherSlime);
        UpdateSlimeScale();
        AnimateSpawn();
    }

    public void AnimateShoot()
    {
        anim.SetTrigger("Shoot");
    }

    public void AnimateSpawn()
    {
        anim.SetTrigger("Spawn");
    }

    public void ToggleWalk(bool _value)
    {
        anim.SetBool("Walking", _value);
    }

    private void CheckForNearbySlimes()
    {
        if (!PrimeSlime)
        {
            RaycastHit[] _sphereHits = Physics.SphereCastAll(transform.position, maxSlimeDetectionRange, transform.forward, slimeMask.value);

            foreach (var _hit in _sphereHits)
            {
                _hit.transform.Translate(transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Slime _slime = collision.transform.GetComponent<Slime>();

        if (_slime != null && !_slime.PrimeSlime)
        {
            if (_slime.PickupSlime) { AudioManager.Instance.CreateAudioPart(transform.position, 
                AudioManager.Instance.SpecialPickupClips[Random.Range(0, AudioManager.Instance.SpecialPickupClips.Length)], 0.25f); }
            MergeWithOtherSlime(_slime);
        }
    }
}
