using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHealth : MonoBehaviour {

	static float max_health = 100f;
	static float cur_health = 0f;
	public GameObject Healthbar;
	bool ranOnce = true;
	public float recover;
	public float calc_health;

	// Use this for initialization
	void Start() {
		recover = GameObject.FindGameObjectWithTag ("Potato").GetComponent<ProgBarHealth> ().gain_prog+=1;
		//Invoke("decreasehealth",.5f);
	//	cur_health = max_health;
		//GameObject.Find ("ProgressBarLabelFollow").GetComponent<TestProgress>().barpercent = healing;
	}
		


	// Update is called once per frame
	void Update () {
		
		if (ranOnce==true)
		{
			cur_health = max_health;
			decreasehealth ();
		}
			
	}

	void decreasehealth(){
		int randomIntlost;
		var minRan = 10;
		var maxRan = 50;
		randomIntlost = Random.Range(minRan, maxRan);

		cur_health -= randomIntlost;
		calc_health = cur_health / max_health;
		SetHealthBar (calc_health);
		ranOnce = false;
	}

	void increasehealth(){
		float regained;

		regained = recover / 10;
		cur_health = cur_health + regained;

		calc_health = cur_health / max_health;
		SetHealthBar (calc_health);
	}
		

	public void SetHealthBar(float myHealth){
		Healthbar.transform.localScale = new Vector3 (myHealth, Healthbar.transform.localScale.y, Healthbar.transform.localScale.z);
		//ranOnce = false;
	}

}
