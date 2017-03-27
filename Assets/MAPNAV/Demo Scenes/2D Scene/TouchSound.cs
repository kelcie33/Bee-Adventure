using UnityEngine;
using System.Collections;

public class TouchSound : MonoBehaviour {

	public AudioClip sample;
	
	void OnMouseDown () {
		GetComponent<AudioSource> ().clip = sample;
		GetComponent<AudioSource>().Play();
		print("touched");
	}
}
