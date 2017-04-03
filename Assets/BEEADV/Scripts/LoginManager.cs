using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/**
 * Workarounds:
 * 
 * 1. Collaborate may crash when editing UI controls (known issue)
 * So, I turned Collaborate OFF until ready to publish an update
 * http://answers.unity3d.com/questions/1315372/unitycollaboration-parameter-assetguid-is-null-err.html
 * 
 * 2. Collaborate may show unexpected changes when switching from Windows to Mac
 * So, I only publish specific files instead of publishing all files
 * 
 **/

/**
 * References:
 * [1] PHP MySQL vs. Unity
 * http://www.cs.vu.nl/~eliens/.CREATE/local/essay/09/nm2/klitsie-joost.pdf
 * 
 * [2] Tutorial: Unity and PHP login script - simple but useful
 * https://forum.unity3d.com/threads/tutorial-unity-and-php-login-script-simple-but-useful.24721/
 * 
 **/

public class LoginManager : MonoBehaviour
{

    public UnityEngine.UI.Text usernameEntered;
    public UnityEngine.UI.Text passwordEntered;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void LoginPressed()
    {
        Debug.Log("Called: LoginPressed");
        Debug.Log("Username: " + usernameEntered.text);
        Debug.Log("Password: " + passwordEntered.text);

		ShowUserAccount ();
    }

    private void SubmitUserAccount()
    {
        string username = usernameEntered.text;
        string password = passwordEntered.text;
        string email = "temp@gmail.com"; // TODO

        var myForm = new WWWForm();
        myForm.AddField("action", "submit_useraccount");
        myForm.AddField("username", username);
        myForm.AddField("password", password);
        myForm.AddField("email", email);

		string myUrl = "http://students.washington.edu/kelcie/bee-adventure/userAccount.php";
		WWW w = new WWW (myUrl, myForm);
		StartCoroutine(WaitForRequest(w)); // i.e. yield w;
    }

	private void ShowUserAccount()
	{
		string username = usernameEntered.text;

		// Prepare WWW request
		var myForm = new WWWForm ();
		myForm.AddField ("action", "show_useraccount");
		myForm.AddField ("username", username);

		// Send WWW request
		string myUrl = "http://students.washington.edu/kelcie/bee-adventure/userAccount.php";
		WWW w = new WWW (myUrl, myForm);
		StartCoroutine(WaitForRequest(w)); // i.e. yield w;
		while(!w.isDone) {}

		// Process WWW response
		var receivedData = Regex.Split(w.data, "</next>");
		for(var i = 0; i < receivedData.Length; i++)
		{
			Debug.Log("Received: " + receivedData[i]);
		}
	}

	private IEnumerator WaitForRequest(WWW w)
	{
		yield return w;

		// check for errors
		if (w.error == null)
		{
			Debug.Log("WWW Ok!: " + w.data);
		}
		else
		{
			Debug.Log("WWW Error: "+ w.error);
		}    
	}
}
