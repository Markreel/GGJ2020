using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Stats")]
    public float maxDistance = 5;
    public float movementSpeed;
    public float gravityAmplifier;

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

        CustomGravity();
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

    private void CustomGravity()
    {
        rb.velocity = new Vector3(rb.velocity.x, -gravityAmplifier, rb.velocity.z);
    }

    protected virtual Vector3 Move()
    {
        if(Vector3.Distance(transform.position, SlimeManager.Instance.transform.position) > maxDistance) { return rb.velocity = Vector3.zero; }

        Vector3 moveVelocity = Vector3.zero;

        Quaternion walkDir = Quaternion.Euler(transform.rotation.eulerAngles.x, forwardTranform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = walkDir;

        moveVelocity += transform.forward * vInput;
        moveVelocity += transform.right * hInput;


        moveVelocity = moveVelocity.normalized;
        moveVelocity *= movementSpeed;

        rb.velocity = moveVelocity;

        return moveVelocity;
    }
}
