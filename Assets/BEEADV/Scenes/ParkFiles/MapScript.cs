using System.Collections;
using System.Resources;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour {
	public float lat = 47.630115f;
	public float lon =-0.122314997f;
	string url = "";
	public string markerUrl="magentamapmarker.png";
	public int zoom = 14;
	public int mapWidth = 640;
	public int mapHeight = 640;
	public enum mapType

	{
		RoadMap,
		Satellite,
		Terrain,
		Hybrid
	}
	public float[,] flaglatlon = new float[,]{
		{47.750136f,	-122.156124f}
		,{47.639803f,	-122.294483f}
		,{47.628141f,	-122.294354f}

		,{47.630115f,	-122.314997f}
		,{47.54639f,	-122.372353f}
		,{47.682353f,	-122.256781f}
		,{47.760225f,	-122.192667f}
		,{47.679915f,	-122.328944f}
		,{47.671713f,	-122.306671f}


	};
//	21 Acres
//	Washington Park Arboretum 
//	Washington Park Arboretum 
//	Volunteer Park 
//	West Seattle Bee Garden 
//	Magnuson Park
//	University of Washington Bothell 
//	Green Lake
//	Ravenna Park 





	public mapType mapselected;
	public int scale;
	private IEnumerator mapCoroutine;
	String pipeString = "%7C";// escaped pipe |
	IEnumerator getGoogleMap(float lat,float lon){
	//	src  ="https://maps.googleapis.com/maps/api/js?key=
		url = "https://maps.googleapis.com/maps/api/staticmap?"
		+"center="+ lat + "," + lon
		+ "&zoom=" + zoom
		+ "&size=" + mapWidth + "x" + mapHeight
		+ "&scale=" + scale
		+ "&maptype=" + mapselected
			+ "&markers=icon:" +markerUrl + pipeString +"title:2"+pipeString +flaglatlon[1,0] +"," + flaglatlon[1,1]
			+ "&markers=icon:" +markerUrl + pipeString +"title:5"+pipeString +flaglatlon[4,0] +"," + flaglatlon[4,1]
			+ "&markers=icon:" +markerUrl + pipeString +"title:6"+pipeString +flaglatlon[5,0] +"," + flaglatlon[5,1]


			// ////////////////////////
//			+ "&markers=icon:" +markerUrl + pipeString +"title:1"+pipeString +flaglatlon[0,0] +"," + flaglatlon[0,1]
//			+ "&markers=icon:" +markerUrl + pipeString +"title:3"+pipeString +flaglatlon[2,0] +"," + flaglatlon[2,1]
//			+ "&markers=icon:" +markerUrl + pipeString +"title:4"+pipeString +flaglatlon[3,0] +"," + flaglatlon[3,1]
//			+ "&markers=icon:" +markerUrl + pipeString +"title:7"+pipeString +flaglatlon[6,0] +"," + flaglatlon[6,1]
//			+ "&markers=icon:" +markerUrl + pipeString +"title:8"+pipeString +flaglatlon[7,0] +"," + flaglatlon[7,1]
//			+ "&markers=icon:" +markerUrl + pipeString +"title:9"+pipeString +flaglatlon[8,0] +"," + flaglatlon[8,1]

			// //////////////////////
			//			+ "&key=" + "AIzaSyAe7r_gW3WsdO5iY7LP2sg_gRrMIkDyksI";
//
//		+ "&markers=color:red%7Clabel:1%7C" +flaglatlon[0,0] +"," + flaglatlon[0,1]
//			+ "&markers=color:blue%7Clabel:2%7C" +flaglatlon[1,0] +"," + flaglatlon[1,1]
//			+ "&markers=color:green%7Clabel:3%7C" +flaglatlon[2,0] +"," + flaglatlon[2,1]
//			+ "&markers=color:yellow%7Clabel:4%7C" +flaglatlon[3,0] +"," + flaglatlon[3,1]
//			+ "&markers=color:orange%7Clabel:5%7C" +flaglatlon[4,0] +"," + flaglatlon[4,1]
//			+ "&markers=color:purple%7Clabel:6%7C" +flaglatlon[5,0] +"," + flaglatlon[5,1]
//			+ "&markers=color:black%7Clabel:7%7C" +flaglatlon[6,0] +"," + flaglatlon[6,1]
//			+ "&markers=color:brown%7Clabel:8%7C" +flaglatlon[7,0] +"," + flaglatlon[7,1]
//			+ "&markers=color:white%7Clabel:9%7C" +flaglatlon[8,0] +"," + flaglatlon[8,1]
//			+ "&markers=icon:"+markerUrl +flaglatlon[8,0] +"," + flaglatlon[8,1]

		+ "&key=" + "AIzaSyAe7r_gW3WsdO5iY7LP2sg_gRrMIkDyksI";

		//		+ "&markers=color:red%7Clabel:R%7C" +lat +"," + lon
		//		+ "&markers=color:blue%7Clabel:B%7C" +lat +"," + (lon - .002f)
		//			+ "&markers=color:green%7Clabel:G%7C" +lat +"," + (lon - .004f)
//		?center=Brooklyn+Bridge,New+York,NY&zoom=13&size=600x300&maptype=roadmap
//			&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318
//		&markers=color:red%7Clabel:C%7C40.718217,-73.998284



		WWW www = new WWW (url);
		yield return www;
		gameObject.GetComponent<RawImage> ().texture = www.texture;
		StopCoroutine (mapCoroutine);
	}

	// Use this for initialization
	void Start () {
		mapCoroutine = getGoogleMap (lat,lon);
		StartCoroutine (mapCoroutine);
	}
	public void gotoScene03(){
		SceneManager.LoadScene ("scene03");
	}
	// Update is called once per frame


	void Update () {
		
	}
}
