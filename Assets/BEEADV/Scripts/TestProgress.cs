using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProgressBar;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Configuration;

public class TestProgress: MonoBehaviour {
	public ParticleSystem parts;
	ProgressBarBehaviour BarBehaviour;
	[SerializeField] float UpdateDelay = 1f;
	public int lastcount = 0;
	public int totalcount = 0;
	public int barpercent = 0;
	public GameObject hiveButton;
	public Text pollenText;

	//Natalia doing shenanagins
	public Animator BeetriceLevelUp;
	public GameObject BeetriceAnimationsLevelUp;


	IEnumerator Start ()
	{

		lastcount = 0;
		totalcount = 0;
		barpercent = 0;
		BarBehaviour = GetComponent<ProgressBarBehaviour>();

		//hiveButton = GetComponent<UnityEngine.UI.Button>();

		while (true)
		{
			yield return new WaitForSeconds(UpdateDelay);
			int partcount = parts.particleCount;
			if (partcount != lastcount) {
				//				Debug.Log("partcount="+partcount);
				//				Debug.Log("lastcount="+lastcount);
				if(partcount>lastcount)
					totalcount += partcount;
				lastcount = partcount;

				//				int x = (int)((totalcount / 30000f)*100);
				//				Debug.Log("totalcount=" + totalcount);
				//				Debug.Log("XXXXX=" + x);
				barpercent =(int)((totalcount / 10000f)*100);
				if (barpercent > 100)
					hiveButton.SetActive (true);

				if (totalcount > 10000)
					totalcount = 10000;
				//hiveButton.interactable = true;
				//		SceneManager.LoadScene ("scene02");
				if (barpercent > 100)
					barpercent = 100;
				//				barpercent += 1;
				//				Debug.Log("barpercent=>" + barpercent);

			}
			//BarBehaviour.Value = Random.value * 100;
			if(BarBehaviour.Value<100)
				BarBehaviour.Value=barpercent;
			//				BarBehaviour.SetFillerSizeAsPercentage(barpercent);
			//print("new value: " + BarBehaviour.Value);
		}
	}


	// Update is called once per frame
	void Update (){ 

		if (pollenText != null){
			pollenText.text = "Pollen = " + totalcount.ToString ();
			//		BarBehaviour.Value=BarBehaviour.Value+1000;

			if (Input.GetKey ("escape"))
				Application.Quit ();
			handlekey ();
		}

        /**
         * TODO: Need to set these in UNITY INSPECTOR:
         * - BeetriceLevelUp
         * - BeetriceAnimationsLevelUp
         * 
         * Missing these is causing ERRORS !!!!!!!!!!!!!!!!
         * 
         */

        //Natalia doing shenanagins
        BeetriceLevelUp = BeetriceAnimationsLevelUp.GetComponent<Animator>();

		if (hiveButton.activeInHierarchy==false) {
			//Natalia doing shenanagins
			BeetriceLevelUp.Play ("static");
		}

		if (hiveButton.activeInHierarchy) {
			//Natalia doing shenanagins
			BeetriceLevelUp.Play ("celebration");
		}
	}
	private bool handlekey(){
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
