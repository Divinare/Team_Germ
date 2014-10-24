using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopMenuBar : MonoBehaviour {

	private ItemStats itemStats;

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
	private Vector2 inventoryButtonSize;
	private string[,] inventoryContent;
	
	void Start () {
		gameStatus = GameObject.Find ("GameStatus").GetComponent<GameStatus> ();
		itemStats = ItemStats.itemStats;
		getGoldAndXp ();

		// Common GUI stuff
		menuBarSize = MenuBar.menuBar.menuBarSize;
		menuBarPosition = MenuBar.menuBar.menuBarPosition;
		menuBarDescriptionHeight = MenuBar.menuBar.menuBarDescriptionHeight;
		xpGoldButtonSize = MenuBar.menuBar.xpGoldButtonSize;
		menuButtonSize = MenuBar.menuBar.menuButtonSize;
		menuPosition = MenuBar.menuBar.menuPosition;
		shopMapTrainerButtonSize = MenuBar.menuBar.shopMapTrainerButtonSize;
		
		inventoryButtonSize = new Vector2 (menuBarSize.y, menuBarSize.y);
		inventoryContent = itemStats.getInventoryContent ();
	}


	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		inventoryContent = ItemStats.itemStats.getInventoryContent ();
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
		Shop_GUI shopGUI = GameObject.FindGameObjectWithTag ("ShopGUI").GetComponent<Shop_GUI> ();
		int items = itemStats.getInventorySize();
		float centerInventoryPosition = (Screen.width - (inventoryButtonSize.x*items))/2;
		int index = 0;


		for(int i = 0; i < items; i++) {
			string itemName = inventoryContent[i,0];
			if (GUI.Button (new Rect (centerInventoryPosition + inventoryButtonSize.x * index, menuBarPosition.y, inventoryButtonSize.x, inventoryButtonSize.y), itemStats.itemIcons[itemName])) {
				if(!itemName.Equals("empty")) {

					shopGUI.setItemOwned(true);
					shopGUI.setSelectedItem(itemName);
					shopGUI.setSelectedInventoryIndex(i+1);
				}

			}
			index++;
		}




		/*
		if (GUI.Button (new Rect (centerInventoryPosition, menuBarPosition.y, inventoryButtonSize.x, inventoryButtonSize.y), selectedItems[0])) {
			
		} else if (GUI.Button (new Rect (centerInventoryPosition + inventoryButtonSize.x * 1, menuBarPosition.y, inventoryButtonSize.x, inventoryButtonSize.y), selectedItems[1])) {
			
		} else if (GUI.Button (new Rect (centerInventoryPosition + inventoryButtonSize.x * 2, menuBarPosition.y, inventoryButtonSize.x, inventoryButtonSize.y), selectedItems[2])) {
			
		}
		*/
	}
	
	
	public void getGoldAndXp() {
		gold = gameStatus.getGold();
		xp = gameStatus.getXp();
	}
}
