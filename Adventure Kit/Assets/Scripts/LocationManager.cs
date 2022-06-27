using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationManager : SaveGameContent
{
    [Serializable]
    class LocationManagerData
    {
        public string sceneName;
        public string positionName;
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLocation(string sceneName, string positionName)
    {
        PositionName = positionName;
        StartCoroutine(LoadNewLocation(sceneName, true));
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

    public override string Key()
    {
        return "LocationManager";
    }

    public override string ToJson()
    {
        LocationManagerData data = new();
        data.sceneName = CurrentSceneName;
        data.positionName = PositionName;
        return JsonUtility.ToJson(data);
    }

    public override void FromJson(string json)
    {
        var data = JsonUtility.FromJson<LocationManagerData>(json);
        PositionName = data.positionName;
        StartCoroutine(LoadNewLocation(data.sceneName, true));
    }

    public override void Reset()
    {
        PositionName = null;
        StartCoroutine(LoadNewLocation(startSceneName, true));
    }
}
