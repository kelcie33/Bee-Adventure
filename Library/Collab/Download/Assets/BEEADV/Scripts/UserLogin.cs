
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
public class UserLogin : MonoBehaviour {
	public GameObject username;
	public GameObject password;
	public GameObject name; 
	public string Name;
	private string Password;
	public string[] lines;
	private string decryptedPass;

	public void LoginButton(){
		bool N = false;
		bool P = false;
		if (Name != ""){
			if (System.IO.File.Exists(@"E:\UnityTestFolder\"+Name+".txt")){
				lines = File.ReadAllLines(@"E:\UnityTestFolder\"+Name+".txt");
				N = true;
			}  else {
				Debug.LogWarning("Username Field is Incorrect");
			}
		}  else {
			Debug.LogWarning("Username Field is empty");
		}
		if (Password != ""){
			if (System.IO.File.Exists(@"E:\UnityTestFolder\"+Name+".txt")){
				int i = 1;
				foreach(char c in lines[2]){
					i++;
					char decrypted = (char)(c / i);
					decryptedPass += decrypted.ToString();
				}
				if (Password == decryptedPass){
					P = true;
				}  else {
					Debug.LogWarning("Password Field is Incorrect");
					decryptedPass = "";
				}
			}  else {
				Debug.LogWarning("Password Field is Incorrect");
				decryptedPass = "";
			}
		}  else {
			Debug.LogWarning("Password Field is empty");
			decryptedPass = "";
		}
		if (N == true&& P == true){
			print("Login Successful");
			username.GetComponent<InputField>().text = "";
			password.GetComponent<InputField>().text = "";
			Application.LoadLevel("Start Menu");
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)){
			if (username.GetComponent<InputField>().isFocused){
				password.GetComponent<InputField>().Select();
			}
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			if (Password != ""&&Name != ""){
				LoginButton();
			}
		}
		Name = username.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
	}
}
