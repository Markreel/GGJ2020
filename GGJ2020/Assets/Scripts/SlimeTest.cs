using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SlimeTest : MonoBehaviour
{
    [SerializeField] List<GameObject> slimeList = new List<GameObject>();


    //[SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] GameObject newSlimePrefab;

    [Header("Input")]
    [SerializeField] string horizontalInputString = "Horizontal";
    [SerializeField] string verticalInputString = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShootBlob()
    {
        
    }



}
