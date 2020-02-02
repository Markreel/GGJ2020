using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelfDestruct : MonoBehaviour
{
    public void SelfDestruct(float _delay)
    {
        StartCoroutine(IESelfDestruct(_delay));
    }

    private IEnumerator IESelfDestruct(float _delay)
    {
        yield return new WaitForSeconds(_delay);

        Destroy(gameObject);

        yield return null;
    }
}
