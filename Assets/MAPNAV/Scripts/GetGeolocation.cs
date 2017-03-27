//MAPNAV Navigation ToolKit v.1.4.0
//Attention: This script uses a custom editor inspector: MAPNAV/Editor/SetGeoInspector.cs

using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("MAPNAV/GetGeolocation")]

public class GetGeolocation : MonoBehaviour
{
    public float lat;
    public float lon;
    public float height;
    public float orientation;
    public float scaleX;
    public float scaleY;
    public float scaleZ;
    private float posX;
    private float posY;
    private float posZ;
    private float initX;
    private float initZ;
    private MapNav gps;
    private bool gpsFix;
    private int mapScale;
    double _lon;
    double _lat;

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

    }

    void Update()
    {
        if (gpsFix)
        {
            orientation = transform.eulerAngles.y;
            posX = transform.position.x;
            posZ = transform.position.z;
            height = transform.position.y * 100 / mapScale;
            scaleX = transform.localScale.x;
            scaleY = transform.localScale.y;
            scaleZ = transform.localScale.z;

            _lon = ((posX + initX) / 20037508.34) * 180 * mapScale;
            _lat = ((posZ + initZ) / 20037508.34) * 180 * mapScale;
            _lat = 180 / Math.PI * (2 * Math.Atan(Math.Exp(_lat * Math.PI / 180)) - Math.PI / 2);

            lat = (float)_lat;
            lon = (float)_lon;
        }
        else
        {
            lat = 0;
            lon = 0;
        }
    }
}