using UnityEngine;
using System.Collections;

public class BattleMenuBar : MonoBehaviour {
	
	private ItemStats itemStats = ItemStats.itemStats;
	private AudioController audioController;

	public Texture2D meleeIcon;
	public Texture2D rangedIcon;
	public Texture2D skipIcon;
	public Texture2D rangedStunIcon;
	public Texture2D poisonIcon;

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

		GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
		for(int i = 0; i < items; i++) {
			string itemName = inventoryContent[i,0];
			if (GUI.Button (new Rect (battlelogSize.x + itemMenuButtonSize.x * index, menuBarPosition.y + menuBarDescriptionHeight, itemMenuButtonSize.x, itemMenuButtonSize.y), itemStats.getItemIcon(itemName))) {
				if(!itemName.Equals("empty")) {
					string itemType = itemStats.getItemType(itemName);
					if(itemType.Equals("Potion")) {
						currentUnit.GetComponent<UnitStatus>().switchSelectedAction (itemName);
					}

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
		GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
		createActivityButton (currentUnit, 5, "skipTurn", "Skip Turn", skipIcon, true);
		createActivityButton (currentUnit, 4, "melee", "Fancy melee attack", meleeIcon, false);
		createActivityButton (currentUnit, 3, "ranged", "Fancy ranged attack", rangedIcon, false);
		createActivityButton (currentUnit, 2, "rangedStun", "Fancy special attack", rangedStunIcon, false);

		//Tooltip position
		GUI.Label(new Rect(Screen.width - 100, Screen.height - activityMenuButtonSize.y - 50, 100, 100), GUI.tooltip);
	}

	private void createActivityButton(GameObject currentUnit, int index, string action, string actionDescription, Texture2D texture, bool skipTurn) {

		if (GUI.Button (new Rect (menuBarSize.x-activityMenuButtonSize.x*index, menuBarPosition.y + menuBarDescriptionHeight, activityMenuButtonSize.x, activityMenuButtonSize.y), new GUIContent (texture, action))) {
			if(skipTurn) {
				if (!currentUnit.GetComponent<UnitStatus>().IsEnemy()) {
					currentUnit.GetComponent<UnitStatus>().Deselect();
				}
			} else {
				currentUnit.GetComponent<UnitStatus>().switchSelectedAction (action);
			}
			//audioController.playClickSound();		
		}
	}

}
