using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TargetMovement : MonoBehaviour
{
    [Header("Stats")]
    public float movementSpeed;

    [Header("Input")]
    public string horizontalJoystick = "Horizontal";
    public string verticalJoystick = "Vertical";
    public Transform forwardTranform;

    private float vInput = 0f;
    private float hInput = 0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        vInput = Input.GetAxis(verticalJoystick);
        hInput = Input.GetAxis(horizontalJoystick);
    }

    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        Quaternion walkDir = Quaternion.Euler(transform.rotation.eulerAngles.x, forwardTranform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = walkDir;

        moveVelocity += transform.forward * vInput;
        moveVelocity += transform.right * hInput;


        moveVelocity = moveVelocity.normalized;
        moveVelocity *= movementSpeed;

        rb.velocity = moveVelocity;
    }
}
