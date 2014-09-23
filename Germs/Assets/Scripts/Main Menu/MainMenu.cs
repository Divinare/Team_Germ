using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.Box(new Rect(Screen.width /2 - Screen.width/5,Screen.height /2 - Screen.height/3,Screen.width/3,Screen.height - Screen.height/3), "Main Menu");
		if (GUI.Button (new Rect (Screen.width /2 - Screen.width/5, Screen.height / 2 - Screen.height/4, Screen.width/3, Screen.height/8), "New Game")) {
		}
		if (GUI.Button (new Rect (Screen.width /2 - Screen.width/5, Screen.height / 2 - Screen.height/8, Screen.width/3, Screen.height/8), "Load Game")) {
		}
		if (GUI.Button (new Rect (Screen.width /2 - Screen.width/5, Screen.height / 2, Screen.width/3, Screen.height/8), "Save Game")) {
		}
		if (GUI.Button (new Rect (Screen.width /2 - Screen.width/5, Screen.height / 2 + Screen.height/8, Screen.width/3, Screen.height/8), "Quit Game")) {
		}
	}
}
