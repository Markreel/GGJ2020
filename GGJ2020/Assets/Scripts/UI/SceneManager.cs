using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRegulator : MonoBehaviour
{
    private SceneRegulator instance;
    private SceneRegulator Instance { get { return instance; } set { instance = value; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public enum Scenes
    {
        StartLevel,
        TweedeLevel,
        DerdeLevel

    }

    public static void SwitchScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}
