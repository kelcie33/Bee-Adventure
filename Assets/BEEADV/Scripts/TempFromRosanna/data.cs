﻿#if false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour {

	public string[] items;

	IEnumerator Start(){
		WWW data = new WWW("http://localhost/beeadventurous/data.php");
		yield return data;
		string itemsDataString = data.text;
		print (itemsDataString);
		items = itemsDataString.Split(';');
		print(GetDataValue(items[0], "Username:"));
		print(GetDataValue(items[0], "Password:"));
	}

	string GetDataValue(string data, string index){
		string value = data.Substring(data.IndexOf(index)+index.Length);
		if(value.Contains("|"))value = value.Remove(value.IndexOf("|"));
		return value;
	}


}

#endif