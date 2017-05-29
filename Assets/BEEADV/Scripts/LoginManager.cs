using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;  // Text
using UnityEngine.SceneManagement;

/**
 * Programming ideas:
 * 
 * 1. Manage all login activities in this scene.  Don't need to switch scenes since all these activities are related.
 * 
 * 2. Use multiple panels for login, forgot and register activities.  The panels can be enabled or disabled, based on which activity is happening.
 * 
 * 3. Use WWW class to communicate with PHP script on MySQL server.  It can send and receive messages.  Deviced to write PHP script with "</next>" between all data send back in PHP response message.  That way it is easily split by the Unity program.
 * 
 * 4. Once user account is validated, store it in a GameManager class that will be shared with other scenes so we don't forget who is playing.
 * 
 * 5. Use transform.find to find UI controls by name when they are needed, so they don't all have to be manually dragged onto the inspector panel.  Use awake function to find top-level controls before the start function.
 * 
 **/

/**
 * Workarounds:
 * 
 * 1. Problem: Collaborate crashes when editing UI controls
 * Fix: Turning Collaborate OFF until ready to publish an update
 * Reference: http://answers.unity3d.com/questions/1315372/unitycollaboration-parameter-assetguid-is-null-err.html
 * 
 * 2. Problem: Collaborate shows unexpected changes when switching between OS environments or Unity versions
 * Fix: Only publish update for specific files, not all files
 * 
 * 3. Problem: PHP response is long and difficult to process
 * Fix: Created PhpResponse class to organize the data
 * 
 * 4. Problem: Scene data like active user is destroyed when loading next scene
 * Fix: Stored data in GameManager and set it as DontDestroyOnLoad
 * 
 * 5. Problem: Game crashes sometimes when loading GPS data
 * Fix: Update to Unity 5.6.0 (tbd)
 * See: https://issuetracker.unity3d.com/issues/android-location-app-crashes-due-to-error-jstring-has-wrong-type-android-dot-location-dot-location
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
    public GameObject loginView;

    private GameObject loginPanel;
    private GameObject forgotPanel;
    private GameObject registerPanel;

    // Use this for initialization before Start
    void Awake()
    {
        loginPanel = loginView.transform.Find("LoginPanel").gameObject;
        forgotPanel = loginView.transform.Find("ForgotPanel").gameObject;
        registerPanel = loginView.transform.Find("RegisterPanel").gameObject;
    }

    // Use this for initialization before Update
    void Start()
    {
        // Ensure only certain panels are active
        loginPanel.SetActive(true); // Active
        forgotPanel.SetActive(false);
        registerPanel.SetActive(false);

        // Ensure some controls are disabled
        loginPanel.transform.Find("LoginButton/ResultText").gameObject.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {

    }

    // UI INTERACTION ================

    public void LoginPressed()
    {
        Text usernameEntered = loginPanel.transform.Find("UsernameField/EnteredText").gameObject.GetComponent<Text>();
        Text passwordEntered = loginPanel.transform.Find("PasswordField/EnteredText").gameObject.GetComponent<Text>();
        Text loginResult = loginPanel.transform.Find("LoginButton/ResultText").gameObject.GetComponent<Text>();

        // Print button press info to console
        Debug.Log("Called: LoginPressed"
            + "\nUsername: " + usernameEntered.text
            + "\nPassword: " + passwordEntered.text);

        PhpResponse myPhpResponse = ShowUserAccount(usernameEntered.text);
        if (myPhpResponse._userAccounts.Count == 0)
        {
            // Username doesn't exist, so indicate result
            loginResult.enabled = true;
            loginResult.text = "Username doesn't exist";
        }
        else if (myPhpResponse._userAccounts.Count > 1)
        {
            // Multiple usernames exist, so indicate database error
            loginResult.enabled = true;
            loginResult.text = "Database error";
        }
        else if ((myPhpResponse._userAccounts[0]._username != usernameEntered.text)
            || (myPhpResponse._userAccounts[0]._password != passwordEntered.text))
        {
            // Password doesn't match, so indicate result
            loginResult.enabled = true;
            loginResult.text = "Username and password don't match";
        }
        else
        {
            // Password does match, so save active user and load next scene
            loginResult.enabled = true;
            loginResult.text = "Username and password match";

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.activeUser = usernameEntered.text;

            string name = "StoryScene";
            Debug.Log("Level load requested for: " + name);
            SceneManager.LoadScene(name);
        }
    }

    public void LoginShowPressed()
    {
        loginPanel.transform.Find("UsernameField/EnteredText").gameObject.GetComponent<Text>().text = "";
        loginPanel.transform.Find("PasswordField/EnteredText").gameObject.GetComponent<Text>().text = "";

        loginPanel.SetActive(true); // Active
        forgotPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    public void ForgotPressed()
    {
        
    }

    public void ForgotShowPressed()
    {
        loginPanel.SetActive(false);
        forgotPanel.SetActive(true); // Active
        registerPanel.SetActive(false);
    }

    public void RegisterPressed()
    {
        Text usernameEntered = registerPanel.transform.Find("UsernameField/EnteredText").gameObject.GetComponent<Text>();
        Text passwordEntered = registerPanel.transform.Find("PasswordField/EnteredText").gameObject.GetComponent<Text>();
        Text emailEntered = registerPanel.transform.Find("EmailField/EnteredText").gameObject.GetComponent<Text>();
        Text registerResult = registerPanel.transform.Find("RegisterButton/ResultText").gameObject.GetComponent<Text>();

        // Print button press info to console
        Debug.Log("Called: RegisterPressed"
            + "\nUsername: " + usernameEntered.text
            + "\nPassword: " + passwordEntered.text
            + "\nEmail: " + emailEntered.text);

        PhpResponse myPhpResponse = ShowUserAccount(usernameEntered.text);
        if (myPhpResponse._userAccounts.Count == 1)
        {
            // Username exists, so indicate result
            registerResult.enabled = true;
            registerResult.text = "Username already exists";
        }
        else if (myPhpResponse._userAccounts.Count > 1)
        {
            // Multiple usernames exist, so indicate database error
            registerResult.enabled = true;
            registerResult.text = "Database error";
        }
        else
        {
            // Username doesn't exist, so try to register and confirm after
            myPhpResponse = RegisterUserAccount(usernameEntered.text, passwordEntered.text, emailEntered.text);
            myPhpResponse = ShowUserAccount(usernameEntered.text);
            if (myPhpResponse._userAccounts.Count == 0)
            {
                // Username doesn't exist, so indicate database error
                // so, indicate failure :(
                registerResult.enabled = true;
                registerResult.text = "Database error";
            }
            else if (myPhpResponse._userAccounts.Count > 1)
            {
                // Multiple usernames exist, so indicate database error
                registerResult.enabled = true;
                registerResult.text = "Database error";
            }
            else if ((myPhpResponse._userAccounts[0]._username != usernameEntered.text)
                || (myPhpResponse._userAccounts[0]._password != passwordEntered.text))
            {
                // Password doesn't match, so indicate database error
                registerResult.enabled = true;
                registerResult.text = "Database error";
            }
            else
            {
                // Password does match, so save active user and load next scene
                registerResult.enabled = true;
                registerResult.text = "Username and password match";

                GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                gameManager.activeUser = usernameEntered.text;

                string name = "StoryScene";
                Debug.Log("Level load requested for: " + name);
                SceneManager.LoadScene(name);
            }
        }
    }

    public void RegisterShowPressed()
    {
        registerPanel.transform.Find("UsernameField/EnteredText").gameObject.GetComponent<Text>().text = "";
        registerPanel.transform.Find("PasswordField/EnteredText").gameObject.GetComponent<Text>().text = "";
        registerPanel.transform.Find("EmailField/EnteredText").gameObject.GetComponent<Text>().text = "";

        loginPanel.SetActive(false);
        forgotPanel.SetActive(false);
        registerPanel.SetActive(true); // Active
    }

    // MYSQL PHP INTERACTION ================

    private PhpResponse ShowAllUserAccounts()
    {
        // Prepare WWW request
        var myForm = new WWWForm();
        myForm.AddField("action", "show_all_user_accounts");

        // Send WWW request
        string myUrl = "http://students.washington.edu/kelcie/bee-adventure/userAccount.php";
        WWW w = new WWW(myUrl, myForm);
        StartCoroutine(WaitForRequest(w)); // i.e. yield w;
        while (!w.isDone) { }

        // Process WWW response
        var receivedData = Regex.Split(w.data, "</next>");
        return new PhpResponse(receivedData);
    }

    private PhpResponse ShowUserAccount(string username)
    {
        // Prepare WWW request
        var myForm = new WWWForm();
        myForm.AddField("action", "show_user_account");
        myForm.AddField("username", username);

        // Send WWW request
        string myUrl = "http://students.washington.edu/kelcie/bee-adventure/userAccount.php";
        WWW w = new WWW(myUrl, myForm);
        StartCoroutine(WaitForRequest(w)); // i.e. yield w;
        while (!w.isDone) { }

        // Process WWW response
        var receivedData = Regex.Split(w.data, "</next>");
        return new PhpResponse(receivedData);
    }

    private PhpResponse RegisterUserAccount(string username, string password, string email)
    {
        // Prepare WWW request
        var myForm = new WWWForm();
        myForm.AddField("action", "register_user_account");
        myForm.AddField("username", username);
        myForm.AddField("password", password);
        myForm.AddField("email", email);

        // Send WWW request
        string myUrl = "http://students.washington.edu/kelcie/bee-adventure/userAccount.php";
        WWW w = new WWW(myUrl, myForm);
        StartCoroutine(WaitForRequest(w)); // i.e. yield w;
        while (!w.isDone) { }

        // Process WWW response
        var receivedData = Regex.Split(w.data, "</next>");
        return new PhpResponse(receivedData);
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


    public class PhpResponse
    {
        // Example:
        // mysqli_connect TRUE</next>show_user_account</next>SELECT * FROM `user_accounts` WHERE `username` = 'cooper'</next>mysqli_query TRUE</next>cooper</next>fancyfeast</next>cooperthecat@gmail.com</next>

        // Note:
        // Data in receivedData[] is separated by </next> markers
        // receivedData[0] mysqli_connect TRUE
        // receivedData[1] show_user_account
        // receivedData[2] SELECT * FROM `user_accounts` WHERE `username` = 'cooper'
        // receivedData[3] mysqli_query TRUE
        // receivedData[4] cooper
        // receivedData[5] fancyfeast
        // receivedData[6] cooperthecat@gmail.com
        // receivedData[7] (blank)

        // Note:
        // **** SECURITY RISK ****
        // Storing password in variable here is a security risk
        // It is better to validate in PHP (with no transmission)
        // instead of doing so in Unity (and transmitting over WWW)
        
        public bool _mysqliConnect;
        public string _action;
        public string _query;
        public bool _mysqliQuery;
        public List<UserAccount> _userAccounts;

        public PhpResponse(string[] receivedData)
        {
            Debug.Log("Process: " + receivedData.Length);
            for (var i = 0; i < receivedData.Length; i++)
            {
                Debug.Log("Received: " + receivedData[i]);
            }

            if (receivedData.Length > 0)
            {
                _mysqliConnect = (receivedData[0] == "mysqli_connect TRUE");
            }
            if (receivedData.Length > 1)
            {
                _action = receivedData[1];
            }
            if (receivedData.Length > 2)
            {
                _query = receivedData[2];
            }
            if (receivedData.Length > 3)
            {
                _mysqliQuery = (receivedData[3] == "mysqli_query TRUE");
            }
            _userAccounts = new List<UserAccount>();
            for (int i = 4; (i + 3) < receivedData.Length; i += 3)
            {
                _userAccounts.Add(new UserAccount(
                    receivedData[i],
                    receivedData[i + 1],
                    receivedData[i + 2]));
            }
        }
    }


    public class UserAccount
    {
        public string _username;
        public string _password;
        public string _email;

        public UserAccount(
            string username,
            string password,
            string email)
        {
            _username = username;
            _password = password;
            _email = email;
        }
    }
}
