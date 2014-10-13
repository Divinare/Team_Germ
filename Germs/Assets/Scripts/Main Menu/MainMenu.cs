﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Gui element sizes
	private Vector2 buttonSize;
	private Vector2 menuWindowSize;
	private Vector2 menuPosition;
	
	private int buttons = 5;
	private string showing = "menu"; //"menu" "loadgame" "savegame" "exitClicked" etc


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

		if (showing.Equals ("menu")) {
				createMenu ();
		} else if (showing.Equals ("loadgame")) {
				createLoadGameMenu ();
		} else if (showing.Equals ("savegame")) {
				createSaveGameMenu ();
		} else if (showing.Equals ("exitClicked")) {
				createExitConfirmWindow();
		}


	}

	private void createMenu() {

		// Create menu
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y, buttonSize.x, buttonSize.y), "New Game")) {
			Application.LoadLevel ("Map");
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
			this.showing = "exitClicked";
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

	private void createExitConfirmWindow() {
		Vector2 quitWindowSize = new Vector2(300,100);
		Vector2 quitWindowPos = new Vector2((Screen.width-quitWindowSize.x)/2, (Screen.height-quitWindowSize.y)/2);
		Vector2 quitWindowButton = new Vector2(60, 40);
		GUI.Box (new Rect (quitWindowPos.x, quitWindowPos.y, quitWindowSize.x, quitWindowSize.y), "Do you really wish to exit?");
		if (GUI.Button (new Rect (quitWindowPos.x+quitWindowButton.x, quitWindowPos.y + (quitWindowSize.y-quitWindowButton.y)/2, quitWindowButton.x, quitWindowButton.y), "Yes")) {
			// exit game
			Application.Quit();
		}
		if (GUI.Button (new Rect (quitWindowPos.x+quitWindowSize.x - quitWindowButton.x*2, quitWindowPos.y + (quitWindowSize.y-quitWindowButton.y)/2, quitWindowButton.x, quitWindowButton.y), "No")) {
			this.showing = "menu";
		}
	}
}
