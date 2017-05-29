using UnityEngine;
using System.Collections;

public class Flipping : MonoBehaviour 
{
	private Ray m_Ray;
	private RaycastHit m_RayCastHit;
	private TouchableObject m_CurrentTouchableObject;
	private Animator BeetriceController;
	public GameObject BeetriceAnimations003;


	void Update () {
		BeetriceController = BeetriceAnimations003.GetComponent<Animator>();
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
						BeetriceController.Play ("flip");
					} else {
						if (m_CurrentTouchableObject) {
							m_CurrentTouchableObject = null;
							BeetriceController.Play ("static");
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
		BeetriceController.Play ("flip");
	}

}

