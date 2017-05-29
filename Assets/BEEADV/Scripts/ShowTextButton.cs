using UnityEngine;
using System.Collections;

class ShowTextButton : MonoBehaviour
{
	private bool showText = false;
	// Create a bool to say whether to show the button or not

	void OnMouseDown()
	{
		if(!showText)
			showText = true;
		// If you clicked the object, set showText to true
	}


	void OnGUI()
	{
		if(showText)
		{
			// If you've clicked the object, show this button
			if(GUI.Button(new Rect(100,100,100,20), "Click To Close"))
				// If you click this button, set showText to false
				showText = false;
		}
	}
}

