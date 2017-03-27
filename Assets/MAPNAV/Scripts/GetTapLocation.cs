//MAPNAV Navigation ToolKit v.1.4.0
//Please attach this script to the Main Camera (2D mode only)

using UnityEngine;
using System.Collections;
using System;
[AddComponentMenu("MAPNAV/GetTapLocation")]

public class GetTapLocation : MonoBehaviour
{
    float lat;
    float lon;

    private float posX;
    private float posZ;
    private float initX;
    private float initZ;
    private MapNav gps;
    private bool gpsFix;
    private bool moved;

	Camera mainCam; 
	private Vector3 touchPos;

    //Line renderer variables
    LineRenderer lineRenderer;
    private Vector3[] waypoints;
    private float radius;
    private int resolution = 60;
    private float lineWidth = 0.1f;
    private Material myMaterial;
    private float wavefrontSpeed;
    private float minRadius = 0.0f;
    private float maxRadius;
    private float positionY = 1.0f;

    void Awake()
    {
		mainCam = GetComponent<Camera>();

        //Reference to the MapNav.js script and gpsFix variable. gpsFix will be true when a valid location data has been set.
        gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
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
        if (gps.gpsFix && gps.ready)
        {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                moved = false;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
                moved = true;
            }
            if (Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Ended && moved==false)
            {
				touchPos = mainCam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0));
				posX = touchPos.x;
				posZ = touchPos.z;
                double _lat = ((posZ + initZ) / 20037508.34) * 180 * gps.mapScale;
                lat = (float)(180 / Math.PI * (2 * Math.Atan(Math.Exp(_lat * Math.PI / 180)) - Math.PI / 2));
            	lon = (float)((180*gps.mapScale * (posX + initX)) / 20037508.34);

				Debug.Log ("Touch detected at latitude: "+lat+", longitude: "+lon);
                gps.status = "\nTouch detected at latitude: " + lat + ", longitude: " + lon;
                if(gps.triDView==false)
                    StartCoroutine(RenderCircle());
            }
        }
        else
        {
            lat = 0;
            lon = 0;
        }
    }

    IEnumerator RenderCircle()
    {
        //Line renderer setup
        if (gameObject.GetComponent<LineRenderer>() == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            waypoints = new Vector3[resolution + 1];
            myMaterial = (Material)Resources.Load("Beacon", typeof(Material));
        }

        //LineRenderer on touch
        radius = minRadius;
        lineRenderer.material = myMaterial;
        float temp = lineWidth * mainCam.orthographicSize / 5;
        lineRenderer.SetWidth(temp, temp);
        maxRadius = 1.5f * mainCam.orthographicSize /5;
        wavefrontSpeed = 4.0f * mainCam.orthographicSize / 5;
        lineRenderer.material.shader = Shader.Find("Particles/Alpha Blended");
        lineRenderer.material.SetColor("_TintColor", new Color(0.5f, 0.27f, 0f, 0.5f));

        while (radius <= maxRadius)
        {
            //Set Points
            for (int n = 0; n < resolution; n++)
            {
                waypoints[n] = new Vector3(posX, positionY, posZ) + new Vector3(-radius * Mathf.Cos(n * 2 * Mathf.PI / resolution), positionY, radius * Mathf.Sin(n * 2 * Mathf.PI / resolution));
            }
            waypoints[resolution] = new Vector3(posX, positionY, posZ) + new Vector3(-radius * Mathf.Cos(2 * Mathf.PI), positionY, radius * Mathf.Sin(2 * Mathf.PI));

            //Render Points
            lineRenderer.SetVertexCount(resolution + 1);
            for (int j = 0; j <= resolution; j++)
            {
                Vector3 flatPos = new Vector3(waypoints[j].x, positionY, waypoints[j].z);
                lineRenderer.SetPosition(j, flatPos);
            }

            //Increase Circle Radius 
            radius += wavefrontSpeed * Time.deltaTime;

            //Fade off alpha channel (transparency)
            lineRenderer.material.shader = Shader.Find("Particles/Alpha Blended");
            lineRenderer.material.SetColor("_TintColor", new Color(0.5f, 0.27f, 0f, 0.5f - radius / (2 * (maxRadius - minRadius))));

            yield return null;
        }
        lineRenderer.SetVertexCount(0);
    }
}