using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    public Sprite sprite1;

    private GameObject slideshowSpriteGo;

    // Use this before initialization
    void Awake()
    {
        if (name == "SlideshowSprite")
        {
            slideshowSpriteGo = GameObject.Find("SlideshowSprite").gameObject;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (name == "SlideshowSprite")
        {
            slideshowSpriteGo.GetComponent<SpriteRenderer>().sprite = sprite1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (name == "SlideshowSprite")
        {
            if (Input.GetMouseButtonDown(0) /* PC Input */
                || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) /* Mobile Input */)
            {
                LoadLevel("LoginScene2");
            }
        }
    }

    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);
    }

}
