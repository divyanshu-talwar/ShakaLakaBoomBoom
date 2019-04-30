using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour {

	// Use this for initialization

	public GameObject[] listOfObjects;
	void Start () {
		foreach(GameObject g in listOfObjects) {
			g.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
