using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float rayHeight = 10f;
    public float rayLength = 2f;

    [Header("Input")]
    public string horizontalJoystick = "Horizontal";
    public string verticalJoystick = "Vertical";
    public Transform forwardTranform;


    private Vector3 pos;
    private float vInput = 0f;
    private float hInput = 0f;

    private void Start()
    {
        OnEnterAimMode();
    }

    private void SetPos(Vector3 point)
    {
        pos = point;
        transform.position = pos;
    }

    private void Update()
    {
        GetInput();
        Movement();
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
            SetPos(hit.point);
        }
    }

    private void OnEnterAimMode()
    {
        SetPos(forwardTranform.forward);
    }
}
