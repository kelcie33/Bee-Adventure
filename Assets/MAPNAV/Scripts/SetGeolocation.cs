//MAPNAV Navigation ToolKit v.1.4.0
//Attention: This script uses a custom editor inspector: MAPNAV/Editor/SetGeoInspector.cs

using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("MAPNAV/SetGeolocation")]

public class SetGeolocation : MonoBehaviour
{
    public float lat;
    public float lon;
    public float height;
    public float orientation;
    public float scaleX;
    public float scaleY;
    public float scaleZ;
    public bool mercatorDistortion;
    private float initX;
    private float initZ;
    private MapNav gps;
    private bool gpsFix;
    private float fixLat;
    private float fixLon;
    private int mapScale;
    private float scaleFactor;

    void Awake()
    {
        //Reference to the MapNav.js script and gpsFix variable. gpsFix will be true when a valid location data has been set.
        gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
        gpsFix = gps.gpsFix;
        mapScale = gps.mapScale;
    }

    IEnumerator Start()
    {
        //Wait until the gps sensor provides a valid location.
        while (!gpsFix)
        {
            gpsFix = gps.gpsFix;
            yield return null;
        }
        //Read initial position (used as a reference system)
        initX = gps.iniRef.x;
        initZ = gps.iniRef.z;
        //Set object geo-location
        GeoLocation();
    }

    [ContextMenu("GeoLocation")]

    void GeoLocation()
    {
        //Translate the geographical coordinate system used by gps mobile devices(WGS84), into Unity's Vector2 Cartesian coordinates(x,z).
        transform.position = WGS84toWebMercator(lon, lat, height);
      
        //Set object orientation
        Vector3 tmp = transform.eulerAngles;
        tmp.y = orientation;
        transform.eulerAngles = tmp;

        //Set local object scale
        if (transform.localScale != Vector3.zero)
        {
            if (mercatorDistortion)
                scaleFactor = mapScale * Mathf.Cos(lat * Mathf.PI / 180);
            else
                scaleFactor = mapScale;

            transform.localScale = new Vector3(scaleX / scaleFactor, scaleY / scaleFactor, scaleZ / scaleFactor);
        }
    }

    //This function is to be used by SetGeoInspector.cs
    public void EditorGeoLocation()
    {
        gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
        fixLat = gps.fixLat;
        fixLon = gps.fixLon;
        mapScale = gps.mapScale;

        initX = fixLon * 20037508.34f / (180*mapScale);
        initZ = (float) (System.Math.Log(System.Math.Tan((90 + fixLat) * System.Math.PI / 360)) / (System.Math.PI / 180));
        initZ = initZ * 20037508.34f / (180*mapScale);

        GeoLocation();
    }

    //Convert  WGS84 to WebMercator
    Vector3 WGS84toWebMercator(float _lon, float _lat, float _height)
    {
        double x = (_lon * 20037508.34 / 180) / mapScale - initX;
        double z = (Math.Log(Math.Tan((90 + _lat) * Math.PI / 360)) / (Math.PI / 180));
        z = (z * 20037508.34 / 180) / mapScale - initZ;
        float y = _height/mapScale;
        return new Vector3((float)x, y, (float)z);
    }
}