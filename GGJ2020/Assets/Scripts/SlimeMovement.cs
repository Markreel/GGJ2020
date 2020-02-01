using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlimeMovement : Movement
{
    //[Header("Stats")]
    //public float movementSpeed;
    //public float gravityAmplifier;

    //[Header("Input")]
    //public string horizontalJoystick = "Horizontal";
    //public string verticalJoystick = "Vertical";
    //public Transform forwardTranform;

    //private float vInput = 0f;
    //private float hInput = 0f;

    //private Rigidbody rb;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}

    //private void FixedUpdate()
    //{
    //    Move();

    //    CustomGravity();
    //}

    //private void Update()
    //{
    //    GetInput();
    //}

    //private void GetInput()
    //{
    //    vInput = Input.GetAxis(verticalJoystick);
    //    hInput = Input.GetAxis(horizontalJoystick);
    //}

    //private void CustomGravity()
    //{
    //    rb.velocity = new Vector3(rb.velocity.x, -gravityAmplifier, rb.velocity.z);
    //}

    protected override Vector3 Move()
    {
        Vector3 vel = base.Move();

        Quaternion WantedRotation = Quaternion.LookRotation(vel);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, WantedRotation, Time.deltaTime * 5);
        return Vector3.zero;
    }

}
