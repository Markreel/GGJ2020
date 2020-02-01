using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float gravityAmplifier = 2f;
    public float angleThreshold = 15f;
    public float rayHeight = 10f;
    public float rayLength = 2f;
    public float yThreshold = 0.2f;

    [Header("Input")]
    public string horizontalJoystick = "Horizontal";
    public string verticalJoystick = "Vertical";
    public Transform forwardTranform;


    private Vector3 pos;
    private bool mayChangeDirection = true;
    private bool checkForDirection = true;
    private Vector3 forwardVector, rightVector;
    private float vInput = 0f;
    private float hInput = 0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        forwardVector = transform.forward;
        rightVector = transform.right;
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

    private void FixedUpdate()
    {
        Movement();
        if (checkForDirection)
        {
            CustomGravity();
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
        Quaternion lookDir = Quaternion.Euler(transform.rotation.eulerAngles.x, forwardTranform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = lookDir;

        if (checkForDirection)
        {
            forwardVector = transform.forward;
            rightVector = transform.right;
        }

        Vector3 moveVelocity = Vector3.zero;
        moveVelocity += forwardVector * vInput * speed;
        moveVelocity += rightVector * hInput * speed;

        moveVelocity = moveVelocity.normalized * speed;

        rb.velocity = moveVelocity;

        Quaternion WantedRotation = Quaternion.LookRotation(moveVelocity);
        transform.localRotation = WantedRotation;
    }

    private void CustomGravity()
    {
        rb.velocity = new Vector3(rb.velocity.x, -gravityAmplifier, rb.velocity.z);
    }

    private void OnEnterAimMode()
    {
        SetPos(forwardTranform.forward);
    }

    private void OnCollisionStay(Collision collision)
    {

        List<ContactPoint> contactPoints = new List<ContactPoint>();
        collision.GetContacts(contactPoints);

        if (contactPoints.Count > 0)
        {
            CheckOnCollisionHits(contactPoints);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (mayChangeDirection)
        {
            checkForDirection = true;
        }
    }

    private void CheckOnCollisionHits(List<ContactPoint> contactPoints)
    {
        if (mayChangeDirection)
        {
            for (int i = 0; i < contactPoints.Count; i++)
            {
                if (contactPoints[i].otherCollider == null) break;

                Vector3 dir = contactPoints[i].normal;

                //Forward
                if (Vector3.Angle(dir, -transform.forward) <= angleThreshold && Vector3.Angle(dir, -transform.forward) >= -angleThreshold)
                {
                    StartCoroutine(WaitDirChange());
                    checkForDirection = false;
                    //Debug.DrawRay(transform.position, dir, Color.blue, 1f);
                    //Debug.DrawRay(transform.position, GetTangent(dir), Color.red, 2f);
                    //Debug.DrawRay(transform.position, GetBiNormal(dir, GetTangent(dir)), Color.green, 2f);
                    Vector3 tangent = GetTangent(dir);
                    rightVector = tangent;
                    forwardVector = -GetBiNormal(dir, tangent);
                    return;
                } 
            
                //Ground
                //else if (Vector3.Angle(dir, -Vector3.up) <= angleThreshold && Vector3.Angle(dir, -Vector3.up) >= -angleThreshold)
                //{
                //    checkForDirection = true;
                //}
            }
        }
    }

    private IEnumerator WaitDirChange(float time = 0.1f)
    {
        mayChangeDirection = false;
        yield return new WaitForSeconds(time);
        mayChangeDirection = true;
    }

    private Vector3 GetTangent(Vector3 normal)
    {
        Vector3 tangent;
        Vector3 t1 = Vector3.Cross(normal, transform.forward);
        Vector3 t2 = Vector3.Cross(normal, transform.up);
        if (t1.magnitude > t2.magnitude)
        {
            tangent = t1;
        } else
        {
            tangent = t2;
        }

        return tangent;
    }

    private Vector3 GetBiNormal(Vector3 normal, Vector3 tangent)
    {
        return Vector3.Cross(normal, tangent);
    }
}
