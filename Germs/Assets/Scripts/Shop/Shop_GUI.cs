using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public class Shop_GUI : MonoBehaviour {

	//selection
	RaycastHit hit;
	RaycastHit storedHit;
	private float raycastLength = 200;

	public float gold = 0;
	public float xp = 0;
	
	private int amountOfIndexes = 3;
	private int amountOfItemColumns = 4;

	public Texture2D goldIcon;
	public Texture2D xpIcon;

	public Texture2D healPotion;
	public Texture2D ragePotion;
	public Texture2D speedPotion;

	public GUIStyle trainerHover;
	public GUIStyle mapHover;
	public GUIStyle bigNumbers;
	//public GUIStyle yellowText;
	//public GUIStyle blueText;
	
	public Texture shopBox;
	public Texture shopText;
	public Texture stashText;
	public Texture selectedItemWindow;
	
	private Transform gameStatus;
	private Transform battleTracker;

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

	private string selectedItemName = "healPotion";
	public Dictionary<string, Texture2D> itemIcons = new Dictionary<string, Texture2D>();
	private Dictionary<string, int[]> currentPotionStats;

	//string[] stashItems =  new string[] {"axe", "potion1", "potion2", "boots", "potion3"};
	//string[] armors = new string[] {"chainmail", "jacket", "leather coat"};
	//string[] weapons = new string[] {"dagger"};

	private int clickedIndex = 1;


	bool itemOwned = false;
	
	public AudioSource clickSound;
	
	// Use this for initialization
	void Start () {
		//gold = gameStatus.gameObject.GetComponent<GameStatus>().getGold();
		//xp = gameStatus.gameObject.GetComponent<GameStatus>().getXp();

		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> ();
		windowSize = new Vector2 (Screen.width * 0.45f, Screen.height * 0.45f);
		itemSize = new Vector2 (Screen.width*0.1f, Screen.width*0.1f);
		itemInfoSize = new Vector2(windowSize.x, windowSize.y);
		shopPos = new Vector2 (Screen.width*0.04f, Screen.height * 0.15f);
		itemInfoPos = new Vector2 (windowSize.x + shopPos.x + 25, shopPos.y);
		itemInfoButtonSize = new Vector2 (windowSize.x/3, itemInfoSize.y*0.25f);

		currentPotionStats = ItemStats.itemStats.getCurrentPotionStats();
		addPotionIcons ();
	
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
				createItem ((int)itemSize.x * column, (int)itemSize.y * row, key, itemIcons[key]);
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
			this.selectedItemName = itemName;
			itemOwned = false;
			clickSound.Play ();	
		}
		//GUI.TextArea (new Rect (x, y, itemSize.x, itemSize.y), description);
	}

	private void createItemInfo() {
		// Item image:
		GUI.Box (new Rect(0, 0, itemSize.y+10, itemSize.y+10), itemIcons[selectedItemName]); 
		float descriptionHeight = itemInfoSize.y * 0.2f;

		// Buy, sell, upgrade Buttons
		float offFromSide = itemInfoSize.x*0.1f;

		Vector2 firstButtonPos = new Vector2 (offFromSide, (itemInfoSize.y - itemSize.y + descriptionHeight -itemInfoButtonSize.y)/2+itemSize.y);
		Vector2 secondButtonPos = new Vector2 (itemInfoSize.x - itemInfoButtonSize.x - offFromSide, firstButtonPos.y);
		if (itemOwned) {
			//createSellButton(0, itemInfoSize.y * 0.5f, itemInfoSize.x, itemInfoSize.y*0.2f);

		} else {
			createBuyButton(firstButtonPos.x, firstButtonPos.y);
			createUpgradeButton(secondButtonPos.x, secondButtonPos.y);
		}

		
		// Item status
		GUI.BeginGroup (new Rect (itemSize.x + 20, 0, itemInfoSize.x - itemSize.x - 20, itemInfoSize.y + 20));
			GUI.Label(new Rect (0, 0, itemSize.y, (itemInfoSize.x - itemSize.x) / 2), "level jotain");
		GUI.EndGroup ();

		// Next level item status
		//GUI.BeginGroup (new Rect (itemSize.x + 20, 0, itemInfoSize.x - itemSize.x - 20, itemInfoSize.y + 20));
		//GUI.Label(new Rect (0, 0, itemSize.y, (itemInfoSize.x - itemSize.x) / 2), " next level jotain");
		//GUI.EndGroup ();
		
	}
	private void createBuyButton(float x, float y) {
		if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Buy")) {
			clickSound.Play ();	
		}
	}
	private void createSellButton(float x, float y) {
		if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Sell")) {
			clickSound.Play ();	
		}
	}
	private void createUpgradeButton(float x, float y) {
		if (GUI.Button (new Rect (x, y, itemInfoButtonSize.x, itemInfoButtonSize.y), "Upgrade")) {
			clickSound.Play ();	
		}
	}
	
	private void addPotionIcons() {
		itemIcons.Add ("healPotion", healPotion);
		itemIcons.Add ("ragePotion", ragePotion);
		itemIcons.Add ("speedPotion", speedPotion);
	}

	private void drawTexture(float x, float y, float width, float height, Texture texture) {
		GUI.DrawTexture (new Rect (x, y, width, height), texture, ScaleMode.ScaleToFit, true, width/height);
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
