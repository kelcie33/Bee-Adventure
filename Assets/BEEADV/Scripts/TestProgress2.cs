using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProgressBar;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Configuration;

public class TestProgress2: MonoBehaviour
{
	ProgressBarBehaviour BarBehaviour;
	[SerializeField] float UpdateDelay = 1f;
	private static int lastcount = 0;
	private static int totalcount = 0;
	private static int barpercent = 0;
	//	public GameObject hiveButton;
	//	public Text pollenText;

void Start ()
	{
		if (totalcount == 0)
			totalcount = 10000;
		lastcount = 0;
		barpercent = 0;
		BarBehaviour = GetComponent<ProgressBarBehaviour> ();

		//hiveButton = GetComponent<UnityEngine.UI.Button>();

//		while (true) {
//			yield return new WaitForSeconds (UpdateDelay);


//		}
	}


	// Update is called once per frame
	void Update ()
	{
//		if(pollenText !=null)
//			pollenText.text = "Pollen = " +  totalcount.ToString();
		totalcount = totalcount - 1;
		barpercent = (int)((totalcount / 10000f) * 100);

		if (totalcount > 10000)
			totalcount = 10000;

		if (barpercent > 100)
			barpercent = 100;

		BarBehaviour.Value = barpercent;


		if (Input.GetKey ("escape"))
			Application.Quit ();
		handlekey ();
	}

	private bool handlekey ()
	{
		//Debug.Log ("testProgress");


		//
		bool found = false;
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				found = true;
				break;
			}
		}

		//
		if (Input.anyKeyDown)
			found = true;
		//
		if (found) {
			// dosomething
		}
		return found;
	}


}

