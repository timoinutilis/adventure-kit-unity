using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class LocationManagerSaver : Saver
{
    public LocationManager locationManager;

    private class LocationManagerData
    {
        public string SceneName;
        public string PositionName;
    }

    public override string SaveGameKey()
    {
        return "LocationManager";
    }

    public override JObject ToSaveGameObject()
    {
        LocationManagerData data = new()
        {
            SceneName = locationManager.CurrentSceneName,
            PositionName = locationManager.PositionName
        };
        return JObject.FromObject(data);
    }

    public override void FromSaveGameObject(JObject obj)
    {
        LocationManagerData data = obj.ToObject<LocationManagerData>();
        locationManager.ChangeLocation(data.SceneName, data.PositionName);
    }

    public override void Reset()
    {
        locationManager.Reset();
    }
}
