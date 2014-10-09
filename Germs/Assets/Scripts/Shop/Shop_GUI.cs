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

	public Texture2D goldIcon;
	public Texture2D xpIcon;

	public GUIStyle trainerHover;
	public GUIStyle shopHover;

	public GUIStyle bigNumbers;

	public List<string> selectedItems = new List<string>();

	private Transform gameStatus;
	private Transform battleTracker;

	private float hScrollbarValue;
	public Vector2 scrollPosition = Vector2.zero;

	//sound
	public AudioSource clickSound;

	//Size of GUI elements
	private int MenuWidth = 400;
	private int MenuHeight = 700;




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

		// http://www.youtube.com/watch?v=VwgDfVs8TNI

		createShopMenu ();

		drawSelectedItems ();

		createExpAndGoldIcons ();

		createMapAndTrainingButtons ();

	}

	private void createShopMenu() {
		float x = Screen.width*0.2f;
		float y = Screen.height*0.31f;

		// Vertical scrollbar
		//hScrollbarValue = GUI.VerticalScrollbar (new Rect (205, 100, 100, 30), hScrollbarValue, 1.0f, 0.0f, 10.0f);

		//Sroll view		// alwaysShowVertical: true
		scrollPosition = GUI.BeginScrollView (new Rect (x, y, x-Screen.width*0.05f, y+Screen.height*0.05f), scrollPosition, new Rect (0, 0, 0, MenuHeight*3));
	

		createIcon (0, 0);
		GUI.TextArea (new Rect (100, 100, 75, 75), "items come here!");
		GUI.TextArea (new Rect (100, 200, 100, 100), "items come here!");
		GUI.EndScrollView ();
	}

	private void createIcon(int x, int y) {
		GUI.TextArea (new Rect (x, y, 50, 50), "items come here!");

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
}
