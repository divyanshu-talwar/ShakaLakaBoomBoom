using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

public class LoadFromURL : MonoBehaviour {

	public string server;
	string address;
	public GameObject loadingGIF;
	public GameObject loadingText;
	public UploadImage imageUploader;
	WWW www;


	// Use this for initialization
	void Start () {
		server = Settings.ServerAddress;
	}

	public void HACKKK() {
		char[] delimiter = {'.'};
		string[] parts = imageUploader.path.Split(delimiter);
		string urllll = parts[0].Replace("/", "_").Replace(":", "") + ".obj";
		BeginDownload(urllll);
	}

	
	public void BeginDownload(string url) {
		address = server + ":8000/" + url;
		// Debug.Log("Downloading from: " + address);
		// address = "127.0.0.1:1234/bottle.obj";
		// StartCoroutine(download());
		
		StartCoroutine(DownloadFile());

		// DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
		// 	StartCoroutine(download());
		// });
	}

	
	IEnumerator DownloadFile() {
		yield return new WaitForSeconds(10);
		UnityWebRequest uwr;
		uwr = new UnityWebRequest(address);
		uwr.downloadHandler = new DownloadHandlerBuffer();
		yield return uwr.SendWebRequest();

		while (uwr.isNetworkError || uwr.isHttpError) {
			Debug.Log(uwr.error);
			yield return new WaitForSeconds(5);
			
			uwr = new UnityWebRequest(address);
			uwr.downloadHandler = new DownloadHandlerBuffer();
			yield return uwr.SendWebRequest();
		}
		byte[] results = uwr.downloadHandler.data;
		Debug.Log("Downloaded");


		char[] delimiter = {'/'};
		char[] delimiter2 = {'.'};
		string[] segments = address.Split(delimiter);
		string filename = segments[segments.Length - 1];
		File.WriteAllBytes(filename, results);
		Debug.Log("Done");
		string[] details = new string[2];
		details[0] = imageUploader.path;
		details[1] = filename;
		Debug.Log(details[0] + " " + details[1]);
		// AllObjects.ao.dict.
		string[] partsss = imageUploader.path.Split(delimiter);
		string key = partsss[partsss.Length - 1].Split(delimiter2)[0];
		Debug.Log("KEY = " + key);
		AllObjects.dict.Add(key, details);
		loadingGIF.SetActive(false);
		loadingText.GetComponent<Text>().text = "Download Complete";
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("Menu");
	}




	// void MakeObject(string path, string colorpath) {
	// 	Mesh m = new Mesh();
	// 	ObjImporter oi = new ObjImporter();
	// 	m = oi.ImportFile(path, colorpath);
	// 	GameObject x = new GameObject(path);
	// 	x.transform.parent = gameObject.transform;
	// 	MeshRenderer mr = x.AddComponent<MeshRenderer>();
	// 	MeshFilter mf = x.AddComponent<MeshFilter>();
	// 	mf.mesh = m;
	// 	mr.material = new Material(Shader.Find("Standard"));
	// 	mr.material.color = Color.red;
	// }
	
	// Update is called once per frame
	void Update () {
		
	}
}
