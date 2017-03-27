//MAPNAV Navigation ToolKit v.1.4.0

using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour
{
	private Transform target;
	private MapNav mapnav;
	private float screenX;
	private float screenY;
	private float dot;

	void Awake(){
		target = transform.parent.transform;
		mapnav = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
		screenX = Screen.width;
		screenY = Screen.height;
		if(screenY >= screenX){
			dot = screenY/800;
		}else{
			dot = screenX/800;
		}
	}

	void Start(){
		transform.parent.GetComponent<Renderer>().enabled = true;
	}

	void Update () {
		Vector3 screenPos = Camera.main.WorldToViewportPoint (target.position);
		if(!float.IsNaN(screenPos.x) && !float.IsNaN(screenPos.y)){
			transform.position = new Vector3(screenPos.x,screenPos.y, transform.position.z);
		}
		if(mapnav.mapping == false && mapnav.ready)
        {
			if(mapnav.gpsFix && !GetComponent<GUIText>().enabled)
				GetComponent<GUIText>().enabled=true;
            GetComponent<GUIText>().fontSize = Mathf.Clamp( (int) (18000*dot/Camera.main.orthographicSize/mapnav.mapScale),1,100);
		}
	}
}

