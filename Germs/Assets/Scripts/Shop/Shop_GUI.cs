using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shop_GUI : MonoBehaviour {

	//selection
	RaycastHit hit;
	RaycastHit storedHit;
	private float raycastLength = 200;

	public float gold = 0;
	public float xp = 0;

	private int clickedIndex = 1;
	private int amountOfIndexes = 4;
	private int amountOfItemColumns = 4;

	public Texture2D goldIcon;
	public Texture2D xpIcon;

	public GUIStyle trainerHover;
	public GUIStyle shopHover;
	public GUIStyle bigNumbers;

	public Texture menu;
	public Texture shopText;
	public Texture stashText;
	public Texture selectedItemWindow;

	public List<string> selectedItems = new List<string>();

	private Transform gameStatus;
	private Transform battleTracker;

	private float hScrollbarValue;
	public Vector2 shopScrollPosition = Vector2.zero;
	public Vector2 stashScrollPosition = Vector2.zero;

	// items hardcoded
	string[] stashItems =  new string[] {"axe", "potion1", "potion2", "boots", "potion3"};


	//sound
	public AudioSource clickSound;

	//Size of GUI elements
	private Vector2 windowSize = new Vector2 (200, 300);
	private Vector2 itemSize = new Vector2 (95, 95);
	private Vector2 selectedItemWindowSize = new Vector2(200, 200);

	// Use this for initialization
	void Start () {
		//gold = gameStatus.gameObject.GetComponent<GameStatus>().getGold();
		//xp = gameStatus.gameObject.GetComponent<GameStatus>().getXp();
		this.selectedItems.Add ("Miekka");
		this.selectedItems.Add ("Potion1");
		this.selectedItems.Add ("Potion1");
		this.selectedItems.Add ("Potion1");
		this.selectedItems.Add ("Potion1");

		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> (); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

		// Creating stash
		createMenu ("shop", Screen.width*0.025f, Screen.height * 0.05f, shopScrollPosition);

		// Creating shop
		createMenu ("stash", Screen.width * 0.575f, Screen.height * 0.05f, stashScrollPosition);

		drawSelectedItems ();

		createExpAndGoldIcons ();

		createMapAndTrainingButtons ();

		createSelectedItemWindow ((int)(Screen.width *  0.4362), (int)(Screen.height * 0.2), (int)(selectedItemWindowSize.x), (int)(selectedItemWindowSize.y));
	}

	private void createMenu(string type, float x, float y, Vector2 scrollPosition) {


	//	GameObject shopMenu = GameObject.FindGameObjectWithTag ("ShopMenu");
		Vector2 windowPosition = new Vector2 (x, y);
		Vector2 windowSize = new Vector2 (Screen.width * 0.4f, Screen.height * 0.65f);
		Vector2 topicSize = new Vector2 (windowSize.x, Screen.height * 0.1f);
		Vector2 tabSize = new Vector2 (windowSize.x, Screen.height * 0.05f);
		GUI.BeginGroup (new Rect (windowPosition.x, windowPosition.y, windowSize.x, windowSize.y), menu);


		    // Scroll view
		if(type.Equals("shop")) {
			// Creating topic picture
			GUI.Label (new Rect (0, 0, topicSize.x, topicSize.y), shopText);
			// Creating tab bar
			createTabButton(0,topicSize.y,tabSize.x/amountOfIndexes,tabSize.y, "Armour", 1);
			createTabButton(0,topicSize.y,tabSize.x/amountOfIndexes,tabSize.y, "Weapons", 2);
			createTabButton(0,topicSize.y,tabSize.x/amountOfIndexes,tabSize.y, "Skills", 3);
			createTabButton(0,topicSize.y,tabSize.x/amountOfIndexes,tabSize.y, "Potions", 4);

			createScrollView(topicSize, tabSize, windowSize, "shop");

		} else if(type.Equals("stash")) {
			// Creating topic picture
			GUI.Label (new Rect (0, 0, topicSize.x, topicSize.y), stashText);

			createScrollView(topicSize, tabSize, windowSize, "stash");

		}
		
	}

	
	private void createTabButton(float x, float y, float width, float height, string txt, int index) {
		if (GUI.Button (new Rect (x+width*(index-1f), y, width+(index-1f) * x, height), txt)) {
			clickedIndex = index;
		}
	}

	private void createScrollView(Vector2 topicSize, Vector2 tabSize, Vector2 windowSize, string type) {

		if (type.Equals ("shop")) {
			shopScrollPosition = GUI.BeginScrollView (new Rect (0, topicSize.y + tabSize.y, windowSize.x, windowSize.y - topicSize.y - tabSize.y), shopScrollPosition, new Rect (0, 0, 0, 500));

		} else if (type.Equals ("stash")) {
			stashScrollPosition = GUI.BeginScrollView (new Rect (0, topicSize.y, windowSize.x, windowSize.y - topicSize.y), stashScrollPosition, new Rect (0, 0, 0, 500));
			createStashContent();
		}

		GUI.EndScrollView ();
		
		GUI.EndGroup ();

	}

	private void createShopContent() {
		createItem (0, 0, "axe");
		GUI.TextArea (new Rect (100, 100, 75, 75), "items come here!");
		GUI.TextArea (new Rect (100, 200, 100, 100), "items come here!");

	}

	private void createStashContent() {
		createContent (stashItems);
	}

	private void createContent(string[] items) {
		int column = 0;
		int row = 0;
		for (int i = 1; i <= stashItems.Length; i++) {
			createItem ((int)itemSize.x * column, (int)itemSize.y * row, stashItems[i-1]);
			if(i % amountOfItemColumns == 0) {
				row++;
				column = 0;
			} else {
				column++;
			}
		}
	}


	private void createItem(int x, int y, string description) {
		Debug.Log ("x: " + x + " y: " + y);
		GUI.TextArea (new Rect (x, y, itemSize.x, itemSize.y), description);

	}

	private void drawSelectedItems() {
		if (GUI.Button (new Rect (Screen.width/2 - Screen.width/4,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedItems[0])) {
			
		}
		if (GUI.Button (new Rect (Screen.width/2 - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedItems[1])) {
			
		}
		if (GUI.Button (new Rect (Screen.width/2 - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedItems[2])) {
			
		}
		if (GUI.Button (new Rect (Screen.width/2,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedItems[3])) {
			
		}
		if (GUI.Button (new Rect (Screen.width/2 + Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedItems[4])) {
			
		}
	}

	private void createExpAndGoldIcons() {
		GUI.Box (new Rect (Screen.width - Screen.width / 6, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), xpIcon);
		GUI.Label (new Rect (Screen.width - Screen.width / 6, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), xp.ToString (), bigNumbers);
		
		GUI.Box (new Rect (Screen.width - Screen.width / 12, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), goldIcon);
		GUI.Label (new Rect (Screen.width - Screen.width / 12, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), gold.ToString (), bigNumbers);
	}

	private void createMapAndTrainingButtons() {
		if (GUI.Button (new Rect (0 + Screen.height/6,Screen.height - Screen.height/12,Screen.height/6,Screen.height/12), "", shopHover)) {
			clickSound.Play ();	
			Application.LoadLevel ("Map");
			Debug.Log ("Map");
		}
		if (GUI.Button (new Rect (0,Screen.height - Screen.height/12,Screen.height/6,Screen.height/12), "", trainerHover)) {
			clickSound.Play ();	
			Debug.Log ("Training");
		}
	}

	private void createSelectedItemWindow (int x, int y, int width, int height) {
		GUI.Label (new Rect (x, y, width, height), selectedItemWindow);
	}

}
