using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class loadingGIF : MonoBehaviour {

	public Sprite[] frames;
	public int framesPerSecond;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int index = (int)(Time.time * framesPerSecond) % frames.Length;
		GetComponent<Image>().sprite = frames[index];
	}
}
