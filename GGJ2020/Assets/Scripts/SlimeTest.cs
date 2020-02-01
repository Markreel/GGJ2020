using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SlimeTest : MonoBehaviour
{
    public enum States { Idle, Moving, Dead }
    private States currentState;

    [SerializeField] CinemachineFreeLook thirdPersonCam;
    [SerializeField] List<GameObject> slimeList = new List<GameObject>();


    //[SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] GameObject newSlimePrefab;

    [Header("Blob Settings: ")]
    [SerializeField] AimTarget aimTarget;
    [SerializeField] GameObject blobPrefab;
    [SerializeField] float blobFlightDuration = 1;
    [SerializeField] AnimationCurve blobFlightCurve;
    private Coroutine blobFlightRoutine;

    [Header("Input")]
    [SerializeField] string aimInputString = "Aim";
    [SerializeField] string fireInputString = "Fire1";
    [SerializeField] string horizontalInputString = "Horizontal";
    [SerializeField] string verticalInputString = "Vertical";
    [SerializeField] string horizontalViewInputString = "HorizontalView";
    [SerializeField] string verticalViewInputString = "VerticalView";

    private bool isAiming = true;

    private void Start()
    {
        ToggleAim();
    }

    private void Update()
    {
        if (Input.GetButtonDown(aimInputString)) { ToggleAim(); }
        if (Input.GetButtonDown(fireInputString) && isAiming) { ShootBlob(); }
    }

    private void ToggleAim()
    {
        isAiming = isAiming ? false : true;

        thirdPersonCam.m_XAxis.m_InputAxisName = isAiming ? "" : horizontalViewInputString;
        thirdPersonCam.m_YAxis.m_InputAxisName = isAiming ? "" : verticalViewInputString;

        thirdPersonCam.m_XAxis.m_InputAxisValue = 0;
        thirdPersonCam.m_YAxis.m_InputAxisValue = 0;

        aimTarget.transform.position = transform.position;
        aimTarget.gameObject.SetActive(isAiming);
    }

    private void ShootBlob()
    {
        blobFlightRoutine = StartCoroutine(IELerpBlobOverCurve());
    }


    private IEnumerator IELerpBlobOverCurve()
    {
        GameObject _blob;

        Vector3[] _points = aimTarget.Points;
        System.Array.Reverse(_points);
        _blob = Instantiate(blobPrefab, _points[0], blobPrefab.transform.rotation);

        for (int i = 1; i < _points.Length - 2; i++)
        {
            float _timeValue = 0;
            while (_timeValue < 1)
            {
                _timeValue += Time.deltaTime / (blobFlightDuration / _points.Length);
                _blob.transform.position = Vector3.Lerp(_points[i], _points[i + 1], _timeValue);
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
}
