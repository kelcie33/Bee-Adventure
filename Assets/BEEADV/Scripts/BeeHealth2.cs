using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHealth2 : MonoBehaviour {

	static float max_health = 100f;
	static float cur_health = 0f;
	public GameObject Healthbar;
	bool ranOnce = true;
	public float recover;
	//public float calc_health;
	//public float regained;

	// Use this for initialization
	void Start() {
		recover = GameObject.FindGameObjectWithTag ("Potato").GetComponent<ProgBarHealth> ().gain_prog;
		//Invoke("decreasehealth",.5f);
	//	cur_health = max_health;
		//GameObject.Find ("ProgressBarLabelFollow").GetComponent<TestProgress>().barpercent = healing;
	}
		


	// Update is called once per frame
	void Update () {
		recover = GameObject.FindGameObjectWithTag ("Potato").GetComponent<ProgBarHealth> ().gain_prog;
		if (ranOnce == true) {
			cur_health = max_health;
			decreasehealth ();
		}
		if (cur_health != max_health) {
			increasehealth ();
		}
	}

	void decreasehealth(){
		int randomIntlost;
		var minRan = 10;
		var maxRan = 50;
		randomIntlost = Random.Range(minRan, maxRan);

		cur_health -= randomIntlost;
		float calc_health = cur_health / max_health;
		SetHealthBar (calc_health);
		ranOnce = false;
	}

	void increasehealth(){ 
			float regained = recover / 10f;

			cur_health += regained;
			float calc_health = cur_health / max_health;

			SetHealthBar (calc_health);
	}
		

	public void SetHealthBar(float myHealth){
		if (myHealth > 1) {
			myHealth = 1;
		}
		Healthbar.transform.localScale = new Vector3 (myHealth, Healthbar.transform.localScale.y, Healthbar.transform.localScale.z);
		//ranOnce = false;
	}

}
