﻿using UnityEngine;
using System.Collections;
using System.IO;                    // For parsing text file, StringReader
using System.Collections.Generic;    // For List
using UnityEngine.UI;



public class Mixture : MonoBehaviour
{
	public TextAsset wordFile;                                // Text file (assigned from Editor)
	private List<string> lineList = new List<string>();        // List to hold all the lines read from the text file
	public Text Quote;
	private bool showText = false;

	RectTransform rt;
	Text txt;

	void Start()
	{
		//	Text Quote = RandomLine.GetComponent<Text>();
		Quote = GetComponent<UnityEngine.UI.Text>();
		ReadWordList();
		Debug.Log("Random line from list: " + GetRandomLine());
		//	Quote = GameObject.Find("Text").GetComponent<Text>();
		Quote.text = "" + GetRandomLine();

		rt = gameObject.GetComponent<RectTransform>(); // Acessing the RectTransform 
		txt = gameObject.GetComponent<Text>(); // Accessing the text component

	}
	void OnMouseDown()
	{
		Quote = GetComponent<UnityEngine.UI.Text>();
		ReadWordList();
		Debug.Log("Random line from list: " + GetRandomLine());
		//	Quote = GameObject.Find("Text").GetComponent<Text>();
		Quote.text = "" + GetRandomLine();
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
			rt.sizeDelta = new Vector2(rt.rect.width, txt.preferredHeight); // Setting the height to equal the height of text
			if(GUI.Button(new Rect(50,100,150,150), Quote.text))
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
		//Get animation done here
		return lineList[animation];
		//return lineList[Random.Range(0, lineList.Count)];
	}
}