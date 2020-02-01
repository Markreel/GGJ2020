using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] int scaleFactor;

    public int SlimeSize = 1;

    public void ScaleUp()
    {
        transform.localScale = Vector3.one * SlimeSize * scaleFactor;
    }
}
