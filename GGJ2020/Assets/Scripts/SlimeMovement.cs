using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlimeMovement : Movement
{
    protected override Vector3 Move()
    {
        Vector3 vel = base.Move();

        Quaternion WantedRotation = Quaternion.LookRotation(vel);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, WantedRotation, Time.deltaTime * 5);
        return Vector3.zero;
    }

}
