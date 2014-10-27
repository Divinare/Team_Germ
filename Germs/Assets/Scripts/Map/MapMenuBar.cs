using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapMenuBar : MonoBehaviour {
		
	public float gold;
	public float xp;
	
	private GameStatus gameStatus;
	private BattleStatus battleStatus;
	private UnitStats unitStats;

	public AudioSource clickSound;
	public Texture2D goldIcon;
	public Texture2D xpIcon;
	private bool initialized = false;

	// Common stuff
	public Vector2 menuBarSize;
	public Vector2 menuBarPosition;
	public float menuBarDescriptionHeight;
	public Vector2 xpGoldButtonSize;
	public Vector2 shopMapTrainerButtonSize;
	public Vector2 menuButtonSize;
	public Vector2 menuPosition;

	private Vector2 middleBar;
	public int clickedIndex;
	public Dictionary<string, int[]> playerUnitStats = new Dictionary<string, int[]>();
	public List<string> selectedUnits = new List<string>();
	private Dictionary<string, Texture2D> allUnitImages = new Dictionary<string, Texture2D>();
	public bool bacChooser;
	
	void Start () {

		battleStatus = GameObject.Find ("BattleStatus").GetComponent<BattleStatus> ();
		unitStats = GameObject.Find ("UnitStats").GetComponent<UnitStats> ();

		allUnitImages = unitStats.getImageDict();
		playerUnitStats = unitStats.getPlayerUnitStats();
	}

	private void getMenuBarValues() {
		gameStatus = GameStatus.gameStatus;

		// Common GUI stuff
		menuBarSize = MenuBar.menuBar.menuBarSize;
		menuBarPosition = MenuBar.menuBar.menuBarPosition;
		menuBarDescriptionHeight = MenuBar.menuBar.menuBarDescriptionHeight;
		xpGoldButtonSize = MenuBar.menuBar.xpGoldButtonSize;
		menuButtonSize = MenuBar.menuBar.menuButtonSize;
		menuPosition = MenuBar.menuBar.menuPosition;
		shopMapTrainerButtonSize = MenuBar.menuBar.shopMapTrainerButtonSize;
		
		// Map GUI stuff
		middleBar = new Vector2 (shopMapTrainerButtonSize.x+Screen.width/4, Screen.height-menuButtonSize.y);
		selectedUnits = battleStatus.getSelectedUnits();
		bacChooser = false;
		
		getGoldAndXp ();
	}
	
	void OnGUI() {

	if (!initialized) {
		getMenuBarValues ();
		initialized = true;
	}

	createMapMenu ();
	}
	

	private void createMapMenu() {
		MenuBar.menuBar.createTrainerButton(1);
		MenuBar.menuBar.createShopButton (2);
		
		createBacteriaBar ();
		MenuBar.menuBar.createXpAndGoldButtons ();
		MenuBar.menuBar.createMainMenuButton ();
	}
	
	private void createBacteriaBar() {
		//frame for chosen bacteria
		float width = 0;
		for (int i = 0; i < selectedUnits.Count; i++) {
			if (GUI.Button (new Rect (middleBar.x+width,middleBar.y,menuButtonSize.x,menuButtonSize.y), allUnitImages[selectedUnits[i]])) {
				clickedIndex = i;
				drawChooserBar();
			}
			width += menuButtonSize.x;
		}

		if (bacChooser) {
			float pos = 0;
			foreach (string bac in allUnitImages.Keys) {
				if (playerUnitStats.ContainsKey(bac) && !selectedUnits.Contains(bac) && bac != "empty") {
					if (GUI.Button (new Rect (middleBar.x +pos,middleBar.y-menuButtonSize.y,menuButtonSize.x,menuButtonSize.y), allUnitImages[bac])) {
						battleStatus.setSelectedUnit(bac, clickedIndex);
						bacChooser = false;
					}
					pos += menuButtonSize.x;
				}
				if (bac == "empty") {
					if (GUI.Button (new Rect (middleBar.x +pos,middleBar.y-menuButtonSize.y,menuButtonSize.x,menuButtonSize.y), allUnitImages[bac])) {
						battleStatus.setSelectedUnit(bac, clickedIndex);
						bacChooser = false;
					}
				}
			}
		}
	}
	
	private void drawChooserBar() {
		bacChooser = true;
	}

		
	private void getGoldAndXp() {
		gold = gameStatus.getGold();
		xp = gameStatus.getXp();
	}
}
