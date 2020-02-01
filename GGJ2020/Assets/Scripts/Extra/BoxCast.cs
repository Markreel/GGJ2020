using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoxCast
{
    public Vector3 center;
    public Vector3 halfExtents;
    public Vector3 direction;

    public BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction)
    {
        this.center = center;
        this.halfExtents = halfExtents;
        this.direction = direction;
    }
}
