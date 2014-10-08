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

	//GameStateObject
	private Transform gameStatus;
	private Transform battleTracker;

	//sound
	public AudioSource clickSound;

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
			//gold and xp
			GUI.Box (new Rect (Screen.width - Screen.width / 6, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), xpIcon);
			GUI.Label (new Rect (Screen.width - Screen.width / 6, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), xp.ToString (), bigNumbers);
	
			GUI.Box (new Rect (Screen.width - Screen.width / 12, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), goldIcon);
			GUI.Label (new Rect (Screen.width - Screen.width / 12, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), gold.ToString (), bigNumbers);

		// 
		if (GUI.Button (new Rect (0 + Screen.height/6,Screen.height - Screen.height/12,Screen.height/6,Screen.height/12), "", shopHover)) {
			clickSound.Play ();	
			Application.LoadLevel ("Map");
			Debug.Log ("Map");
		}
		if (GUI.Button (new Rect (0,Screen.height - Screen.height/12,Screen.height/6,Screen.height/12), "", trainerHover)) {
			clickSound.Play ();	
			Debug.Log ("Training");
		}

		drawSelectedItems ();

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
}
