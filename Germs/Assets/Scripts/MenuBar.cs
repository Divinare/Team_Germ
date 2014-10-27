using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuBar : MonoBehaviour {

	public static MenuBar menuBar;

	private GameStatus gameStatus;
	private AudioController audioController;
	private BattleStatus battleStatus;

	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public Texture2D meleeIcon;
	public Texture2D rangedIcon;

	public GUIStyle bigNumbers;
	public GUIStyle trainerHover;
	public GUIStyle shopHover;
	public GUIStyle mapHover;

	// Common stuff
	public Vector2 menuBarSize;
	public Vector2 menuBarPosition;
	public float menuBarDescriptionHeight;
	public Vector2 xpGoldButtonSize;
	public Vector2 shopMapTrainerButtonSize;
	public Vector2 menuButtonSize;
	public Vector2 menuPosition;
	

	void Start () {
		if (menuBar == null) {
			DontDestroyOnLoad (gameObject);
			menuBar = this;
		} else if (menuBar != this) {
			Destroy (gameObject);
		}
		gameStatus = GameStatus.gameStatus;
		audioController = AudioController.audioController;
		battleStatus = BattleStatus.battleStatus;

		// Common stuff
		menuBarSize = new Vector2 (Screen.width, 70);
		menuBarPosition = new Vector2 (0, Screen.height - menuBarSize.y);
		menuBarDescriptionHeight = 0; // menuBarSize.y/3;
		xpGoldButtonSize = new Vector2 (menuBarSize.y, menuBarSize.y);
		menuButtonSize = new Vector2(menuBarSize.y-menuBarDescriptionHeight, menuBarSize.y-menuBarDescriptionHeight);
		menuPosition = new Vector2 (menuBarSize.x - menuButtonSize.x, menuBarPosition.y + menuBarDescriptionHeight);
		shopMapTrainerButtonSize = new Vector2 (menuBarSize.y*2, menuBarSize.y);

	}

	
	// Common methods
	public void createTrainerButton(int index) { // index 1 or 2, for which spot the button belongs
		if (GUI.Button (new Rect (shopMapTrainerButtonSize.x * (index-1), menuBarPosition.y, shopMapTrainerButtonSize.x ,shopMapTrainerButtonSize.y), "", trainerHover)) {
			audioController.playClickSound();
			Application.LoadLevel ("Trainer");
		}
	}

	public void createShopButton(int index) {
		if (GUI.Button (new Rect (shopMapTrainerButtonSize.x * (index-1), menuBarPosition.y, shopMapTrainerButtonSize.x ,shopMapTrainerButtonSize.y), "", shopHover)) {
			audioController.playClickSound();
			Application.LoadLevel ("Shop");
		}
	}

	public void createReturnToMapButton(int index) {
		if (GUI.Button (new Rect (shopMapTrainerButtonSize.x * (index-1), menuBarPosition.y, shopMapTrainerButtonSize.x ,shopMapTrainerButtonSize.y), "", mapHover)) {
			audioController.playClickSound();
			Application.LoadLevel ("Map");
		}
	}

	public void createXpAndGoldButtons() {
		float gold = gameStatus.getGold();
		float xp = gameStatus.getXp ();

		GUI.Box (new Rect (menuBarSize.x - menuButtonSize.x - xpGoldButtonSize.x, menuBarPosition.y, xpGoldButtonSize.x, xpGoldButtonSize.y), goldIcon);
		GUI.Label(new Rect(menuBarSize.x - menuButtonSize.x - xpGoldButtonSize.x, menuBarPosition.y, xpGoldButtonSize.x, xpGoldButtonSize.y), gold.ToString(), bigNumbers);
		
		GUI.Box (new Rect (menuBarSize.x - menuButtonSize.x - xpGoldButtonSize.x*2, menuBarPosition.y, xpGoldButtonSize.x, xpGoldButtonSize.y), xpIcon);
		GUI.Label(new Rect(menuBarSize.x - menuButtonSize.x - xpGoldButtonSize.x*2, menuBarPosition.y, xpGoldButtonSize.x, xpGoldButtonSize.y), xp.ToString(), bigNumbers);
	}

	public void createMainMenuButton() {
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y, menuButtonSize.x, menuButtonSize.y), "Main\nMenu")) {
			audioController.playClickSound();
			audioController.stopMapMusic();
			audioController.stopBattleMusic();
			audioController.playMenuMusic();
			Application.LoadLevel ("MainMenu");
		}
	}

	/*
	private void changeMenubarHeight(int height) {
		menuBarSize.y = height;
		menuBarPosition.y = Screen.height - menuBarSize.y;
	}
	*/
}
