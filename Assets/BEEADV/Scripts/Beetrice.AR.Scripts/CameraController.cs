using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{ 
    public GameObject plane;
    public WebCamTexture mCamera = null;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Script has been started");
		plane = GameObject.FindWithTag ("Thing");

		mCamera = new WebCamTexture ();
		plane.GetComponent<Renderer>().material.mainTexture = mCamera;
		//plane.transform.Rotate (new Vector3 (0, 180, 0));  // Compensate for camera rotation
		plane.transform.Rotate (new Vector3 (0, 270, 0));  // Compensate 180 deg webcam + 90 deg android
		mCamera.Play ();

	}

	// Update is called once per frame
	void Update ()
	{

	}
}
