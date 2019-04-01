using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadFromURL : MonoBehaviour {

	public string server;
	string address;

	// Use this for initialization
	void Start () {

	}

	
	public void BeginDownload(string url) {
		address = server + "/" + url;
		// address = "127.0.0.1:1234/bottle.obj";
		DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
			StartCoroutine(download());
		});
	}

	IEnumerator download() {
		WWW www = new WWW(address);
		char[] delimiter = {'/'};
		string[] segments = address.Split(delimiter);
		string filename = segments[segments.Length - 1];
		yield return www;
		File.WriteAllBytes(filename, www.bytes);
		Debug.Log("Done");
		MakeObject(filename);
	}


	void MakeObject(string path) {
		Mesh m = new Mesh();
		ObjImporter oi = new ObjImporter();
		m = oi.ImportFile(path);
		GameObject x = new GameObject(path);
		x.transform.parent = gameObject.transform;
		MeshRenderer mr = x.AddComponent<MeshRenderer>();
		MeshFilter mf = x.AddComponent<MeshFilter>();
		mf.mesh = m;
		mr.material = new Material(Shader.Find("Standard"));
		mr.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
