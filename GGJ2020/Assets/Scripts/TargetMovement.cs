using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float angleThreshold = 15f;
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

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        Quaternion lookDir = Quaternion.Euler(transform.rotation.eulerAngles.x, forwardTranform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = lookDir;

        Vector3 moveVelocity = Vector3.zero;
        moveVelocity += transform.forward * vInput * speed;
        moveVelocity += transform.right * hInput * speed;

        moveVelocity = moveVelocity.normalized * speed;

        rb.velocity = moveVelocity;

        Quaternion WantedRotation = Quaternion.LookRotation(moveVelocity);
        transform.localRotation = WantedRotation;

        Debug.DrawRay(transform.position, transform.forward, Color.red, 2f);

        //Vector3 wantedPos = pos;

        //Vector3 moveDir = transform.forward * vInput;
        //moveDir += transform.right * hInput;
        //moveDir.y = 0;
        //moveDir = moveDir.normalized;

        //wantedPos += moveDir * speed;

        //if (Physics.Raycast(wantedPos + Vector3.up * rayHeight, Vector3.down * rayLength, out RaycastHit hit))
        //{
        //    Vector3 newPos = hit.point;
        //    if (newPos.y < transform.position.y - yThreshold || newPos.y > transform.position.y + yThreshold)
        //    {
        //        newPos.x = transform.position.x;
        //        newPos.z = transform.position.z;
        //        newPos.y = (newPos.y > transform.position.y)? transform.position.y + speed : transform.position.y - speed;
        //    }
        //    SetPos(newPos);
        //}
    }

    private void OnEnterAimMode()
    {
        SetPos(forwardTranform.forward);
    }

    private void OnCollisionStay(Collision collision)
    {

        List<ContactPoint> contactPoints = new List<ContactPoint>();
        collision.GetContacts(contactPoints);
        contactPoints.RemoveAt(collision.contactCount);
        if (contactPoints.Count > 0)
        {
            CheckOnCollisionHits(contactPoints);
        }
    }

    private void CheckOnCollisionHits(List<ContactPoint> contactPoints)
    {
        for (int i = 0; i < contactPoints.Count; i++)
        {
            if (contactPoints[i].otherCollider == null) break;

            Vector3 dir = contactPoints[i].normal;



            //Ceiling
            //else if (Vector3.Angle(dir, Vector3.down) <= groundCollAngleThreshold && Vector3.Angle(dir, Vector3.down) >= -groundCollAngleThreshold)
            //{
            //    break;
            //}
        }


        for (int i = 0; i < contactPoints.Count; i++)
        {
            if (contactPoints[i].otherCollider == null) break;

            Vector3 dir = contactPoints[i].normal;

            //Ground
            if (Vector3.Angle(dir, Vector3.up) <= angleThreshold && Vector3.Angle(dir, Vector3.up) >= -angleThreshold)
            {
                return;
            }

            //Right
            if (Vector3.Angle(dir, -transform.right) <= angleThreshold && Vector3.Angle(dir, -transform.right) >= -angleThreshold)
            {

                break;
            }

            //Left
            else if (Vector3.Angle(dir, transform.right) <= angleThreshold && Vector3.Angle(dir, transform.right) >= -angleThreshold)
            {

                break;
            }

            //Forward
            else if (Vector3.Angle(dir, -transform.forward) <= angleThreshold && Vector3.Angle(dir, -transform.forward) >= -angleThreshold)
            {
                Debug.Log("Something in front");
                break;
            }
        }
        


    }
}
