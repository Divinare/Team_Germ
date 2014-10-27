using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public class Shop_GUI : MonoBehaviour {

	private ItemStats itemStats;
	private AudioController audioController;
	private GameStatus gameStatus;

	public float gold = 0;
	public float xp = 0;
	
	private int amountOfIndexes = 3;
	private int amountOfItemColumns = 4;

	public Texture2D goldIcon;
	public Texture2D xpIcon;
	
	public GUIStyle trainerHover;
	public GUIStyle mapHover;
	public GUIStyle bigNumbers;
	public GUIStyle buttonBg;
	public GUIStyle buttonDeactivatedBg;
	public GUIStyle topicText;
	public GUIStyle itemDescriptionText;
	public GUIStyle itemStatusText;

	public Texture shopBox;
	public Texture shopTopic;
	public Texture selectedItemWindow;

	private float hScrollbarValue;
	public Vector2 shopScrollPos = Vector2.zero;
	public Vector2 stashScrollPos = Vector2.zero;

	//Size and position of GUI elements
	private Vector2 windowSize;
	private Vector2 itemSize;
	private Vector2 shopPos;
	private Vector2 rightWindowPos;
	private Vector2 itemInfoSize;
	private Vector2 itemInfoPos;
	// GUI elements inside itemInfoWindow
	private Vector2 itemInfoButtonSize;

	private string selectedItem = "healingPotion";
	bool itemOwned = false;
	private int selectedInventoryIndex = 0; // 1 - inventorySize

	private Dictionary<string, int[]> currentItemStats;

	//string[] stashItems =  new string[] {"axe", "potion1", "potion2", "boots", "potion3"};
	//string[] armors = new string[] {"chainmail", "jacket", "leather coat"};
	//string[] weapons = new string[] {"dagger"};

	private int clickedIndex = 1;
	

	// Use this for initialization
	void Start () {
		itemStats = ItemStats.itemStats;
		audioController = AudioController.audioController;
		gameStatus = GameStatus.gameStatus;

		gold = gameStatus.getGold ();
		xp = gameStatus.getXp ();

		windowSize = new Vector2 (Screen.width * 0.45f, Screen.height * 0.50f);
		itemSize = new Vector2 (Screen.width*0.1f, Screen.width*0.1f);
		itemInfoSize = new Vector2(windowSize.x, windowSize.y);
		shopPos = new Vector2 (Screen.width*0.04f, Screen.height * 0.15f);
		itemInfoPos = new Vector2 (windowSize.x + shopPos.x + 25, shopPos.y);
		itemInfoButtonSize = new Vector2 (windowSize.x/3, itemInfoSize.y*0.3f);

		currentItemStats = ItemStats.itemStats.getCurrenItemStats ();
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {


		drawTexture (0, 0, (int)windowSize.x*0.5f, (int)windowSize.y*0.3f, shopTopic);

		// Creating shop
		GUI.BeginGroup (new Rect (shopPos.x, shopPos.y, windowSize.x, windowSize.y));
		createShopContent();
		GUI.EndGroup ();

		// Creating info window
		GUI.BeginGroup (new Rect (itemInfoPos.x, itemInfoPos.y, windowSize.x, windowSize.y));
		createItemInfo();
		GUI.EndGroup ();

	}



	private void createShopContent() {
		drawTexture (0, 0, (int)windowSize.x, (int)windowSize.y, shopBox);

		if (clickedIndex == 1) {
			createContent(currentItemStats);
		} 
		/*
		else if (clickedIndex == 2) {
			createContent(weapons);
		} else if (clickedIndex == 3) {
			createContent(armors);
		}
		*/
	}

	private void createContent(Dictionary<string, int[]> items) {
		int itemIndex = 0;
		int column = 0;
		int row = 0;
		foreach (string key in items.Keys) {
				createItem ((int)itemSize.x * column, (int)itemSize.y * row, key, itemStats.getItemIcon(key));
			if((itemIndex+1) % amountOfItemColumns == 0) {
				row++;
				column = 0;
			} else {
				column++;
			}
			itemIndex++;
		}
	}

	private void createItem(int x, int y, string itemName, Texture2D icon) {
		if (GUI.Button (new Rect (x, y, itemSize.x, itemSize.y), icon)) {
			this.selectedItem = itemName;
			itemOwned = false;
			audioController.playClickSound();
		}
		//GUI.TextArea (new Rect (x, y, itemSize.x, itemSize.y), description);
	}

	private void createItemInfo() {
		drawTexture (0, 0, (int)windowSize.x, (int)windowSize.y, shopBox);

		string itemName = itemStats.getInventoryContent()[selectedInventoryIndex, 0];
		if (itemName.Equals ("empty") && itemOwned) {
			return;
		}

		// Item image:
		GUI.Box (new Rect(0, 0, itemSize.x+10, itemSize.y+10), itemStats.getItemIcon(selectedItem)); 

		createItemInfoButtons ();

		Vector2 itemInfoTextSize = new Vector2 (itemInfoSize.x - itemSize.x*1.35f, itemInfoSize.y*0.6f);
		GUI.BeginGroup (new Rect (itemSize.x*1.2f, 0, itemInfoTextSize.x, itemInfoTextSize.y));
		createTextItemInfo (itemInfoTextSize);
		GUI.EndGroup ();
		
	}

	private void createTextItemInfo(Vector2 itemInfoTextSize) {
		// Name and level
		string topic = "";
		if (itemOwned) {

			topic = itemStats.getItemName(selectedItem) + "\n" + "level " + itemStats.getItemLevel(selectedItem, selectedInventoryIndex);
		} else {
			topic = itemStats.getItemName(selectedItem) + "\n" + "level " + itemStats.getItemLevel (selectedItem, -1);
		}

		GUI.Label (new Rect (0, 0, itemInfoTextSize.x, itemInfoTextSize.y * 0.3f), topic, topicText);

		// Item description

		GUI.Label (new Rect (0, itemInfoTextSize.y * 0.4f, itemInfoTextSize.x, itemInfoTextSize.y * 0.3f), itemStats.getItemDescription(selectedItem), itemDescriptionText);

		// Item status
	
		if (!itemOwned) {
				int level = itemStats.getItemLevel (selectedItem, -1);
				int effect = itemStats.getItemEffect (selectedItem, -1);
				string itemStatus = "Level " + level + " stats" + "\n" + "Effect: " + effect;
				GUI.Label (new Rect (0, itemInfoTextSize.y * 0.7f, itemInfoTextSize.x / 2, itemInfoTextSize.y * 0.2f), itemStatus, itemStatusText);

				string nextLvlItemStatus = "Next Level " + (level + 1) + "\n" + "Effect: " + (effect + effect * itemStats.getLvlUpEffectFactor ());
				GUI.Label (new Rect (itemInfoTextSize.x / 2, itemInfoTextSize.y * 0.7f, itemInfoTextSize.x / 2, itemInfoTextSize.y * 0.2f), nextLvlItemStatus, itemStatusText);
		} else {
				int level = itemStats.getItemLevel (selectedItem, selectedInventoryIndex);
				int effect = itemStats.getItemEffect (selectedItem, selectedInventoryIndex);
				string itemStatus = "Level " + level + " stats" + "\n" + "Effect: " + effect;
				GUI.Label (new Rect (0, itemInfoTextSize.y * 0.7f, itemInfoTextSize.x / 2, itemInfoTextSize.y * 0.2f), itemStatus, itemStatusText);
		}

	}

	// Buy, sell, upgrade Buttons, cost and value fields
	private void createItemInfoButtons() {
		float offFromSide = itemInfoSize.x*0.1f;
		
		Vector2 firstButtonPos = new Vector2 (offFromSide, (itemInfoSize.y*0.65f));
		Vector2 secondButtonPos = new Vector2 (itemInfoSize.x - itemInfoButtonSize.x - offFromSide, firstButtonPos.y);
		if (itemOwned) {
			//createSellButton(0, itemInfoSize.y * 0.5f, itemInfoSize.x, itemInfoSize.y*0.2f);
			createSellButton(firstButtonPos.x, firstButtonPos.y);
			
		} else {
			createBuyButton(firstButtonPos.x, firstButtonPos.y);
			createUpgradeButton(secondButtonPos.x, secondButtonPos.y);
		}

		// Cost / value amount
		/*
		string text = "";
		if (itemOwned) {
			text = "Value: " + itemStats.getValueOfItem(selectedInventoryIndex);
		} else {
			text = "Cost: " + itemStats.getItemCost(selectedItem);
		}
		GUI.Label (new Rect (10, itemInfoSize.y - itemInfoSize.y*0.15f, itemSize.x + 10, itemSize.y*0.3f), text);
		*/
	}


	private void createBuyButton(float x, float y) {
		bool enoughGold = false;
		if (gameStatus.getGold () - itemStats.getItemCost (selectedItem) > 0) {
			enoughGold = true;
		}
		if(enoughGold) {
			if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Buy" + "\n" + "Cost: " + itemStats.getItemCost(selectedItem), buttonBg)) {
				itemStats.buyItem(selectedItem);
				audioController.playClickSound();
			}
		} else {
			if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Buy" + "\n" + "Cost: " + itemStats.getItemCost(selectedItem), buttonDeactivatedBg)) {
				itemStats.buyItem(selectedItem);
				audioController.playClickSound();
			}
		}
	}
	private void createSellButton(float x, float y) {
		if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Sell" + "\n" + "Value: " + itemStats.getValueOfItem(selectedInventoryIndex), buttonBg)) {
			bool itemSold = itemStats.sellItem(selectedInventoryIndex);
			audioController.playClickSound();

		}
	}
	
	private void createUpgradeButton(float x, float y) {
		bool enoughGold = false;
		if (gameStatus.getGold () - itemStats.getItemUpgradeCost (selectedItem) > 0) {
			enoughGold = true;
		}
		if (enoughGold) {
			if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Upgrade" + "\n" + "Cost: " + itemStats.getItemUpgradeCost (selectedItem), buttonBg)) {
					ItemStats.itemStats.levelUpItem (selectedItem);
					audioController.playClickSound ();
			}
		} else {
			if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Upgrade" + "\n" + "Cost: " + itemStats.getItemUpgradeCost (selectedItem), buttonDeactivatedBg)) {
				ItemStats.itemStats.levelUpItem (selectedItem);
				audioController.playClickSound ();
			}
		}
	
	}

	private void drawTexture(float x, float y, float width, float height, Texture texture) {
		GUI.DrawTexture (new Rect (x, y, width, height), texture, ScaleMode.ScaleToFit, true, width/height);
	}

	public void setItemOwned(bool b) {
		this.itemOwned = b;
	}
	public void setSelectedItem(string itemName) {
		this.selectedItem = itemName;
	}
	public void setSelectedInventoryIndex(int index) {
		this.selectedInventoryIndex = index;
	}

	private void showNotification(string text) {

	}
}





// DONT delete, needed in the future:


/*
private void createTabButton(float x, float y, float width, float height, string txt, int index) {
	if (GUI.Button (new Rect (x+width*(index-1f), y, width+(index-1f) * x, height), txt)) {
		clickedIndex = index;
		clickSound.Play ();
	}
}

	private void createScrollView(Vector2 tabSize, Vector2 windowSize, string type) {

		if (type.Equals ("shop")) {
			shopScrollPosition = GUI.BeginScrollView (new Rect (0, tabSize.y, windowSize.x, windowSize.y - tabSize.y), shopScrollPosition, new Rect (0, 0, 0, 500));
			createShopContent();
		} 

		else if (type.Equals ("stash")) {
			stashScrollPosition = GUI.BeginScrollView (new Rect (0, 0, windowSize.x, windowSize.y), stashScrollPosition, new Rect (0, 0, 0, 500));
			createStashContent();
		}

		GUI.EndScrollView ();
		GUI.EndGroup ();
	}
*/
