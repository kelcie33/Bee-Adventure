using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnterText : MonoBehaviour
{

	//	// Use this for initialization
	//	void Start () {
	//
	//	}
	//
	//	// Update is called once per frame
	//	void Update () {
	//
	//	}
	public InputField inputField;
	public Text textName;
	public String defaultText;

	//	public void SubmitName (string arg0)
	public void SubmitName ()
	{
		Debug.Log ("fieldName= " + inputField.text);
		textName.text = inputField.text;
//		Debug.Log ("textName= " + textName.text);
//		inputField.text = defaultText;
		//defaultText  = inputField.text;
	}
}
