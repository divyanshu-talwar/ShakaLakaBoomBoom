using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveSettings : MonoBehaviour {

	public Text ServerAddress;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void save() {
		Settings.ServerAddress = ServerAddress.text;
		Debug.Log("Server Address set to: " + Settings.ServerAddress);
		SceneManager.LoadScene("Menu");
	}
}
