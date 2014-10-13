using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

	public static GameController controller;

	// Use this for initialization
	void Start () {
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;
		} else if (controller != this) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Save() {
		String fileName = "firstSavedGame";
		
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/" + fileName);
		GameData data = new GameData ();

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(string fileName) {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);
		GameData data = (GameData)bf.Deserialize (file);
		file.Close ();

	}
	
}

[Serializable]
class GameData {



}
