//MAPNAV Navigation ToolKit v.1.4.0
//Please attach this script to any game object in your scene

using UnityEngine;
using System;
using System.Collections;


public class InstantVelocity : MonoBehaviour
{
	private float initX;
	private float initZ;
	private MapNav gps;
    private GPS_Status status;
	private bool gpsFix;
    double instantVelocity;
	Transform user;
	Vector3 prePos;

	void Awake()
	{
		//Reference to the MapNav.js script and gpsFix variable. gpsFix will be true when a valid location data has been set.
		gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
        status = GameObject.FindGameObjectWithTag("GameController").GetComponent<GPS_Status>();
        gpsFix = gps.gpsFix;
        status.speedometer = true;
		user = GameObject.FindGameObjectWithTag ("Player").transform;
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

		StartCoroutine("CalculateVelocity");
	}
	
	public IEnumerator CalculateVelocity(){

		while (true) {
			double instantDistance = GeoDistance (Pos2Loc (prePos), Pos2Loc (user.position));
			instantVelocity = (instantDistance / 1.0f);

			prePos = user.position;

			//Debug.Log ("Velocity: " + (instantVelocity * 3600.0f) + "Km/h");
            status.instantVel = Mathf.Round(((float)instantVelocity * 3600.0f));
            yield return new WaitForSeconds (1.0f);
		}
	}

	public double GeoDistance (locWGS84 loc1, locWGS84 loc2)
	{
		double radius = 6371;
		double dLat = this.toRadian(loc2.Latitude - loc1.Latitude);
		double dLon = this.toRadian(loc2.Longitude - loc1.Longitude);
		double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
			Math.Cos(this.toRadian(loc1.Latitude)) * Math.Cos(this.toRadian(loc2.Latitude)) *
				Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
		double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
		double d = radius * c;
		return d;
	}
	
	public locWGS84 Pos2Loc (Vector3 wayPoint) {

        double lon = ((wayPoint.x + initX) / 20037508.34) * 180 * gps.mapScale;
        double lat = ((wayPoint.z + initZ) / 20037508.34) * 180 * gps.mapScale;
        lat = 180 / Math.PI * (2 * Math.Atan(Math.Exp(lat * Math.PI / 180)) - Math.PI / 2);
		
		locWGS84 result = new locWGS84 (lat,lon);
		return result;
		
	}
	
	// Convert to Radians.
	private double toRadian(double val)
	{
		return (Math.PI / 180) * val;
	}
	
}	
