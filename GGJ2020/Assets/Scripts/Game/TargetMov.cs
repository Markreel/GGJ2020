using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMov : MonoBehaviour
{
    [Header("Stats")]
    public LayerMask layerMask;
    public float maxDistanceFromPlayer = 4f;
    public float speed;
    public float angleThreshold = 15f;
    public float rayHeight = 10f;
    public float rayLength = 2f;
    public float yThreshold = 0.2f;

    [Header("Input")]
    public string horizontalJoystick = "Horizontal";
    public string verticalJoystick = "Vertical";
    public Transform forwardTranform;
    public Transform playerTransform;


    private Vector3 pos;
    private bool mayChangeDirection = true;
    private bool checkForDirection = true;
    private Vector3 forwardVector, rightVector;
    private float vInput = 0f;
    private float hInput = 0f;

    private Rigidbody rb;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Default");
    }

    private void SetPos(Vector3 point)
    {
        pos = point;
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        if (hInput + vInput != 0)
        {
            Movement();
        }
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        vInput = Input.GetAxis(verticalJoystick);
        hInput = Input.GetAxis(horizontalJoystick);
    }

    private void Movement()
    {
        Vector3 wantedPos = pos;

        Quaternion lookDir = Quaternion.Euler(transform.rotation.eulerAngles.x, forwardTranform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = lookDir;

        Vector3 moveDir = transform.forward * vInput;
        moveDir += transform.right * hInput;
        moveDir.y = 0;
        moveDir = moveDir.normalized;

        wantedPos += moveDir * speed;

        if (Physics.Raycast(wantedPos + Vector3.up * rayHeight, Vector3.down * rayLength, out RaycastHit hit))
        {
            Vector3 newPos = hit.point;

            if (newPos.y < transform.position.y - yThreshold || newPos.y > transform.position.y + yThreshold)
            {
                newPos.x = transform.position.x;
                newPos.z = transform.position.z;
                newPos.y = (newPos.y > transform.position.y) ? transform.position.y + speed : transform.position.y - speed;
            }
            float dist = Vector3.Distance(playerTransform.position, newPos);
            dist += (newPos.y - pos.y > 0) ? newPos.y - pos.y : 0;

            if (dist < maxDistanceFromPlayer)
            {
                SetPos(newPos);
            }
        }
    }


    private void OnEnable()
    {
        SetPos(playerTransform.position);
    }
}
