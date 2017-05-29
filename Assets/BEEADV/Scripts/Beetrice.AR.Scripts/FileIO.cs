using UnityEngine;
using System.Collections;
using System.IO;                    // For parsing text file, StringReader
using System.Collections.Generic;    // For List
using UnityEngine.UI;



public class FileIO : MonoBehaviour
{
	public TextAsset wordFile;                                // Text file (assigned from Editor)
	private List<string> lineList = new List<string>();        // List to hold all the lines read from the text file
	public Text Quote;


	private bool showText = false;
	RectTransform rt;


	void Start()
	{
	//	Text Quote = RandomLine.GetComponent<Text>();
		Quote = GetComponent<UnityEngine.UI.Text>();
		ReadWordList();
		Debug.Log("Random line from list: " + GetRandomLine());
	//	Quote = GameObject.Find("Text").GetComponent<Text>();
		Quote.text = "" + GetRandomLine();
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



	public string GetRandomLine()
	{
		// Returns random line from list
		//Quote = GetComponent<UnityEngine.UI.Text>();
		//print(wordFile.text);
		return lineList[Random.Range(0, lineList.Count)];
	}
}
