using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleMenuBar : MonoBehaviour {
	
	private ItemStats itemStats = ItemStats.itemStats;
	private AudioController audioController;

	public Texture2D meleeIcon;
	public Texture2D selectedMeleeIcon;
	public Texture2D rangedIcon;
	public Texture2D selectedRangedIcon;
	public Texture2D skipIcon;
	public Texture2D rangedStunIcon;
	public Texture2D selectedRangedStunIcon;
	public Texture2D poisonIcon;
	public Texture2D selectedPoisonIcon;
	public Texture2D healIcon;
	public Texture2D selectedHealIcon;
	public Texture2D detoxIcon;
	public Texture2D selectedDetoxIcon;

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

	private int selectedItemIndex;

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
	

	void OnGUI() {
		createBattlelog ();
		createItemMenu ();
		createActivityMenu ();
		MenuBar.menuBar.createMainMenuButton ();
	}

	private void createBattlelog() {
		// Text area background
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

		string[,] inventoryContent = itemStats.getInventoryContent ();

		int items = itemStats.getInventorySize();
		float centerInventoryPosition = (Screen.width - (itemMenuButtonSize.x*items))/2;
		int index = 0;

		GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
		for(int i = 0; i < items; i++) {
			string itemName = inventoryContent[i,0];
			string potionIcon = itemName;
			if (itemName.Equals (currentUnit.GetComponent<UnitStatus>().selectedAction)) {
				potionIcon = potionIcon + "Selected";
				Debug.Log ("Potion icon name is now " + potionIcon);
			}
			if (GUI.Button (new Rect (battlelogSize.x + itemMenuButtonSize.x * index, menuBarPosition.y + menuBarDescriptionHeight, itemMenuButtonSize.x, itemMenuButtonSize.y), itemStats.getItemIcon(potionIcon))) {
				if(!itemName.Equals("empty")) {
					string itemType = itemStats.getItemType(itemName);
					if(itemType.Equals("Potion")) {
						currentUnit.GetComponent<UnitStatus>().switchSelectedAction (itemName);
						selectedItemIndex = i;
					}

				}
			}
			index++;
		}
		
		//Tooltip position
		GUI.Label(new Rect(battlelogSize.x+5, Screen.height - itemMenuButtonSize.y-20, 100, 100), GUI.tooltip);
		
	}
	
	private void createActivityMenu() {

		//buttons
		GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
		createActivityButton (currentUnit, 5, "skipTurn", "Skip Turn", skipIcon);
		if (currentUnit.GetComponent<UnitStatus>().selectedAction.Equals ("melee")) {
			createActivityButton (currentUnit, 4, "melee", "Melee attack", selectedMeleeIcon);
		}
		else {
			createActivityButton (currentUnit, 4, "melee", "Melee attack", meleeIcon);
		}
		if (currentUnit.GetComponent<UnitStatus>().selectedAction.Equals ("ranged")) {
			createActivityButton (currentUnit, 3, "ranged", "Ranged attack", selectedRangedIcon);
		}
		else {
			createActivityButton (currentUnit, 3, "ranged", "Ranged attack", rangedIcon);
		}
		
		bool hasSpecialAbility = false;
		if (currentUnit.GetComponent<UnitStatus>().hasRangedStun) {
			Texture2D texture = rangedStunIcon;
			if (currentUnit.GetComponent<UnitStatus>().selectedAction.Equals ("rangedStun")) {
				texture = selectedRangedStunIcon;
			}
			createActivityButton (currentUnit, 2, "rangedStun", "Stun", texture);
			hasSpecialAbility = true;
		}
		if (currentUnit.GetComponent<UnitStatus>().hasDetox) {
			Texture2D texture = detoxIcon;
			if (currentUnit.GetComponent<UnitStatus>().selectedAction.Equals ("detox")) {
				texture = selectedDetoxIcon;
			}
			createActivityButton (currentUnit, 2, "detox", "Detoxify", texture);
			hasSpecialAbility = true;
		}
		if (currentUnit.GetComponent<UnitStatus>().hasHeal) {
			Texture2D texture = healIcon;
			if (currentUnit.GetComponent<UnitStatus>().selectedAction.Equals ("heal")) {
				texture = selectedHealIcon;
			}
			createActivityButton (currentUnit, 2, "heal", "Heal", texture);
			hasSpecialAbility = true;
		}
		if (currentUnit.GetComponent<UnitStatus>().hasPoison) {
			Texture2D texture = poisonIcon;
			if (currentUnit.GetComponent<UnitStatus>().selectedAction.Equals ("poison")) {
				texture = selectedPoisonIcon;
			}
			createActivityButton (currentUnit, 2, "poison", "Poison", texture);
			hasSpecialAbility = true;
		}
		if (!hasSpecialAbility) {
			createActivityButton (currentUnit, 2, "poison", "No action", null);
		}

		//Tooltip position
		GUI.Label(new Rect(Screen.width - 100, Screen.height - activityMenuButtonSize.y - 50, 100, 100), GUI.tooltip);
	}

	private void createActivityButton(GameObject currentUnit, int index, string action, string actionDescription, Texture2D texture) {

		Dictionary<string, bool> unitHasActions = currentUnit.GetComponent<UnitStatus> ().GetUnitActions ();
		Color original = GUI.color;

		if (!unitHasActions[action]) {
			GUI.enabled = false;
			actionDescription = "Action not available";
			Color transparentButtonColor = original;
			transparentButtonColor.a = 0.5f;
			GUI.color = transparentButtonColor;
		}
		else if (!action.Equals ("melee") && !action.Equals ("ranged") && !action.Equals ("skipTurn")) {
			if (currentUnit.GetComponent<UnitStatus>().actionCooldown > 0) {
				GUI.enabled = false;
				actionDescription = "Action on cooldown";
				Color transparentButtonColor = original;
				transparentButtonColor.a = 0.5f;
				GUI.color = transparentButtonColor;
			}
		}
		
		if (GUI.Button (new Rect (menuBarSize.x-activityMenuButtonSize.x*index, menuBarPosition.y + menuBarDescriptionHeight, activityMenuButtonSize.x, activityMenuButtonSize.y), new GUIContent (texture, actionDescription))) {
			if(action.Equals("skipTurn")) {
				if (!currentUnit.GetComponent<UnitStatus>().IsEnemy()) {
					currentUnit.GetComponent<UnitStatus>().Deselect();
				}
			} else {
				currentUnit.GetComponent<UnitStatus>().switchSelectedAction (action);
			}
			//audioController.playClickSound();		
		}
		GUI.enabled = true;
		GUI.color = original;
	}

	public int getSelectedItemIndex() {
		return selectedItemIndex;
	}

}
