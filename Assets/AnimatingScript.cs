using UnityEngine;
using System.Collections;
using System.IO;                    // For parsing text file, StringReader
using System.Collections.Generic;    // For List
using UnityEngine.UI;



public class AnimatingScript : MonoBehaviour
{
	public TextAsset wordFile;                                // Text file (assigned from Editor)
	private List<string> lineList = new List<string>();        // List to hold all the lines read from the text file
	public Text Quote;
	private bool showText = false;

	RectTransform rt;
	Text txt;

	private Ray m_Ray;
	private RaycastHit m_RayCastHit;
	private TouchableObject m_CurrentTouchableObject;
	private Animator BeetriceController;
	public GameObject BeetriceAnimations003;

	void Start()
	{
		ReadWordList();
		//	Text Quote = RandomLine.GetComponent<Text>();
		Quote = GetComponent<UnityEngine.UI.Text>();
		Debug.Log("Random line from list: " + GetRandomLine());
		//	Quote = GameObject.Find("Text").GetComponent<Text>();
		Quote.text = "" + GetRandomLine();

		rt = gameObject.GetComponent<RectTransform>(); // Acessing the RectTransform 
		txt = gameObject.GetComponent<Text>(); // Accessing the text component

	}

	void Update () {
		BeetriceController = GetComponent<Animator>();
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
						BeetriceController.Play ("headshake");
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


	void OnMouseDown()
	{
		Quote = GetComponent<UnityEngine.UI.Text>();
	//	ReadWordList();
		//Debug.Log("Random line from list: " + GetRandomLine());
		//	Quote = GameObject.Find("Text").GetComponent<Text>();
		Quote.text = "" + GetRandomLine();
		Debug.Log("Random line from list: " + Quote.text);
		if(!showText)
			showText = true;
		// If you clicked the object, set showText to true
	}

	public void ReadWordList()
	{
		// Check if file exists before reading
		if (wordFile)
		{
			string line;
			StringReader textStream = new StringReader(wordFile.text);

			while((line = textStream.ReadLine()) != null)
			{
				// Read each line from text file and add into list
				lineList.Add(line);
			}

			textStream.Close();
		}
	}
	void OnGUI()
	{
		if(showText)
		{
			GUI.skin.button.wordWrap = true;
			// If you've clicked the object, show this button
			//rt.sizeDelta = new Vector2(rt.rect.width, txt.preferredHeight); // Setting the height to equal the height of text
			if(GUI.Button(new Rect(75, 200 ,150,150), Quote.text))
			//if(GUI.Button(new Rect(100,100,150,150), Quote.text))
				// If you click this button, set showText to false
				showText = false;
		}
	}


	public string GetRandomLine()
	{
		// Returns random line from list
		//Quote = GetComponent<UnityEngine.UI.Text>();
		//print(wordFile.text);
		
		int animation = Random.Range(0, lineList.Count);
		Debug.Log ("animation " + lineList.Count);
		if (animation > 13) {
			BeetriceController.Play ("talking angry");
			BeetriceController.GetParameter (1);
		} else{
			BeetriceController.Play ("talking happy");
			BeetriceController.GetParameter (1);
		}

		return lineList[animation];
		//return lineList[Random.Range(0, lineList.Count)];
	}
}