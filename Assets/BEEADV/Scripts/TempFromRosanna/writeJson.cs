#if false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson; 
using System.IO;

public class writeJson : MonoBehaviour {
	
	public Login user = new Login (0, "Anna","password","itchycat@gmail.com",0); 
	JsonData userJson; 
	// Use this for initialization
	void Start () {
		userJson = JsonMapper.ToJson (user); 
		//Debug.Log (userJson); 
		File.WriteAllText(Application.dataPath + "/user.json", userJson.ToString()); 
	}
		
}

	public class Login {
		public int id; 
		public string name; 
		public string username; 
		public string password;
		public string email; 
		public int score; 
	
		public Login (int id, string name, string password, string email, int score) 
		{
			this.id = id; 
			this.name = name; 
			this.password = password; 
			this.email = email; 
			this.score = score;	
		} 
	} 
#endif