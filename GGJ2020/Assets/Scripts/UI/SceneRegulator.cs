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

    public void LoadSceneAs(int index)
    {
        StartCoroutine(LoadSceneAsCor(index));
    }

    public IEnumerator LoadSceneAsCor(int index)
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        AO.allowSceneActivation = false;
        while (AO.progress < 0.9f)
        {
            yield return null;
        }

        //Fade the loading screen out here

        AO.allowSceneActivation = true;
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
