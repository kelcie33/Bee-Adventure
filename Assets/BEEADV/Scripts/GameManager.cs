using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Text
using UnityEngine.SceneManagement;
using System; // Convert

// **** WARNING ***
// Only associate this with the GameObject named GameManager in LoginScene
// Do not associate it with any other objects
// Otherwise, references to this object may not work


public class GameManager : MonoBehaviour
{
    public GameObject gameView;
    public string activeUser = "";
    public int activeUserScore = 1;
    public int activeUserHives = 0;

    private GameObject lowerPanel;
    private GameObject upperPanel;

    private List<GameObject> dupButtons = new List<GameObject>();

    // Variables to store camera transform data (i.e. position, rotation and scale)
    // since we need to restore camera back to last position when switching modes
    // Note we can't just use a transform variable, since Unity doesn't allow that
    private Vector3 lastArCameraPosition = new Vector3();
    private Quaternion lastArCameraRotation = new Quaternion();
    private Vector3 lastArCameraScale = new Vector3();
    private Vector3 lastGpsCameraPosition = new Vector3();
    private Quaternion lastGpsCameraRotation = new Quaternion();
    private Vector3 lastGpsCameraScale = new Vector3();

    private bool everBeenInGpsView = false;
    private bool everBeenInArView = false;

    // Use this for initialization before Start
    void Awake()
    {
        // Check whether GameManager script is attached to top-level GameObject
        // since that is the intended usage for this script
        if (transform.parent == null)
        {
            // Set the GameManager to remain alive and not be deleted
            // even when loading the next scene
            DontDestroyOnLoad(transform.gameObject);

            // Setup references to commonly used GameManager panels
            // to avoid calling Find multiple times (improves speed)
            lowerPanel = gameView.transform.Find("LowerPanel").gameObject;
            upperPanel = gameView.transform.Find("UpperPanel").gameObject;

            // Ask the SceneManager to call our function when "sceneLoaded" event happens
            // so that our code is called at the right time automatically
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
    }

    // Use this for initialization before Update
    void Start()
    {
        // Check whether GameManager script is attached to top-level GameObject
        // since that is the intended usage for this script
        if (transform.parent == null)
        {
            SetupNavigateButtons();

            Text scoreText = upperPanel.transform.Find("ScoreText").gameObject.GetComponent<Text>();
            Text hivesText = upperPanel.transform.Find("HivesText").gameObject.GetComponent<Text>();

            scoreText.text = "Level: " + activeUserScore;
            hivesText.text = "Hives: " + activeUserHives;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequested()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);

        // Check whether GameManager script is attached to top-level GameObject
        // since that is the intended usage for this script
        if (transform.parent == null)
        {
            // Set lowerpanel and upperpanel to be active or inactive (hidden)
            // based on the name of the newly loaded scene
            if (scene.name == "LoginScene2"
                || scene.name == "StoryScene")
            {
                lowerPanel.SetActive(false);
                upperPanel.SetActive(false);
            }
            else
            {
                lowerPanel.SetActive(true);
                upperPanel.SetActive(true);
            }

            // Set upperpanel switchbutton to default state or disable it
            // based on the name of the newly loaded scene
            Button switchButton = upperPanel.transform.Find("SwitchButton").gameObject.GetComponent<Button>();
            if (scene.name == "HomeScene")
			{
				switchButton.gameObject.SetActive(true);
                switchButton.enabled = true;

            }
            else
			{
				switchButton.enabled = false;
				switchButton.gameObject.SetActive(false);
            }

            /**
            // Set initial camera transform data (i.e. position, rotation and scale)
            // so we can restore the data when pressing the Switch button
            if (scene.name == "HomeScene")
            {
                // Make references to container GameObject in the HomeScene
                // so it can be checked later
                GameObject arView = GameObject.Find("HomeManager").transform.Find("ArView").gameObject;

                // Make reference to camera GameObject in the HomeScene
                // so it can be accessed later
                GameObject homeCamera = GameObject.Find("Camera").gameObject;

                // Now set the initial data
                // according to which view is currently active in HomeScene
                if (arView.activeInHierarchy)
                {
                    lastArCameraPosition = homeCamera.transform.position;
                    lastArCameraRotation = homeCamera.transform.rotation;
                    lastArCameraScale = homeCamera.transform.localScale;
                    everBeenInArView = true;
                }
                else
                {
                    lastGpsCameraPosition = homeCamera.transform.position;
                    lastGpsCameraRotation = homeCamera.transform.rotation;
                    lastGpsCameraScale = homeCamera.transform.localScale;
                    everBeenInGpsView = true;
                }
            }
            **/

            // Set title text in the upper panel
            // based on the name of the newly loaded scene
            Text titleText = upperPanel.transform.Find("TitleText").gameObject.GetComponent<Text>();
            titleText.text = scene.name;
        }
    }

    private void SetupNavigateButtons()
    {
        //GameObject templateButton = lowerPanel.transform.Find("TemplateButton").gameObject;
        //RectTransform templateRect = templateButton.GetComponent<RectTransform>();
        //float tx = templateRect.sizeDelta.x/2;
        //float ty = templateRect.sizeDelta.y/2;

        //Transform dupParent = templateButton.transform.parent.transform;
        //GameObject dupButton;

        //templateButton.SetActive(true);

        // We are going to dynamically setup duplicates of the template button
        // using our own code instead of relying on the inspector.  This happens a few times below.
        // Note we could make this a loop or function call if we wanted to improve it later.
        // - Instantiate: Makes a duplicate of template button
        // - Name: Sets the name to a new value
        // - AddListener: Sets the on click event to the LoadLevel with a scene name argument
        // - Text: Sets the text to a new value
        // - Add: Adds the duplicate button to a list (in case we need it later)

        /**
        // HomeButton
        dupButton = Instantiate(templateButton, new Vector3(tx, -ty, 0), Quaternion.identity, dupParent);
        dupButton.name = "HomeButton";
        dupButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { LoadLevel("HomeScene"); });
        dupButton.GetComponent<UnityEngine.UI.Text>().text = dupButton.name;
        dupButtons.Add(dupButton);

        // HivesButton
        dupButton = Instantiate(templateButton, new Vector3(tx + 100, -ty, 0), Quaternion.identity, dupParent);
		dupButton.name = "HivesButton";
		dupButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { LoadLevel("HiveScene1"); });
		dupButton.GetComponent<UnityEngine.UI.Text>().text = dupButton.name;
		dupButtons.Add(dupButton);

        // FactsButton
        dupButton = Instantiate(templateButton, new Vector3(tx + 200, -ty, 0), Quaternion.identity, dupParent);
        dupButton.name = "FactsButton";
        dupButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { LoadLevel("FactsScene"); });
        dupButton.GetComponent<UnityEngine.UI.Text>().text = dupButton.name;
        dupButtons.Add(dupButton);

        // ParksButton
        dupButton = Instantiate(templateButton, new Vector3(tx + 300, -ty, 0), Quaternion.identity, dupParent);
        dupButton.name = "ParksButton";
        dupButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { LoadLevel("ParksScene"); });
        dupButton.GetComponent<UnityEngine.UI.Text>().text = dupButton.name;
        dupButtons.Add(dupButton);
        **/

        /**
         * steps to adding new scenes!!
         *
         * #1.
            To ensure scenes can run, they need to be added under "File -> Build Settings" in the "Scenes in Build" window

            #2.
            To ensure scenes are added as bottom buttons, they need to be added under GameManager.cs in function SetupNavigateButtons by copying the code block for an existing scene button
        **/


        /**
        // ParksScene -- PLACEHOLDER FOR NEXT BUTTON
        dupButton = Instantiate(templateButton, new Vector3(tx + 400, -ty, 0), Quaternion.identity, dupParent); // todo: check +400 is x offset that is visible
        dupButton.name = "ParksScene"; // todo: change scene name
        dupButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { LoadLevel("ParksScene"); }); // todo: change scene name
        dupButton.GetComponent<UnityEngine.UI.Text>().text = dupButton.name;
        dupButtons.Add(dupButton);
        **/

        //templateButton.SetActive(false);
    }

    public void SwitchPressed()
    {
        Scene scene = SceneManager.GetActiveScene();

        // Check whether GameManager script is attached to top-level GameObject
        // since that is the intended usage for this script
        if (transform.parent == null)
        {
            if (scene.name == "HomeScene")
            {
                // Make reference to button text in the GameManager
                // so it can change the text
                Text switchButtonText = upperPanel.transform.Find("SwitchButton/Text").gameObject.GetComponent<Text>();

                // Make references to container GameObjects in the HomeScene
                // so they can be enabled or disabled later.
                GameObject arView = GameObject.Find("HomeManager").transform.Find("ArView").gameObject;
                GameObject gpsView = GameObject.Find("HomeManager").transform.Find("GpsView").gameObject;

                // Make reference to camera GameObject in the HomeScene
                // so it can be accessed later
                GameObject homeCamera = GameObject.Find("Camera").gameObject;
                

                // Enable or disable (i.e. toggle) the GameObjects in the HomeScene
                // based on whether they were active before and save/load the camera position data
                if (arView.activeInHierarchy)
                {
                    // Save the last camera position data for AR
                    lastArCameraPosition = homeCamera.transform.position;
                    lastArCameraRotation = homeCamera.transform.rotation;
                    lastArCameraScale = homeCamera.transform.localScale;
                    everBeenInArView = true;

                    switchButtonText.text = "Switch to AR"; // now we are in GPS
                    arView.SetActive(false);
                    gpsView.SetActive(true);

                    // Load the last camera position data for GPS
                    // and update reference to camera in case the view script moved it
                    if(everBeenInGpsView)
                    {
                        homeCamera = GameObject.Find("Camera").gameObject;
                        homeCamera.transform.position = lastGpsCameraPosition;
                        homeCamera.transform.rotation = lastGpsCameraRotation;
                        homeCamera.transform.localScale = lastGpsCameraScale;
                    }
                }
                else
                {
                    // Save the last camera position data for GPS
                    lastGpsCameraPosition = homeCamera.transform.position;
                    lastGpsCameraRotation = homeCamera.transform.rotation;
                    lastGpsCameraScale = homeCamera.transform.localScale;
                    everBeenInGpsView = true;

                    switchButtonText.text = "Switch to GPS"; // now in AR
                    arView.SetActive(true);
                    gpsView.SetActive(false);

                    // Load the last camera position data for AR
                    // and update reference to camera in case the view script moved it
                    if (everBeenInArView)
                    {
                        homeCamera = GameObject.Find("Camera").gameObject;
                        homeCamera.transform.position = lastArCameraPosition;
                        homeCamera.transform.rotation = lastArCameraRotation;
                        homeCamera.transform.localScale = lastArCameraScale;
                    }
                }
            }
        }
    }
}
