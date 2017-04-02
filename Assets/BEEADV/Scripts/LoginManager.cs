using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Notes:
 * [1] Turns out to be a bug on Collaborate preview and will be fixed to 5.6 release according to Unity. Work around this, is to turn off the Collaborate service while working with UIs and turn it back on when finished
 * http://answers.unity3d.com/questions/1315372/unitycollaboration-parameter-assetguid-is-null-err.html
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

        var myUrl = "";

        // CONTINUE HERE
    }
}
