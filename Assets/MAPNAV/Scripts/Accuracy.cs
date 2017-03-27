//MAPNAV Navigation ToolKit v.1.4.0
//Please attach this script to the "Player" game object

using UnityEngine;
using System;
using System.Collections;
[AddComponentMenu("MAPNAV/Horizontal Accuracy")]

public class Accuracy : MonoBehaviour
{
	public int resolution = 60;
	public float lineWidth = 0.08f;
	private Material myMaterial;
	private MapNav gps;
	private Camera mycam;
	Vector3[] waypoints;
	LineRenderer lineRenderer;
    float error;
    float preError;

	void Awake(){
		
		gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
		mycam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		lineRenderer = gameObject.AddComponent<LineRenderer> ();
		waypoints = new Vector3[resolution+1];
	}

	void Start()
	{
		lineRenderer.useWorldSpace = true;
        myMaterial = (Material)Resources.Load("Accuracy", typeof(Material));
        lineRenderer.material = myMaterial;
	}
	
	void Update()
	{
        //Obtain or simulate GPS horizontal accuracy
        if (!gps.simGPS)
            error = gps.accuracy;
        else
            error = 30.0f;


        error = Mathf.Lerp(preError, error, Time.deltaTime);

        //Set points
        for (int n=0; n<resolution; n++){
			waypoints[n]= transform.position + new Vector3(-error/gps.mapScale * (1 / Mathf.Cos(gps.userLat * Mathf.PI / 180)) * Mathf.Cos(n*2*Mathf.PI/resolution),0.0f, error/gps.mapScale * (1 / Mathf.Cos(gps.userLat * Mathf.PI / 180)) * Mathf.Sin(n*2*Mathf.PI/resolution));
		}
		waypoints[resolution] = transform.position + new Vector3(-error/gps.mapScale * (1/Mathf.Cos(gps.userLat * Mathf.PI / 180)) * Mathf.Cos(2*Mathf.PI),0.0f, error/gps.mapScale * (1 / Mathf.Cos(gps.userLat * Mathf.PI / 180)) * Mathf.Sin(2*Mathf.PI));

        //Draw Circle
        if (!gps.triDView)
            lineRenderer.SetWidth(lineWidth / 9.594413f * mycam.orthographicSize, lineWidth / 9.594413f * mycam.orthographicSize);
        else
            lineRenderer.SetWidth(lineWidth*100/gps.mapScale, lineWidth*100/gps.mapScale);

        lineRenderer.SetVertexCount (resolution+1);
		for (int j=0; j<=resolution; j++) {
			Vector3 flatPos = new Vector3(waypoints [j].x,transform.position.y+(0.1f*100/gps.mapScale),waypoints [j].z);
			lineRenderer.SetPosition(j, flatPos);
		}
        if (!gps.triDView)
        {
            if (gps.mapping || transform.localScale.x > error / (gps.mapScale * 4.7f))
            {
                lineRenderer.enabled = false;
            }
            else {
                lineRenderer.enabled = true;
            }
        }
        else
        {
            lineRenderer.enabled = true;
        }
        preError = error;
	}
} 