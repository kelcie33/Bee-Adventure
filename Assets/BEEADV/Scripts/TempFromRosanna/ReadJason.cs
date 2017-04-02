#if false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using LitJson; 

public class ReadJason : MonoBehaviour {
	private string jsonString; 
	private JsonData itemData; 

	// Use this for initialization
	void Start () {
		jsonString = File.ReadAllText(Application.dataPath + "/Resources/Items.json"); 
		itemData = JsonMapper.ToObject(jsonString);
		//Debug.Log(itemData["users"][0]["username"]); 
		Debug.Log(GetItem ("wondercat", "users")["username"]); 
	}
	JsonData GetItem(string name, string type)
	{
		for (int i = 0; i < itemData[type].Count; i++) {
			
			if (itemData[type][i]["name"].ToString() == name || itemData[type][i]["username"].ToString()==name)
				return itemData[type][i];
		}
			return null; 
		}
	}

//light weapon power
#endif