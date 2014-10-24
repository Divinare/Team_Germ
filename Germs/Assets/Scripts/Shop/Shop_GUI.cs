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
	public GUIStyle yellowText;
	public GUIStyle blueText;
	public GUIStyle blackText;

	public Texture shopBox;
	public Texture shopText;
	public Texture stashText;
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

	private string selectedItem = "healPotion";
	bool itemOwned = false;
	private int selectedInventoryIndex = -1; // 1 - inventorySize

	private Dictionary<string, int[]> currentPotionStats;

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
		itemInfoButtonSize = new Vector2 (windowSize.x/3, itemInfoSize.y*0.25f);

		currentPotionStats = ItemStats.itemStats.getCurrentPotionStats();
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {


		// Creating shop
		GUI.BeginGroup (new Rect (shopPos.x, shopPos.y, windowSize.x, windowSize.y));
		createMenu ("shop");
		GUI.EndGroup ();

		// Creating info window
		GUI.BeginGroup (new Rect (itemInfoPos.x, itemInfoPos.y, windowSize.x, windowSize.y));
		createMenu ("itemInfo");
		GUI.EndGroup ();

	}

	private void createMenu(string type) {

		Vector2 tabSize = new Vector2 (windowSize.x, Screen.height * 0.05f);

		// Draw topics
		if (type.Equals ("shop")) {
			//drawTexture(0, Screen.height * 0.05f, windowSize.x/2, Screen.height * 0.10f, stashText);
		} else if (type.Equals ("itemInfo")) {
			//drawTexture(0, Screen.height * 0.05f, windowSize.x/2, Screen.height * 0.10f, stashText);
		}


		drawTexture (0, 0, (int)windowSize.x, (int)windowSize.y, shopBox);

		if(type.Equals("shop")) {

			createShopContent();

		} else if(type.Equals("itemInfo")) {
		
			createItemInfo();
			//GUI.BeginGroup (new Rect (itemInfoRightSidePos.x, itemInfoRightSidePos.y, itemInfoSize.x, itemInfoSize.y));
			//	createItemInfoRightSide();
			//GUI.EndGroup ();

		}
		
	}


	private void createShopContent() {
		if (clickedIndex == 1) {
			createContent(currentPotionStats);
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
				createItem ((int)itemSize.x * column, (int)itemSize.y * row, key, itemStats.itemIcons[key]);
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
		// Item image:
		GUI.Box (new Rect(0, 0, itemSize.y+10, itemSize.y+10), itemStats.itemIcons[selectedItem]); 
		float descriptionHeight = itemInfoSize.y * 0.2f;

		// Buy, sell, upgrade Buttons
		float offFromSide = itemInfoSize.x*0.1f;

		Vector2 firstButtonPos = new Vector2 (offFromSide, (itemInfoSize.y - itemSize.y + descriptionHeight -itemInfoButtonSize.y)/2+itemSize.y);
		Vector2 secondButtonPos = new Vector2 (itemInfoSize.x - itemInfoButtonSize.x - offFromSide, firstButtonPos.y);
		if (itemOwned) {
			//createSellButton(0, itemInfoSize.y * 0.5f, itemInfoSize.x, itemInfoSize.y*0.2f);
			createSellButton(firstButtonPos.x, firstButtonPos.y);
			createUpgradeButton(secondButtonPos.x, secondButtonPos.y);

		} else {
			createBuyButton(firstButtonPos.x, firstButtonPos.y);
			createUpgradeButton(secondButtonPos.x, secondButtonPos.y);
		}

		Vector2 itemInfoTextSize = new Vector2 (itemInfoSize.x - itemSize.x*1.35f, itemInfoSize.y*0.6f);
		GUI.BeginGroup (new Rect (itemSize.x*1.2f, 0, itemInfoTextSize.x, itemInfoTextSize.y));
		createTextItemInfo (itemInfoTextSize);
		GUI.EndGroup ();
		
	}

	private void createTextItemInfo(Vector2 itemInfoTextSize) {
		GUI.Box (new Rect(0, 0,  itemInfoTextSize.x,  itemInfoTextSize.y), "");
		// Name
		string description = "";
		if (itemOwned) {
			description = selectedItem + "\n" + "level " + itemStats.getItemLevel(selectedItem, selectedInventoryIndex);
		} else {
			description = selectedItem + "\n" + "level " + itemStats.getItemLevel (selectedItem, -1);
		}
		GUI.Label (new Rect (0, 0, itemInfoTextSize.x, itemInfoTextSize.y * 0.2f), description, blackText);


		// Item status
		
		//GUI.Label(new Rect (0, 0, itemSize.y, (itemInfoSize.x - itemSize.x) / 2), "level jotain");
		
		// Next level item status
		
		//GUI.Label(new Rect (0, 0, itemSize.y, (itemInfoSize.x - itemSize.x) / 2), " next level jotain");
	}


	private void createBuyButton(float x, float y) {
		if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Buy")) {
			itemStats.addToInventory(selectedItem);
			audioController.playClickSound();
		}
	}
	private void createSellButton(float x, float y) {
		if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Sell")) {
			itemStats.sellItem(selectedInventoryIndex);
			audioController.playClickSound();
		}
	}
	private void createUpgradeButton(float x, float y) {
		if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Upgrade")) {
			ItemStats.itemStats.levelUpItem(selectedItem);
			audioController.playClickSound();
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
