using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance { get; private set; }

    public string startSceneName;

    public string PositionName { get; private set; }
    public string CurrentSceneName { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            throw new UnityException("LocationManager must exist only once");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNewLocation(startSceneName, false));
    }
    
    public void ChangeLocation(string sceneName, string positionName)
    {
        PositionName = positionName;
        StartCoroutine(LoadNewLocation(sceneName, true));
    }

    public void Reset()
    {
        PositionName = null;
        StartCoroutine(LoadNewLocation(startSceneName, true));
    }

    private IEnumerator LoadNewLocation(string sceneName, bool unloadOldScene)
    {
        if (unloadOldScene)
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
        CurrentSceneName = sceneName;
    }
}
