using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRegulator : MonoBehaviour
{
    public static SceneRegulator Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
