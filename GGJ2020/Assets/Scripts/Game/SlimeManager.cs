using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using FMODUnity;

public class SlimeManager : MonoBehaviour
{
    private static SlimeManager instance;
    public static SlimeManager Instance { get { return instance; } private set { instance = value; } }

    public enum States { Idle, Moving, Shooting, Dead };
    private States currentState;

    [SerializeField] CinemachineFreeLook thirdPersonCam;
    [SerializeField] List<Slime> slimeList = new List<Slime>();
    public Slime CurrentSlime;
    private Rigidbody rigidbodyOfCurrentSlime;


    //[SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] GameObject newSlimePrefab;
    [SerializeField] StudioEventEmitter studioEventEmitter;

    [Header("Blob Settings: ")]
    [SerializeField] TargetMovement targetMov;
    [SerializeField] AimTarget aimTarget;
    [SerializeField] GameObject blobPrefab;
    [SerializeField] float blobFlightDuration = 1;
    [SerializeField] AnimationCurve blobFlightCurve;
    private Coroutine blobFlightRoutine;

    [Header("Input")]
    [SerializeField] string aimInputString = "Aim";
    [SerializeField] string fireInputString = "Fire1";
    [SerializeField] string horizontalJoystick = "Horizontal";
    [SerializeField] string verticalJoystick = "Vertical";
    [SerializeField] string horizontalViewInputString = "HorizontalView";
    [SerializeField] string verticalViewInputString = "VerticalView";

    [Header("Stats")]
    public float movementSpeed;
    public float gravityAmplifier;

    public Transform camTransform;

    private float vInput = 0f;
    private float hInput = 0f;

    private bool isAiming = true;

