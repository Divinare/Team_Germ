using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuBar : MonoBehaviour {

	public static MenuBar menuBar;

	public float gold;
	public float xp;
	public bool bacChooser;

	public AudioSource clickSound;
	public BattleStatus battleStatus;
	public GameStatus gameStatus;
	public string showing = "map"; // battle, map, shop or trainer

	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public Texture2D meleeIcon;
	public Texture2D rangedIcon;

	public GUIStyle bigNumbers;
	public GUIStyle trainerHover;
	public GUIStyle shopHover;
	public GUIStyle mapHover;


	// Common stuff
	private Vector2 menuBarSize;
	private Vector2 menuBarPosition;
	private float menuBarDescriptionHeight;
	private Vector2 xpGoldButtonSize;
	private Vector2 shopMapTrainerButtonSize;
	// Battle
	public Vector2 battlelogScrollPosition;
	private string battlelog;
	private Vector2 battlelogSize;
	private Vector2 battlelogPosition;
	private Vector2 itemMenuButtonSize;
	private Vector2 activityMenuButtonSize;
	private Vector2 menuPosition;
	private Vector2 menuButtonSize;
	
	// Map
	public int clickedIndex;
	public Dictionary<string, int[]> allBacteriaStats = new Dictionary<string, int[]>();
	public List<string> selectedUnits = new List<string>();

	// Shop
	public List<string> selectedItems = new List<string>();
	private Vector2 inventoryButtonSize;

	// Trainer



	void Start () {
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();

		if (menuBar == null) {
			DontDestroyOnLoad (gameObject);
			menuBar = this;
			getGoldAndXp();
		} else if (menuBar != this) {
			menuBar.getGoldAndXp();
			Destroy (gameObject);
		}

		// Common stuff
		menuBarSize = new Vector2 (Screen.width, 70);
		menuBarPosition = new Vector2 (0, Screen.height - menuBarSize.y);
		menuBarDescriptionHeight = 0; // menuBarSize.y/3;
		xpGoldButtonSize = new Vector2 (menuBarSize.y, menuBarSize.y);
		menuButtonSize = new Vector2(menuBarSize.y-menuBarDescriptionHeight, menuBarSize.y-menuBarDescriptionHeight);
		menuPosition = new Vector2 (menuBarSize.x - menuButtonSize.x, menuBarPosition.y + menuBarDescriptionHeight);
		shopMapTrainerButtonSize = new Vector2 (menuBarSize.y*2, menuBarSize.y);

		// Battle
		battlelogScrollPosition = new Vector2(0,0);
		battlelog = "";
		battlelogSize = new Vector2 (Screen.width * 0.2f, menuBarSize.y);
		battlelogPosition = new Vector2 (0, menuBarPosition.y);
		itemMenuButtonSize = new Vector2 (menuBarSize.y-menuBarDescriptionHeight, menuBarSize.y-menuBarDescriptionHeight);
		activityMenuButtonSize = new Vector2 (menuBarSize.y-menuBarDescriptionHeight, menuBarSize.y-menuBarDescriptionHeight);


		// Map

		allBacteriaStats = battleStatus.getAllBacteriaStats();
		selectedUnits = battleStatus.getSelectedUnits();
		bacChooser = false;
		
		// Shop

		this.selectedItems.Add ("Potion1");
		this.selectedItems.Add ("Potion2");
		this.selectedItems.Add ("Potion3");

		inventoryButtonSize = new Vector2 (menuBarSize.y, menuBarSize.y);
		
		// Trainer


	}

	void OnGUI() {
		if (showing.Equals ("battle")) {
			createBattleMenu ();
		} else if (showing.Equals ("map")) {
			createMapMenu();
		} else if (showing.Equals ("shop")) {
			createShopMenu();
		} else if (showing.Equals ("trainer")) {
			createTrainerMenu();
		}

		if (bacChooser) {
			allBacteriaStats = battleStatus.getAllBacteriaStats();
			var pos = 0;
			foreach (string bac in allBacteriaStats.Keys) {
				if (!selectedUnits.Contains(bac)) {
					if (GUI.Button (new Rect (Screen.width/8 +pos,Screen.height - Screen.height/4,Screen.width/12,Screen.height/10), bac)) {
						battleStatus.setSelectedUnit(bac, clickedIndex);
						bacChooser = false;
					}
					pos += Screen.width/12;
				}
			}
		}
	}

	private void createBattleMenu() {
			createBattlelog ();
			createItemMenu ();
			createActivityMenu ();
			createMainMenuButton ();
	}


	private void createBattlelog() {
		// Text area background
		GUI.contentColor = Color.yellow;

		// Battlelog
		GUILayout.BeginArea (new Rect (battlelogPosition.x, battlelogPosition.y, battlelogSize.x, battlelogSize.y));
		battlelogScrollPosition = GUILayout.BeginScrollView(battlelogScrollPosition, GUILayout.Width(battlelogSize.x), GUILayout.Height(battlelogSize.y));
		GUILayout.Label(battlelog);
		
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}
	         
	public void addToBattleLog(string txt) {
		if (battlelog.Length == 0) {
			battlelog = txt;
		} else {
			battlelog += "\n" + txt;
		}
		battlelogScrollPosition.y = Mathf.Infinity;
	}

	private void createItemMenu() {
		float amountOfItems = 3;
		float scaleToMiddle = Screen.width * 0.4f;
		
		GUI.contentColor = Color.yellow;

		//buttons
		if (GUI.Button (new Rect (battlelogSize.x, menuBarPosition.y + menuBarDescriptionHeight, itemMenuButtonSize.x, itemMenuButtonSize.y), new GUIContent ("1", "Fancy item 1"))) {
			//clickSound.Play ();		
		}
		if (GUI.Button (new Rect (battlelogSize.x+itemMenuButtonSize.x, menuBarPosition.y + menuBarDescriptionHeight, itemMenuButtonSize.x, itemMenuButtonSize.y), new GUIContent ("2", "Fancy item 2"))) {
			//clickSound.Play ();		
		}
		if (GUI.Button (new Rect (battlelogSize.x+itemMenuButtonSize.x*2, menuBarPosition.y + menuBarDescriptionHeight, itemMenuButtonSize.x, itemMenuButtonSize.y), new GUIContent ("3", "Fancy item 3"))) {
			//clickSound.Play ();		
		}

		//Tooltip position
		GUI.Label(new Rect(battlelogSize.x+5, Screen.height - itemMenuButtonSize.y-20, 100, 100), GUI.tooltip);

	}

	private void createActivityMenu() {
		float amountOfItems = 3;
		
		// Create menu button
		//MenuCreator.menuCreator.createMenu ();
		
		
		//bg box
		//GUI.Box (new Rect (Screen.width-activityMenuButtonSize.x * amountOfItems, Screen.height - activityMenuButtonSize.y, activityMenuButtonSize.x * (amountOfItems+1), activityMenuButtonSize.y), "");
		
		//buttons
		if (GUI.Button (new Rect (menuBarSize.x-activityMenuButtonSize.x*4, menuBarPosition.y + menuBarDescriptionHeight, activityMenuButtonSize.x, activityMenuButtonSize.y), new GUIContent (meleeIcon, "Fancy melee attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("melee");
			//clickSound.Play ();		
		}
		if (GUI.Button (new Rect (menuBarSize.x-activityMenuButtonSize.x*3, menuBarPosition.y + menuBarDescriptionHeight, activityMenuButtonSize.x, activityMenuButtonSize.y), new GUIContent (rangedIcon, "Fancy ranged attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("ranged");
			//clickSound.Play ();		
		}
		if (GUI.Button (new Rect (menuBarSize.x-activityMenuButtonSize.x*2, menuBarPosition.y + menuBarDescriptionHeight, activityMenuButtonSize.x, activityMenuButtonSize.y), new GUIContent ("special here", "Fancy special attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("heal"); // tää vissii se special
			//clickSound.Play ();		
		}

		//Tooltip position
		GUI.Label(new Rect(Screen.width - 100, Screen.height - activityMenuButtonSize.y - 50, 100, 100), GUI.tooltip);
}

	// Map
	public void createMapMenu() {
		createTrainerButton(1);
		createShopButton (2);

		createBacteriaBar ();
		createXpAndGoldButtons ();
		createMainMenuButton ();
	}

	private void createBacteriaBar() {
		//frame for chosen bacteria
		if (GUI.Button (new Rect (Screen.width/2 - Screen.width/4,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[0])) {
			clickedIndex = 0;
			drawChooserBar();
		}
		if (GUI.Button (new Rect (Screen.width/2 - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[1])) {
			clickedIndex = 1;
			drawChooserBar();
		}
		if (GUI.Button (new Rect (Screen.width/2 - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[2])) {
			clickedIndex = 2;
			drawChooserBar();
		}
		if (GUI.Button (new Rect (Screen.width/2,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[3])) {
			clickedIndex = 3;
			drawChooserBar();
		}
		if (GUI.Button (new Rect (Screen.width/2 + Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[4])) {
			clickedIndex = 4;
			drawChooserBar();
		}
	}

	private void drawChooserBar() {
		bacChooser = true;
	}

	// Shop
	public void createShopMenu() {
		createReturnToMapButton (1);
		createTrainerButton(2);
		createInventory();
		createXpAndGoldButtons ();
		createMainMenuButton ();

	}
	
	
	private void createInventory() {
		int items = 3;
		float centerInventoryPosition = (Screen.width - (inventoryButtonSize.x*items))/2;
		Debug.Log ("cent " + centerInventoryPosition);
		if (GUI.Button (new Rect (centerInventoryPosition, menuBarPosition.y, inventoryButtonSize.x, inventoryButtonSize.y), selectedItems[0])) {
			
		} else if (GUI.Button (new Rect (centerInventoryPosition + inventoryButtonSize.x * 1, menuBarPosition.y, inventoryButtonSize.x, inventoryButtonSize.y), selectedItems[1])) {
			
		} else if (GUI.Button (new Rect (centerInventoryPosition + inventoryButtonSize.x * 2, menuBarPosition.y, inventoryButtonSize.x, inventoryButtonSize.y), selectedItems[2])) {
			
		}
	}
	
	

	// Trainer
	public void createTrainerMenu() {
		createReturnToMapButton (1);
		createShopButton(2);
		createXpAndGoldButtons ();
		createMainMenuButton ();
	}

	// Common methods
	private void createTrainerButton(int index) { // index 1 or 2, for which spot the button belongs
		if (GUI.Button (new Rect (shopMapTrainerButtonSize.x * (index-1), menuBarPosition.y, shopMapTrainerButtonSize.x ,shopMapTrainerButtonSize.y), "", trainerHover)) {
			//clickSound.Play ();	
			showing = "trainer";
			Application.LoadLevel ("Trainer");
		}
	}

	private void createShopButton(int index) {
		if (GUI.Button (new Rect (shopMapTrainerButtonSize.x * (index-1), menuBarPosition.y, shopMapTrainerButtonSize.x ,shopMapTrainerButtonSize.y), "", shopHover)) {
		//	clickSound.Play ();	
			showing = "shop";
			Application.LoadLevel ("Shop");
		}
	}

	private void createReturnToMapButton(int index) {
		if (GUI.Button (new Rect (shopMapTrainerButtonSize.x * (index-1), menuBarPosition.y, shopMapTrainerButtonSize.x ,shopMapTrainerButtonSize.y), "", mapHover)) {
			//	clickSound.Play ();	
			showing = "map";
			Application.LoadLevel ("Map");
		}
	}

	public void createXpAndGoldButtons() {
		GUI.Box (new Rect (menuBarSize.x - menuButtonSize.x - xpGoldButtonSize.x, menuBarPosition.y, xpGoldButtonSize.x, xpGoldButtonSize.y), goldIcon);
		GUI.Label(new Rect(menuBarSize.x - menuButtonSize.x - xpGoldButtonSize.x, menuBarPosition.y, xpGoldButtonSize.x, xpGoldButtonSize.y), gold.ToString(), bigNumbers);
		
		GUI.Box (new Rect (menuBarSize.x - menuButtonSize.x - xpGoldButtonSize.x*2, menuBarPosition.y, xpGoldButtonSize.x, xpGoldButtonSize.y), xpIcon);
		GUI.Label(new Rect(menuBarSize.x - menuButtonSize.x - xpGoldButtonSize.x*2, menuBarPosition.y, xpGoldButtonSize.x, xpGoldButtonSize.y), xp.ToString(), bigNumbers);
	}

	private void createMainMenuButton() {
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y, menuButtonSize.x, menuButtonSize.y), "Main\nMenu")) {
			//clickSound.Play ();
			showing = "";
			Application.LoadLevel ("MainMenu");
		}
	}
	
	private void switchShowingTo(string newShowing) {
		this.showing = newShowing;
	}

	public void getGoldAndXp() {
		gold = gameStatus.getGold();
		xp = gameStatus.getXp();
	}
	/*
	private void changeMenubarHeight(int height) {
		menuBarSize.y = height;
		menuBarPosition.y = Screen.height - menuBarSize.y;
	}
	*/
}
