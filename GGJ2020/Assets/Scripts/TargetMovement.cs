using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float rayHeight = 10f;
    public float rayLength = 2f;
    public float yThreshold = 0.2f;

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
        if (vInput + hInput != 0)
        {
            Movement();
        }
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
                ExtDebug.DrawBox(newPos, transform.localScale / 2, Quaternion.identity, Color.blue);
                newPos.y = (newPos.y > transform.position.y)? transform.position.y + speed : transform.position.y - speed;
            }
            SetPos(newPos);
        }
    }

    private void OnEnterAimMode()
    {
        SetPos(forwardTranform.forward);
    }
}