    private bool aimInput = false;
    private bool fireInput = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        CheckWhichSlimeIsTheBiggest();
    }

    private void Start()
    {
        ToggleAim();
    }

    private void FixedUpdate()
    {
        Movement();
        CustomGravity();
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        vInput = Input.GetAxis(verticalJoystick);
        hInput = Input.GetAxis(horizontalJoystick);

        if (Input.GetAxisRaw(aimInputString) != 0 && !aimInput) { ToggleAim(); aimInput = true; }
        if (Input.GetAxisRaw(fireInputString) != 0 && isAiming && !fireInput) { StartShootAnimation(); fireInput = true; }

        if (Input.GetAxisRaw(aimInputString) == 0 && aimInput) { ToggleAim(); aimInput = false; } 
        if (Input.GetAxisRaw(fireInputString) == 0 && fireInput) { fireInput = false; }

    }

    #region Movement

    private void CustomGravity()
    {
        rigidbodyOfCurrentSlime.velocity = new Vector3(rigidbodyOfCurrentSlime.velocity.x, - gravityAmplifier, rigidbodyOfCurrentSlime.velocity.z);
    }

    private void Movement()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (hInput + vInput != 0)
        {
            Quaternion walkDir = Quaternion.Euler(CurrentSlime.transform.rotation.eulerAngles.x, camTransform.rotation.eulerAngles.y, CurrentSlime.transform.rotation.eulerAngles.z);
            CurrentSlime.transform.rotation = walkDir;
        }

        moveVelocity += CurrentSlime.transform.forward * vInput;
        moveVelocity += CurrentSlime.transform.right * hInput;

        moveVelocity = moveVelocity.normalized;
        moveVelocity *= movementSpeed;

        rigidbodyOfCurrentSlime.velocity = moveVelocity;

        if (hInput + vInput != 0)
        {
            Quaternion WantedRotation = Quaternion.LookRotation(moveVelocity);
            CurrentSlime.transform.localRotation = WantedRotation;
        }

        if (moveVelocity != Vector3.zero) { studioEventEmitter.Params[0].Value = 1; CurrentSlime.ToggleWalk(true); }
        else { studioEventEmitter.Params[0].Value = 0; CurrentSlime.ToggleWalk(false); }
    }

    public void PlayMovingAudio()
    {
        studioEventEmitter.Play();
    }
   
    #endregion

    #region BlobRelated

    private void ToggleAim()
    {
        isAiming = isAiming ? false : true;

        thirdPersonCam.m_XAxis.m_InputAxisName = isAiming ? "" : horizontalViewInputString;
        thirdPersonCam.m_YAxis.m_InputAxisName = isAiming ? "" : verticalViewInputString;

        thirdPersonCam.m_XAxis.m_InputAxisValue = 0;
        thirdPersonCam.m_YAxis.m_InputAxisValue = 0;

        aimTarget.transform.position = CurrentSlime.transform.position + Vector3.up * 0.2f;
        aimTarget.gameObject.SetActive(isAiming);
    }

    private void StartShootAnimation()
    {
        CurrentSlime.AnimateShoot();
    }

    public void ShootBlob()
    {
        //CurrentSlime.ShootBlob();
        blobFlightRoutine = StartCoroutine(IELerpBlobOverCurve());
    }

    private IEnumerator IELerpBlobOverCurve()
    {
        GameObject _blob;

        Vector3[] _points = aimTarget.Points;
        System.Array.Reverse(_points);
        _blob = Instantiate(blobPrefab, _points[0], Quaternion.identity);

        for (int i = 1; i < _points.Length - 2; i++)
        {
            float _timeValue = 0;
            while (_timeValue < 1 && _blob != null)
            {
                _timeValue += Time.deltaTime / (blobFlightDuration / _points.Length);
                _blob.transform.position = Vector3.Lerp(_points[i], _points[i + 1], _timeValue);
                yield return new WaitForSeconds(0.01f);
                yield return null;
            }

            //yield return new WaitForSeconds(blobFlightDuration / _points.Length - 1);
        }

        //while (_timeValue < blobFlightDuration)
        //{
        //    _timeValue += Time.deltaTime;
        //    blob.transform.position = Vector3.Lerp();


        //    yield return null;
        //}

        yield return null;
    }

    public void CreateNewSlime(Vector3 _position, bool _isSpecialPickup = false)
    {
        GameObject _newSlime = Instantiate(newSlimePrefab, _position, newSlimePrefab.transform.rotation);
        slimeList.Add(_newSlime.GetComponent<Slime>());
        _newSlime.GetComponent<Slime>().PickupSlime = _isSpecialPickup;

        CheckWhichSlimeIsTheBiggest(true);
    }

    #endregion

    #region PublicFunctions

    public void CheckWhichSlimeIsTheBiggest(bool _checkIfDead = false)
    {
        float _sizeRecord = 0;
        Slime _biggestBoi = null;

        foreach (var _slime in slimeList)
        {
            _slime.PrimeSlime = false;

            if (_slime.SlimeSize > _sizeRecord)
            {
                _sizeRecord = _slime.SlimeSize;
                _biggestBoi = _slime;
            }
        }

        if(_checkIfDead && _sizeRecord <= 1) { SceneRegulator.Instance.RestartScene(); }

        if(CurrentSlime != null && CurrentSlime.SlimeSize == _sizeRecord) { _biggestBoi = CurrentSlime; }

        OnNewPrimeSlime(_biggestBoi);
    }

    public void OnNewPrimeSlime(Slime slime)
    {
        CurrentSlime = slime;
        CurrentSlime.PrimeSlime = true;
        rigidbodyOfCurrentSlime = CurrentSlime.GetComponent<Rigidbody>();
        rigidbodyOfCurrentSlime.constraints = RigidbodyConstraints.FreezeRotation;

        thirdPersonCam.m_LookAt = CurrentSlime.transform;
        thirdPersonCam.m_Follow = CurrentSlime.transform;

        targetMov.playerTransform = CurrentSlime.transform;
    }

    public void RemoveSlime(Slime _slime)
    {
        slimeList.Remove(_slime);
        Destroy(_slime.gameObject);

        CheckWhichSlimeIsTheBiggest();
    }

    #endregion

}
