using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GenerateGameObject : MonoBehaviour {
	public string folderpath;
	private string donefile;
	private bool donefilefound;
	private bool modelfilepathfound;
	private bool modelrendered;
	private StreamReader r;
	private string modelfilepath;

	// Use this for initialization
	void Start () {
		donefile = Path.Combine(folderpath, "done.txt");
		donefilefound = false;
		modelfilepathfound = false;
		modelrendered = false;
		// Mesh m = new Mesh();
		// ObjImporter oi = new ObjImporter();
		// m = oi.ImportFile(filepath);

		// MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
		// MeshFilter mf = gameObject.AddComponent<MeshFilter>();
		// mf.mesh = m;
		// mr.material = new Material(Shader.Find("Standard"));
		// mr.material.color = Color.red;
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
		if (donefilefound && !File.Exists(donefile)) {
			Debug.Log("Lost file");
			donefilefound = false;
			modelfilepathfound = false;
			modelrendered = false;
		}
		if (!donefilefound && File.Exists(donefile)) {
			Debug.Log("Found file");
			donefilefound = true;
		}

		if (donefilefound && !modelfilepathfound) {
			r = new StreamReader(donefile);
			if ((modelfilepath = r.ReadLine()) != null) {
				Debug.Log(modelfilepath);
				modelfilepathfound = true;
			}
			r.Close();
		}

		if (donefilefound && modelfilepathfound && !modelrendered) {
			MakeObject(modelfilepath);
			modelrendered = true;
		}
	}
}
