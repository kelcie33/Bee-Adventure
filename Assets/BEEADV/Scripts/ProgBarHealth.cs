using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgBarHealth : MonoBehaviour {
	//WTD: What this does; cause comments blend into my precious debugging commands.

	// WTD: all this to turn the random amount of progression from the flowertapping scene
	// and store it in the bar
	public float cur_prog = 0; 	//current progression
	public float min_prog = 0; 	//minimal progression
	public float gain_prog = 0; //gained progression
	public float max_prog = 1000; //maximum progression
	static float store_prog; //stored progression


	public GameObject Progbar;
	public GameObject flower3;
	//static float store_prog;

	//WTD: this bool is essentially making sure that the constantly checking for progression
	//does not go into infinity or make what the progression amount is INTO the current
	//without adding up. The flowers have a function that turns this boolean true.
	public bool onetime = false;

	//Level display
	public int level = 1;
	public Text CurrentLevel;

	//WTD: appears if you complete the progress bar
	public GameObject hiveButton;

	//WTD: Beetrice celebrates when you complete the progress bar
	public Animator BeetriceLevelUp;
	public GameObject BeetriceAnimationsLevelUp;

	// Use this for initialization
	void Start() {
		cur_prog = 0;
	}
		


	// Update is called once per frame
	void Update () {
		
		//WTD: makes sure it only gets one value from the variables and stores it
		//until it is true again (called by another flower)
		if (onetime == true) {
			Debug.Log("invoking counting");
			counting();
		}
		onetime = false;
		increaseProg ();
	}

	void counting(){
		//WTD: takes the value and adds it up into the current progression.
		store_prog = GameObject.Find("flower3").GetComponent<Flowertapping514>().randomProgGain; //getting the random amount from the flower
		gain_prog = store_prog;
		cur_prog  = gain_prog + cur_prog;

		//changing the current level in case of level up (resetting the count to 0)
		if (cur_prog >= max_prog) {
			cur_prog = 0;
			print ("level:" + level++);
			CurrentLevel.text = "Level: " + level.ToString ();
			//WTD: make the hivebutton visible and plays the level up animation
			hiveButton.SetActive (true);
			levelupAnimation ();
		}
	}

	void levelupAnimation(){
		//WTD: when the hivesbutton is visible, Beetrice celebrates
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


	void increaseProg(){
		//WTD: making the data into a certain type of number that the bar can move
		float calc_prog = cur_prog/ max_prog;
		SetProgbar(calc_prog);
	}
		

	public void SetProgbar(float myHealth){
		//WTD: the bar actually moving.
		Progbar.transform.localScale = new Vector3 (myHealth, Progbar.transform.localScale.y, Progbar.transform.localScale.z);
	}

}
