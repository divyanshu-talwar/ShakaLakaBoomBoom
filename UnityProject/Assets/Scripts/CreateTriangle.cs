using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTriangle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh triangle = MakeTriangle();
		GameObject x = new GameObject("triangle");
		MeshRenderer mr = x.AddComponent<MeshRenderer>();
		MeshFilter mf = x.AddComponent<MeshFilter>();
		mf.mesh = triangle;
		mr.material = new Material(Shader.Find("Particles/Standard Surface"));
		// mr.material.color = Color.red;
	}

	Mesh MakeTriangle() {
		Vector3[] vertices = new Vector3[3];
		vertices[0] = new Vector3(0, 0, 0);
		vertices[1] = new Vector3(10, 0, 0);
		vertices[2] = new Vector3(10, 10, 0);

		Vector3[] normals = new Vector3[3];
		normals[0] = new Vector3(0, 0, 1);
		normals[1] = new Vector3(0, 0, 1);
		normals[2] = new Vector3(0, 0, 1);

		Vector2[] uv = new Vector2[3];
		uv[0] = new Vector2(0, 0);
		uv[1] = new Vector2(0, 1);
		uv[2] = new Vector2(1, 0);

		int[] triangles = new int[3];
		triangles[0] = 0;
		triangles[1] = 1;
		triangles[2] = 2;
		// triangles[3] = 0;
		// triangles[4] = 2;
		// triangles[5] = 1;

		Color[] colors = new Color[3];
		colors[0] = new Color(1, 0, 0);
		colors[1] = Color.blue;
		colors[2] = Color.green;

		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		// mesh.normals = normals;
		mesh.triangles = triangles;
		mesh.colors = colors;

		mesh.RecalculateBounds();

		return mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
