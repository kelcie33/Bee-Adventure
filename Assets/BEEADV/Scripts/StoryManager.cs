using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	public Sprite sprite6;
	public Sprite sprite7;

	private List<Sprite> mySpriteList;
	private GameObject slideshowSpriteGo;
	private int slideshowSpriteNumber = 0;

	// Use this before initialization
	void Awake()
	{
		if (name == "SlideshowSprite")
		{
			slideshowSpriteGo = GameObject.Find("SlideshowSprite").gameObject;
			mySpriteList = new List<Sprite> { sprite1, sprite2, sprite3, sprite4, sprite5, sprite6, sprite7 };
		}
	}

	// Use this for initialization
	void Start()
	{
		if (name == "SlideshowSprite")
		{
			slideshowSpriteGo.GetComponent<SpriteRenderer> ().sprite = mySpriteList [slideshowSpriteNumber];
			slideshowSpriteNumber = slideshowSpriteNumber + 1;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (name == "SlideshowSprite")
		{
			if (Input.GetMouseButtonDown (0) /* PC Input */
				|| (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) /* Mobile Input */) 
			{
				if ((slideshowSpriteNumber + 1) == mySpriteList.Count)
				{
					LoadLevel ("HomeScene");
				}
				else
				{
					slideshowSpriteGo.GetComponent<SpriteRenderer> ().sprite = mySpriteList [slideshowSpriteNumber];
					slideshowSpriteNumber = (slideshowSpriteNumber + 1) % mySpriteList.Count;
				}
			}
		}
    }

    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);
    }

}
