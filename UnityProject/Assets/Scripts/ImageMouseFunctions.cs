using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEditor;

public class ImageMouseFunctions : MonoBehaviour {

	// Use this for initialization
	public GameObject overlayImage;
	public GameObject imageUploader;
	public void enableOverlay() {
		overlayImage.SetActive(true);
	}

	public void disableOverlay() {
		overlayImage.SetActive(false);
	}

	public void loadCreateObjectScene() {
		SceneManager.LoadScene("CreateObject");
	}

	public void loadCreateSceneScene() {
		SceneManager.LoadScene("CreateScene");
	}

	public void loadSettingsScene() {
		SceneManager.LoadScene("Settings");
	}

	public void loadHome() {
		SceneManager.LoadScene("Menu");
	}

	public void OpenFileDialog() {
		string path = EditorUtility.OpenFilePanel("Select Imaage", "", "png");
		Debug.Log(path);
		if (path == "") {return;}
		UploadImage xxx = imageUploader.GetComponent<UploadImage>();
		xxx.path = path;
		StartCoroutine(imageUploader.GetComponent<UploadImage>().Upload());
	}
}
