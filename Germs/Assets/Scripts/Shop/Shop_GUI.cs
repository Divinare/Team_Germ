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

	private int clickedIndex = 1;
	private int amountOfIndexes = 3;
	private int amountOfItemColumns = 4;

	public Texture2D goldIcon;
	public Texture2D xpIcon;

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
	public Vector2 shopScrollPosition = Vector2.zero;
	public Vector2 stashScrollPosition = Vector2.zero;

	// for testing, items hardcoded
	string[] stashItems =  new string[] {"axe", "potion1", "potion2", "boots", "potion3"};
	string[] armors = new string[] {"chainmail", "jacket", "leather coat"};
	string[] potions = new string[] {"health potion", "rage potion"};
	string[] weapons = new string[] {"dagger"};

	bool itemOwned = false;
	
	public AudioSource clickSound;

	//Size of GUI elements
	private Vector2 windowSize;
	private Vector2 itemSize;
	private Vector2 selectedItemWindowSize;

	// Use this for initialization
	void Start () {
		//gold = gameStatus.gameObject.GetComponent<GameStatus>().getGold();
		//xp = gameStatus.gameObject.GetComponent<GameStatus>().getXp();

		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> ();
		windowSize = new Vector2 (Screen.width * 0.4f, Screen.height * 0.65f);
		itemSize = new Vector2 (Screen.width*0.1f, Screen.width*0.1f);
		selectedItemWindowSize = new Vector2(Screen.width*0.13f, Screen.height*0.4f);

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {

		// Creating stash
		createMenu ("shop", Screen.width*0.025f, Screen.height * 0.15f, shopScrollPosition);

		// Creating shop
		createMenu ("stash", Screen.width * 0.575f, Screen.height * 0.15f, stashScrollPosition);

		createSelectedItemWindow ((Screen.width *  0.4362f), (Screen.height * 0.3f));
	}

	private void createMenu(string type, float x, float y, Vector2 scrollPosition) {

		Vector2 windowPosition = new Vector2 (x, y);

		Vector2 tabSize = new Vector2 (windowSize.x, Screen.height * 0.05f);

		// Draw topics
		if (type.Equals ("shop")) {
			drawTexture(x, Screen.height * 0.05f, windowSize.x, Screen.height * 0.10f, stashText);
		} else if (type.Equals ("stash")) {
			drawTexture(x, Screen.height * 0.05f, windowSize.x, Screen.height * 0.10f, stashText);
		}

		GUI.BeginGroup (new Rect (windowPosition.x, windowPosition.y, windowSize.x, windowSize.y));
		drawTexture (0, 0, (int)windowSize.x, (int)windowSize.y, shopBox);

		if(type.Equals("shop")) {

			// Creating tab bar
			createTabButton(0,0,tabSize.x/amountOfIndexes,tabSize.y, "Potion", 1);
			createTabButton(0,0,tabSize.x/amountOfIndexes,tabSize.y, "Weapons", 2);
			createTabButton(0,0,tabSize.x/amountOfIndexes,tabSize.y, "Armor", 3);

			createScrollView(tabSize, windowSize, "shop");

		} else if(type.Equals("stash")) {

			createScrollView(tabSize, windowSize, "stash");

		}
		
	}

	
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
		} else if (type.Equals ("stash")) {
			stashScrollPosition = GUI.BeginScrollView (new Rect (0, 0, windowSize.x, windowSize.y), stashScrollPosition, new Rect (0, 0, 0, 500));
			createStashContent();
		}
		GUI.EndScrollView ();
		GUI.EndGroup ();
	}

	private void createShopContent() {
		if (clickedIndex == 1) {
			createContent(potions);
		} else if (clickedIndex == 2) {
			createContent(weapons);
		} else if (clickedIndex == 3) {
			createContent(armors);
		}
	}

	private void createStashContent() {
		createContent (stashItems);
	}

	private void createContent(string[] items) {
		int column = 0;
		int row = 0;
		for (int i = 1; i <= items.Length; i++) {
			createItem ((int)itemSize.x * column, (int)itemSize.y * row, items[i-1]);
			if(i % amountOfItemColumns == 0) {
				row++;
				column = 0;
			} else {
				column++;
			}
		}
	}

	private void createSelectedItemWindow (float x, float y) {
		GUI.Label (new Rect (x, y, selectedItemWindowSize.x, selectedItemWindowSize.y), selectedItemWindow);
		
		if (itemOwned) {
			createUpgradeAndSellButtons(x, y+selectedItemWindowSize.y * 0.5f, selectedItemWindowSize.x, selectedItemWindowSize.y*0.2f);
		} else {
			createBuyButton(x, y+selectedItemWindowSize.y * 0.55f, selectedItemWindowSize.x, selectedItemWindowSize.y*0.2f);
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
			Debug.Log ("bought item");
			clickSound.Play ();	
		}
	}

	private void createItem(int x, int y, string description) {
		GUI.TextArea (new Rect (x, y, itemSize.x, itemSize.y), description);
	}


	private void drawTexture(float x, float y, float width, float height, Texture texture) {
		GUI.DrawTexture (new Rect (x, y, width, height), texture, ScaleMode.ScaleToFit, true, width/height);
	}
}
