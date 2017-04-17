using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{ 
    public GameObject plane;
    private WebCamTexture mCamera = null;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Script has been started");
		plane = GameObject.FindWithTag ("Thing");

		mCamera = new WebCamTexture ();
		plane.GetComponent<Renderer>().material.mainTexture = mCamera;
		mCamera.Play ();

	}

	// Update is called once per frame
	void Update ()
	{

	}
}
