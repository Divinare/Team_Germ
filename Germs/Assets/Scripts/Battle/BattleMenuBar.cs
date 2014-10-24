using UnityEngine;
using System.Collections;

public class BattleMenuBar : MonoBehaviour {
	
	public static ItemStats itemStats = ItemStats.itemStats;

	public AudioSource clickSound;

	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public Texture2D meleeIcon;
	public Texture2D rangedIcon;
	public Texture2D skipIcon;

	// Common GUI stuff
	private Vector2 menuBarSize;
	private Vector2 menuBarPosition;
	private float menuBarDescriptionHeight;
	private Vector2 menuPosition;
	private Vector2 menuButtonSize;

	// Battle GUI stuff
	public Vector2 battlelogScrollPosition;
	private string battlelog;
	private Vector2 battlelogSize;
	private Vector2 battlelogPosition;
	private Vector2 itemMenuButtonSize;
	private Vector2 activityMenuButtonSize;


	void Start () {

		// Common GUI stuff
		menuBarSize = MenuBar.menuBar.menuBarSize;
		menuBarPosition = MenuBar.menuBar.menuBarPosition;
		menuBarDescriptionHeight = MenuBar.menuBar.menuBarDescriptionHeight;
		menuButtonSize = MenuBar.menuBar.menuButtonSize;
		menuPosition = MenuBar.menuBar.menuPosition;


		// Battle GUI stuff
		battlelogScrollPosition = new Vector2(0,0);
		battlelog = "";
		battlelogSize = new Vector2 (Screen.width * 0.2f, menuBarSize.y);
		battlelogPosition = new Vector2 (0, menuBarPosition.y);
		itemMenuButtonSize = new Vector2 (menuBarSize.y-menuBarDescriptionHeight, menuBarSize.y-menuBarDescriptionHeight);
		activityMenuButtonSize = new Vector2 (menuBarSize.y-menuBarDescriptionHeight, menuBarSize.y-menuBarDescriptionHeight);
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		createBattlelog ();
		createItemMenu ();
		createActivityMenu ();
		MenuBar.menuBar.createMainMenuButton ();
	}

	private void createBattlelog() {
		// Text area background
		//GUI.contentColor = Color.yellow;
		GUI.Box (new Rect (battlelogPosition.x, battlelogPosition.y, battlelogSize.x, battlelogSize.y), "");
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
		
		//GUI.contentColor = Color.yellow;

		string[,] inventoryContent = itemStats.getInventoryContent ();

		int items = itemStats.getInventorySize();
		float centerInventoryPosition = (Screen.width - (itemMenuButtonSize.x*items))/2;
		int index = 0;

		for(int i = 0; i < items; i++) {
			string itemName = inventoryContent[i,0];
			if (GUI.Button (new Rect (battlelogSize.x + itemMenuButtonSize.x * index, menuBarPosition.y + menuBarDescriptionHeight, itemMenuButtonSize.x, itemMenuButtonSize.y), itemStats.getItemName(itemName))) {
				if(!itemName.Equals("empty")) {
					// using potions function here
				}
			}
			index++;
		}
		
		//Tooltip position
		GUI.Label(new Rect(battlelogSize.x+5, Screen.height - itemMenuButtonSize.y-20, 100, 100), GUI.tooltip);
		
	}

	private void createActivityMenu() {
		float amountOfItems = 4;
		
		// Create menu button
		//MenuCreator.menuCreator.createMenu ();
		
		
		//bg box
		//GUI.Box (new Rect (Screen.width-activityMenuButtonSize.x * amountOfItems, Screen.height - activityMenuButtonSize.y, activityMenuButtonSize.x * (amountOfItems+1), activityMenuButtonSize.y), "");
		
		//buttons
		if (GUI.Button (new Rect (menuBarSize.x-activityMenuButtonSize.x*5, menuBarPosition.y + menuBarDescriptionHeight, activityMenuButtonSize.x, activityMenuButtonSize.y), new GUIContent (skipIcon, "Skip turn"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().Deselect();
			//clickSound.Play ();		
		}
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
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("poison"); // tää vissii se special
			//clickSound.Play ();		
		}

		
		//Tooltip position
		GUI.Label(new Rect(Screen.width - 100, Screen.height - activityMenuButtonSize.y - 50, 100, 100), GUI.tooltip);
	}

}
