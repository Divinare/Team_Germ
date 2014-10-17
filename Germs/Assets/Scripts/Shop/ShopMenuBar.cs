using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopMenuBar : MonoBehaviour {
	
	public float gold;
	public float xp;
	
	public GameStatus gameStatus;
	public AudioSource clickSound;
	
	public Texture2D goldIcon;
	public Texture2D xpIcon;
	
	// Common stuff
	public Vector2 menuBarSize;
	public Vector2 menuBarPosition;
	public float menuBarDescriptionHeight;
	public Vector2 xpGoldButtonSize;
	public Vector2 shopMapTrainerButtonSize;
	public Vector2 menuButtonSize;
	public Vector2 menuPosition;

	// Shop GUI stuff

	public List<string> selectedItems = new List<string>();
	private Vector2 inventoryButtonSize;
	
	
	void Start () {
		gameStatus = GameObject.Find ("GameStatus").GetComponent<GameStatus> ();
		getGoldAndXp ();
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		// Common GUI stuff
		menuBarSize = MenuBar.menuBar.menuBarSize;
		menuBarPosition = MenuBar.menuBar.menuBarPosition;
		menuBarDescriptionHeight = MenuBar.menuBar.menuBarDescriptionHeight;
		xpGoldButtonSize = MenuBar.menuBar.xpGoldButtonSize;
		menuButtonSize = MenuBar.menuBar.menuButtonSize;
		menuPosition = MenuBar.menuBar.menuPosition;
		shopMapTrainerButtonSize = MenuBar.menuBar.shopMapTrainerButtonSize;
		
		this.selectedItems.Add ("Potion1");
		this.selectedItems.Add ("Potion2");
		this.selectedItems.Add ("Potion3");
		
		inventoryButtonSize = new Vector2 (menuBarSize.y, menuBarSize.y);

		createShopMenu ();
	}

	public void createShopMenu() {
		MenuBar.menuBar.createReturnToMapButton (1);
		MenuBar.menuBar.createTrainerButton(2);
		createInventory();
		MenuBar.menuBar.createXpAndGoldButtons ();
		MenuBar.menuBar.createMainMenuButton ();
		
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
	
	
	public void getGoldAndXp() {
		gold = gameStatus.getGold();
		xp = gameStatus.getXp();
	}
}
