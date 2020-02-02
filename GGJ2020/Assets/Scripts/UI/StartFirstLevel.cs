using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartFirstLevel : MonoBehaviour
{
    public string startButton;
    public UnityEvent OnStartButton;

    private void Update()
    {
        if (Input.GetButtonDown(startButton))
        {
            OnStartButton?.Invoke();
        }
    }
}
