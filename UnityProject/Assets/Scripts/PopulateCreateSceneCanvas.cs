using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine;

public class PopulateCreateSceneCanvas : MonoBehaviour {
	
	public static Texture2D LoadPNG(string filePath) {

		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath)) {
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(10, 10);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}

	public GameObject buttonTemplate;
	public GameObject canvas;
	public GameObject ColorOBJLoader;

	Dictionary<string, string[]> allobjects;
	int buttoncount = 0;
	int leftOffset = 15;
	int width = 100;
	float scale = 0.8f;

	// Use this for initialization
	void Start () {
		allobjects = AllObjects.dict;
		foreach(KeyValuePair<string, string[]> entry in allobjects) {
			MakeButton(entry.Key, entry.Value);
			buttoncount++;
		}
		
		//Testing
		// string[] x = {"C:\\Users\\shubhangsati\\Desktop\\icons\\cat.png", "cat.obj"};
		// MakeButton("Cat", x);
		// buttoncount++;

		// string[] y = {"C:\\Users\\shubhangsati\\Desktop\\icons\\shield.png", "shield.obj"};
		// MakeButton("Shield", y);
		// buttoncount++;
	}

	void MakeButton(string name, string[] details) {
		GameObject bt = Instantiate(buttonTemplate, transform.position, transform.rotation);
		bt.transform.localScale = new Vector3(scale, scale, scale);
		
		RectTransform rt = bt.GetComponent<RectTransform>();
		rt.anchoredPosition = new Vector2((leftOffset + width) * buttoncount + leftOffset, 30);
		bt.GetComponentInChildren<Text>().text = name;
				
		Texture2D t = LoadPNG(details[0]);
		TextureScale.Bilinear(t, 100, 100);
		bt.GetComponent<Image>().sprite = Sprite.Create(t, rt.rect, Vector2.zero);
		bt.transform.SetParent(canvas.transform);

		bt.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => CreateObject(details));
	}

	public void CreateObject(string[] details) {
		ColorOBJLoader.GetComponent<ColorObj>().gen(details[1]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
