using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowertapping514 : MonoBehaviour {
	
	//WTD: What this does; cause comments blend into my precious debugging commands.

	//all of this just to say "yes phone, i touch you."
	private Ray m_Ray;
	private RaycastHit m_RayCastHit;
	private TouchableObject m_CurrentTouchableObject;
	private Animator FlowerController;
	public GameObject Flower3;		

	//Random amount of progress gained
	public float randomProgGain;

	//materical to swtich for when the flower turns grey
	public Material[] MyMats; 

	//mainly for animation purposes; opening when touch and closing when no more particals
	public bool firstTimeOpening = true;
	public bool firstTimeClosing = true;

	//particles whoo
	public ParticleSystem parts;

	//these are to ensure random amount of clicking
	public int amount = 0;
	int i;
	bool loop = true;

	void Awake (){

		var minClick = 10; //min	imal times you can click the flower
		var maxClick = 12; // maximum times you can click the flower
		amount = Random.Range(minClick, maxClick); //random amount of clicks
		amount = amount - 2; //for some reason it keeps registering the "amount + 2", so this solves that problem
		GameObject.FindGameObjectWithTag("Potato").GetComponent<ProgBarHealth>().onetime = false;
	}

	void start (){
		GameObject.FindGameObjectWithTag("Potato").GetComponent<ProgBarHealth>().onetime = false;
	}
	void Update () {
		// WTD: basically the phone asking "YOU TOUCHING ME?"
		FlowerController = Flower3.GetComponent<Animator>();
		Input.simulateMouseWithTouches = true;

		if (Input.touchCount > 0) {//  && Input.GetTouch(0).phase == TouchPhase.Began)
			Touch touchedFinger = Input.touches [0];
			Debug.Log ("Current Finger?: " + touchedFinger);
			//Animation.Play ("headShake");
			switch (touchedFinger.phase) {
			case TouchPhase.Ended: 
				m_Ray = Camera.main.ScreenPointToRay (touchedFinger.position);
				if (Physics.Raycast (m_Ray.origin, m_Ray.direction, out m_RayCastHit, Mathf.Infinity)) {
					TouchableObject touchableObj = m_RayCastHit.collider.gameObject.GetComponent<TouchableObject> ();

					if (touchableObj) {								
						m_CurrentTouchableObject = touchableObj;
						Debug.Log ("Click detected");
						FlowerController.Play ("opening");
					} else {
						if (m_CurrentTouchableObject) {
							m_CurrentTouchableObject = null;
							FlowerController.Play ("closed");
						}
					}

				}
				break;
			default:
				break;
			}
		}
	}
	void OnMouseDown(){
		Debug.Log ("BLAH BLAH BLAH");
		// WTD: first time opening is extra shiny and opens from a bud
		if (firstTimeOpening == true) {
			parts.Emit (200);
			FlowerController.Play ("opening");
			firstTimeOpening = false;
		}

		// WTD: you get progress everytime you touch the flower and particals depending on the amount you're able to click
		if (loop == true){
			if (i <= amount) {	
				parts.Emit (100);
				gettingProgress ();
				//GameObject.FindGameObjectWithTag ("Potato").GetComponent<ProgBarHealth> ().onetime = true;
			//	Debug.Log ("yadda yadda " + i);
			//	Debug.Log ("adday addday " + amount);
				i = i + 1;
			} else {
				loop = false;
			}
		}
		if (loop == false) {

			// WTD: Basically telling the flower it's out of clicks and to turn into a grey bud.
			//Debug.Log ("am I working?");
			//GetComponent<SkinnedMeshRenderer> ().material = MyMats [1];
			limitedclicks ();
		}
	}



	void gettingProgress(){

		//random amount of progress in between these two numbers.
		var minRan = 100f;
		var maxRan = 200f;

		randomProgGain = Random.Range(minRan, maxRan);
	//	Debug.Log ("random number:" + randomProgGain);
	//	Debug.Log ("making shit true");
//		limitedclicks ();

		//WTD: this is bit tricky. Changes the booleon on another script (ProgBarHealth) so the progress
		//bar will be able to receive, store, and add the random amount of progress in this area.
		GameObject.FindGameObjectWithTag("Potato").GetComponent<ProgBarHealth>().onetime = true;

	}

	void limitedclicks(){
		// WTD: telling the flower to turn into a little grey bud.
	//	GetComponent<SkinnedMeshRenderer> ().material = MyMats [1];
		if (firstTimeClosing == true) {
			GetComponent<SkinnedMeshRenderer> ().material = MyMats [1];
			FlowerController.Play ("closing");
			firstTimeClosing = false;
		}
	//	Debug.Log ("NO MORE CLICKS MOFO");

	}

}
