using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
