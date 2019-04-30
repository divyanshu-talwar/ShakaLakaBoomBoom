using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ColorObj : MonoBehaviour {

	// Use this for initialization

	public string filepath;
	void Start () {
		// gen(filepath);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void gen(string path) {
		Vector3[] vertices;
		Color[] colors;
		int[] triangles;

		StreamReader stream = File.OpenText(path);
		string entireText = stream.ReadToEnd();
		stream.Close();

		int numberOfVertices = 0, numberOfFaces = 0;
		using (StringReader reader = new StringReader(entireText)) {
			string currentLine = reader.ReadLine();
			
			while (currentLine != null) {
				currentLine = currentLine.Trim();
				char[] delimiter = {' '};
				string[] parts = currentLine.Split(delimiter);
				switch(parts[0]) {
					case "v":
							numberOfVertices += 1;
							break;
					
					case "f":
							numberOfFaces += 1;
							break;
				}

				currentLine = reader.ReadLine();
			}
		}

		vertices = new Vector3[numberOfVertices];
		colors = new Color[numberOfVertices];
		triangles = new int[numberOfFaces * 3];

		int vertexIndex = 0, faceIndex = 0, colorIndex = 0;

		using (StringReader reader = new StringReader(entireText)) {
			string currentLine = reader.ReadLine();

			float x, y, z, r, g, b;
			int i1, i2, i3;
			
			while (currentLine != null) {
				currentLine = currentLine.Trim();
				char[] delimiter = {' '};
				string[] parts = currentLine.Split(delimiter);
				switch(parts[0]) {
					case "v":
							x = float.Parse(parts[1].Trim());
							y = float.Parse(parts[2].Trim());
							z = float.Parse(parts[3].Trim());
							vertices[vertexIndex] = new Vector3(x, y, z);
							vertexIndex++;
							break;
					
					case "f":
							i1 = int.Parse(parts[1].Trim()) - 1;
							i2 = int.Parse(parts[2].Trim()) - 1;
							i3 = int.Parse(parts[3].Trim()) - 1;
							triangles[faceIndex] = i1; faceIndex++;
							triangles[faceIndex] = i2; faceIndex++;
							triangles[faceIndex] = i3; faceIndex++;
							break;
					
					case "c":
							r = float.Parse(parts[1].Trim());
							g = float.Parse(parts[2].Trim());
							b = float.Parse(parts[3].Trim());
							colors[colorIndex] = new Color(r, g, b);
							colorIndex++;
							break;
				}
				currentLine = reader.ReadLine();
			}
		}

		Mesh m = new Mesh();
		m.vertices = vertices;
		m.triangles = triangles;
		m.colors = colors;

		m.RecalculateBounds();

		GameObject xxx = new GameObject(path);
		xxx.transform.parent = gameObject.transform;
		xxx.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
		xxx.transform.localPosition = new Vector3(0f, 0f, 0f);
        xxx.gameObject.tag = "Active";
        MeshRenderer mr = xxx.AddComponent<MeshRenderer>();
		MeshFilter mf = xxx.AddComponent<MeshFilter>();
		mf.mesh = m;
		mr.material = new Material(Shader.Find("Particles/Standard Surface"));
		// mr.material.color = Color.red;
	}
}
