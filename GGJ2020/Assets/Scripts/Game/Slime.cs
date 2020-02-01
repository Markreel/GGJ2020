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

    public int SlimeSize = 1;


    private void Awake()
    {
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
        SlimeManager.Instance.CheckWhichSlimeIsTheBiggest();
    }

    public void ShootBlob()
    {
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
            MergeWithOtherSlime(_slime);
        }
    }
}
