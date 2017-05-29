using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SomeClick : MonoBehaviour
{
	
	public ParticleSystem parts;

	public void returnto01 ()
	{
		SceneManager.LoadScene ("scene01");
	}

	public void itwasclicked ()
	{
//		Debug.Log ("itwasclicked");
		parts.Emit (100);
	}
	// Use this for initialization
	void Start ()
	{
		//
//		var input = gameObject.GetComponent<InputField> ();
//		var se = new InputField.SubmitEvent ();
//		se.AddListener (SubmitName);
//		input.onEndEdit = se;
		//
	}

	public void gotoScene02(){
		//string nextScene = "scene02";  // Renamed to HiveScene4Hive
		string nextScene = "HiveScene4Hive2";
		SceneManager.LoadScene (nextScene);
	}
	
	// Update is called once per frame
	//	void Update () {
	//
	//	}
}