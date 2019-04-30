using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using UnityEngine;

public class UploadImage : MonoBehaviour {

	public GameObject loadingImage;
	public GameObject uploadedText;
	public string path;
	public string uploadURL;
	public LoadFromURL lfu;

	// Use this for initialization
	void Start () {
        
	}

	public IEnumerator Upload() {

		Debug.Log("New uploader created");
		loadingImage.SetActive(true);

		WWWForm form = new WWWForm();
		byte[] filedata = File.ReadAllBytes(path);
		form.AddBinaryData("file", filedata, path, "image/png");

		WWW w = new WWW(uploadURL, form);
		yield return w;
		if(w.progress == 1) {
			uploadedText.SetActive(true);
			lfu.server = Settings.ServerAddress;
			lfu.HACKKK();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
