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
	private Vector2 itemInfoPos;
	private Vector2 itemInfoSize;
	// GUI elements inside itemInfoWindow
	private Vector2 itemInfoStatsSize;
	private Vector2 itemInfoStatsPos;
	private Vector2 itemInfoButtonSize;

	// for testing, items hardcoded
	private Texture2D[] potionIcons = new Texture2D[5];

	private Dictionary<string, int[]> currentPotionStats;

	//string[] stashItems =  new string[] {"axe", "potion1", "potion2", "boots", "potion3"};
	//string[] armors = new string[] {"chainmail", "jacket", "leather coat"};
	//string[] weapons = new string[] {"dagger"};

	private int[] selectedItemInfo;

	private int clickedIndex = 1;


	bool itemOwned = false;
	
	public AudioSource clickSound;
	
	// Use this for initialization
	void Start () {
		//gold = gameStatus.gameObject.GetComponent<GameStatus>().getGold();
		//xp = gameStatus.gameObject.GetComponent<GameStatus>().getXp();

		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> ();
		windowSize = new Vector2 (Screen.width * 0.4f, Screen.height * 0.65f);
		itemSize = new Vector2 (Screen.width*0.1f, Screen.width*0.1f);
		itemInfoSize = new Vector2(windowSize.x*0.8f, Screen.height*0.4f);
		shopPos = new Vector2 (Screen.width*0.025f, Screen.height * 0.15f);
		itemInfoPos = new Vector2 (Screen.width * 0.575f, Screen.height * 0.15f);
		itemInfoStatsSize = new Vector2 (itemInfoSize.x*0.6f, itemInfoSize.y);
		itemInfoStatsPos = new Vector2 (itemInfoSize.x-(1f-itemInfoStatsSize.x), 0);
		itemInfoButtonSize = new Vector2 ((itemInfoSize.x - itemInfoStatsSize.x) * 0.8f, itemInfoSize.y * 0.1f);

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
			GUI.BeginGroup (new Rect (0, 0, itemInfoSize.x, itemInfoSize.y));
			createSelectedItemWindow ();
			GUI.EndGroup ();
		}
		
	}


	private void createShopContent() {
		if (clickedIndex == 1) {
			createContent(currentPotionStats, potionIcons);
		} 
		/*
		else if (clickedIndex == 2) {
			createContent(weapons);
		} else if (clickedIndex == 3) {
			createContent(armors);
		}
		*/
	}

	private void createContent(Dictionary<string, int[]> items, Texture2D[] icons) {


		int itemIndex = 0;
		int column = 0;
		int row = 0;
		foreach (string key in items.Keys) {
				createItem ((int)itemSize.x * column, (int)itemSize.y * row, key, itemIndex, icons[itemIndex]);
			if((itemIndex+1) % amountOfItemColumns == 0) {
				row++;
				column = 0;
			} else {
				column++;
			}
			itemIndex++;
		}
	}

	private void createItem(int x, int y, string description, int itemIndex, Texture2D icon) {
		if (GUI.Button (new Rect (x, y, itemSize.x, itemSize.y), icon)) {
			//this.selectedItemIndex = itemIndex;



			clickSound.Play ();	
		}
		//GUI.TextArea (new Rect (x, y, itemSize.x, itemSize.y), description);
	}

	private void createSelectedItemWindow () {
		GUI.Label (new Rect (0, 0, itemInfoSize.x, itemInfoSize.y), selectedItemWindow);


		//GUI.Box (new Rect(x, y, 100,100), 
		if (itemOwned) {
			createUpgradeAndSellButtons(0, itemInfoSize.y * 0.5f, itemInfoSize.x, itemInfoSize.y*0.2f);
		} else {
			createBuyButton(0, itemInfoSize.y * 0.55f, itemInfoSize.x, itemInfoSize.y*0.2f);
		}
	}
	
	private void createUpgradeAndSellButtons(float x, float y, float width, float height) {
		if (GUI.Button (new Rect (x, y, width, height), "Upgrade")) {
			Debug.Log ("upgraded item");
			clickSound.Play ();	
		}
		if (GUI.Button (new Rect (x, y+height*1.2f, width, height), "Sell")) {
			Debug.Log ("sold item");
			clickSound.Play ();	
		}
	}
	
	private void createBuyButton(float x, float y, float width, float height) {
		if (GUI.Button (new Rect (x, y, width, height), "Buy")) {

			clickSound.Play ();	
		}
	}

	private void addPotionIcons() {
		potionIcons [0] = healPotion;
		potionIcons [1] = ragePotion;
		potionIcons [2] = speedPotion;
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
