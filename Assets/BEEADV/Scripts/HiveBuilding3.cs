using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiveBuilding3 : MonoBehaviour {
	public GameObject Hive1;
	public GameObject Hive2;
	public GameObject Hive3;

	public GameObject Hive4;
	public GameObject Hive5;
	public GameObject Hive6;

	public GameObject Hive7;
	public GameObject Hive8;
	public GameObject Hive9;

	public GameObject Appear;
	public bool VisibleorNot = false;

	public int HiveNumber = 1;
	public float NewHive;
	public float targetScore = 1000;

	void Awake(){ 
		Hive1.SetActive (true);
		Hive2.SetActive (false);

		Hive3.SetActive (false);
		Hive4.SetActive (false);

		Hive5.SetActive (false);
		Hive6.SetActive (false);

		Hive7.SetActive (false);
		Hive8.SetActive (false);

		Hive9.SetActive (false);

		Appear.SetActive (false);

		Appear.SetActive (false);
		VisibleorNot = false;
	}

	// Use this for initialization
	void Start() {
		//HiveNumber =  GameObject.Find("ProgressBarLabelFollow").GetComponent<ProgBarHealth>().level;
		//NewHive =  GameObject.Find("ProgressBarLabelFollow").GetComponent<ProgBarHealth>().cur_prog;
	}
		


	// Update is called once per frame
	void Update () {
		HiveNumber =  GameObject.FindGameObjectWithTag("Potato").GetComponent<ProgBarHealth>().level;
		NewHive =  GameObject.FindGameObjectWithTag("Potato").GetComponent<ProgBarHealth>().cur_prog;
		targetScore = 500;
		Keeping ();
	}

	void Keeping(){
		if (NewHive >= targetScore) {
			Debug.Log ("new hive is bigger than target score " + NewHive + " / " + targetScore);
			VisibleorNot = true;
			Appear.SetActive (true);
		}

	}

	public void OnClick(){
		VisibleorNot = false;
		Appear.SetActive (false);
		AlltheHives ();
		Debug.Log ("fuuckkk");
	}
		


	void AlltheHives(){
		if (HiveNumber > 8) {
			Hive9.SetActive (true);
			Debug.Log ("Hive8 Active with " + HiveNumber);
		} else {
			if (HiveNumber > 7) {
				Hive8.SetActive (true);
				Hive7.SetActive (true);
				Hive6.SetActive (true);
				Hive5.SetActive (true);
				Hive4.SetActive (true);
				Hive3.SetActive (true);
				Hive2.SetActive (true);
				Debug.Log ("Hive8 Active with " + HiveNumber);
			} else {
				if (HiveNumber > 6) {
					Hive7.SetActive (true);
					Hive6.SetActive (true);
					Hive5.SetActive (true);
					Hive4.SetActive (true);
					Hive3.SetActive (true);
					Hive2.SetActive (true);
					Debug.Log ("Hive7 Active with " + HiveNumber);
				} else {
					if (HiveNumber > 5) {
						Hive6.SetActive (true);
						Hive5.SetActive (true);
						Hive4.SetActive (true);
						Hive3.SetActive (true);
						Hive2.SetActive (true);
						Debug.Log ("Hive6 Active with " + HiveNumber);
					} else {
						if (HiveNumber > 4) {
							Hive5.SetActive (true);
							Hive4.SetActive (true);
							Hive3.SetActive (true);
							Hive2.SetActive (true);
							Debug.Log ("Hive5 Active with " + HiveNumber);
						} else {
							if (HiveNumber > 3) {
								Hive4.SetActive (true);
								Hive3.SetActive (true);
								Hive2.SetActive (true);
								Debug.Log ("Hive4 Active with " + HiveNumber);
							} else {
								if (HiveNumber > 2) {
									Hive3.SetActive (true);
									Hive2.SetActive (true);
									Debug.Log ("Hive3 Active with " + HiveNumber);
								} else {
									if (HiveNumber > 1) {	
										Hive2.SetActive (true);
										Debug.Log ("Hive2 Active with " + HiveNumber);
										return;
									}
								}
							}
						}
					}
				}
			}
		}

	}
		


}






