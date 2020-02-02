using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    [SerializeField] bool isSpecialPickup = false;

    private void OnTriggerEnter(Collider _other)
    {
        Slime _slime = _other.gameObject.GetComponent<Slime>();

        if (_slime != null)
        {
            if(!_slime.PrimeSlime)
            {
                _slime.AddBlobToSlime();
            }

            else { return; }
        }

        else
        {
            SlimeManager.Instance.CreateNewSlime(transform.position + Vector3.up / 10, isSpecialPickup);
            //SlimeManager.Instance.CheckWhichSlimeIsTheBiggest(true);
        }

        Destroy(gameObject);
    }
}
