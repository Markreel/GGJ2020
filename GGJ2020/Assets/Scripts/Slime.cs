using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] float scaleFactor;
    public bool PrimeSlime = false;

    public int SlimeSize = 1;

    private void Awake()
    {
        UpdateSlimeScale();
    }

    public void AddBlobToSlime()
    {
        SlimeSize++;
        UpdateSlimeScale();
        SlimeManager.Instance.CheckWhichSlimeIsTheBiggest();
    }

    public void UpdateSlimeScale()
    {
        transform.localScale = Vector3.one * SlimeSize * scaleFactor;
    }

    public void MergeWithOtherSlime()
    {

    }
}
