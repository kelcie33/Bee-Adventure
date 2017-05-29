using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene03 : MonoBehaviour {
	public void gotoScene03(){
		//string nextScene = "scene03"; // Changed to HiveScene4Bee
		string nextScene = "HiveScene4Bee2";
		SceneManager.LoadScene (nextScene);
	}
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
}
