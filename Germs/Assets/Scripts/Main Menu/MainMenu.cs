using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Gui element sizes
	private Vector2 buttonSize;
	private Vector2 menuWindowSize;
	private Vector2 menuPosition;
	
	private int buttons = 5;
	private string showing = "menu"; //"menu" "loadgame" "savegame" etc


	// Use this for initialization
	void Start () {
		buttonSize = new Vector2 (Screen.width/5, Screen.height/9);
		menuWindowSize = new Vector2 (buttonSize.x, buttonSize.y * buttons); 
		menuPosition = new Vector2 ((Screen.width - buttonSize.x)/2, (Screen.height-buttonSize.y*buttons)/2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

		if(showing.Equals("menu")) {
			createMenu();
		} else if(showing.Equals("loadgame")) {
			createLoadGameMenu();
		} else if(showing.Equals("savegame")) {
			createSaveGameMenu();
		}


	}

	private void createMenu() {

		// Create menu
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y, buttonSize.x, buttonSize.y), "New Game")) {
		}
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y+buttonSize.y * 1, buttonSize.x, buttonSize.y), "Load Game")) {
			this.showing = "loadgame";
		}
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y+buttonSize.y * 2, buttonSize.x, buttonSize.y), "Save Game")) {
			this.showing = "savegame";
		}
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y+buttonSize.y * 3, buttonSize.x, buttonSize.y), "Options")) {
		}
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y+buttonSize.y * 4, buttonSize.x, buttonSize.y), "Quit Game")) {
		}
	}

	private void createLoadGameMenu() {
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y- buttonSize.y/3, buttonSize.x/3, buttonSize.y/3), "Back")) {
			this.showing = "menu";
		}
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y, buttonSize.x, buttonSize.y), "SavedGame1")) {
			GameController.controller.Load("SavedGame1");
		}
	}

	private void createSaveGameMenu() {
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y- buttonSize.y/3, buttonSize.x/3, buttonSize.y/3), "Back")) {
			this.showing = "menu";
		}
	}
}
