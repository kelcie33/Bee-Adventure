using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarStatus : MonoBehaviour {

	// Use this for initialization
	void Awake (){
		DontDestroyOnLoad (this.gameObject);
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}
}
