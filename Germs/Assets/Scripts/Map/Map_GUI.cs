using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map_GUI : MonoBehaviour {
	public float gold;
	public float xp;
	public bool bacChooser;
	public int clickedIndex;

	//Real list is transform or gameobject and will display image not text, this is for testing
	public Dictionary<string, int[]> allBacteriaStats = new Dictionary<string, int[]>();
	public List<string> selectedBacsTest = new List<string>();

	public Texture2D goldIcon;
	public Texture2D xpIcon;

	public GUIStyle bigNumbers;
	public GUIStyle trainerHover;
	public GUIStyle shopHover;
	public AudioSource clickSound;

	//GameStateObject
	private Transform gameStatus;
	private Transform battleTracker;
 
	// Use this for initialization
	void Start () {
		gameStatus = GameObject.Find("GameStatus").transform;
		battleTracker = GameObject.Find ("BattleTracker").transform;
		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> (); 

		gold = gameStatus.gameObject.GetComponent<GameStatus>().getGold();
		xp = gameStatus.gameObject.GetComponent<GameStatus>().getXp();
		bacChooser = false;

		allBacteriaStats = battleTracker.gameObject.GetComponent<BattleStatus>().getAllBacteriaStats();
		selectedBacsTest =battleTracker.gameObject.GetComponent<BattleStatus>().getSelectedBacsTest();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//gold and xp
		GUI.Box (new Rect (Screen.width - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), xpIcon);
		GUI.Label(new Rect(Screen.width - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), xp.ToString(), bigNumbers);

		GUI.Box (new Rect (Screen.width - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), goldIcon);
		GUI.Label(new Rect(Screen.width - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), gold.ToString(), bigNumbers);

		//shop buttons

		if (GUI.Button (new Rect (0 + Screen.height/6,Screen.height - Screen.height/12,Screen.height/6,Screen.height/12), "", shopHover)) {
			clickSound.Play ();	
			Application.LoadLevel ("Shop");
			Debug.Log ("Shop");
		}
		if (GUI.Button (new Rect (0,Screen.height - Screen.height/12,Screen.height/6,Screen.height/12), "", trainerHover)) {
			clickSound.Play ();	
			Application.LoadLevel ("Trainer");
			Debug.Log ("Training");
		}

		//frame for chosen bacteria
			if (GUI.Button (new Rect (Screen.width/2 - Screen.width/4,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedBacsTest[0])) {
				clickedIndex = 0;
				bacteriaChooserOn();
			}
			if (GUI.Button (new Rect (Screen.width/2 - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedBacsTest[1])) {
				clickedIndex = 1;
				bacteriaChooserOn();
			}
			if (GUI.Button (new Rect (Screen.width/2 - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedBacsTest[2])) {
				clickedIndex = 2;
				bacteriaChooserOn();
			}
			if (GUI.Button (new Rect (Screen.width/2,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedBacsTest[3])) {
				clickedIndex = 3;
				bacteriaChooserOn();
			}
			if (GUI.Button (new Rect (Screen.width/2 + Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedBacsTest[4])) {
				clickedIndex = 4;
				bacteriaChooserOn();
			}

		if (bacChooser) {
			var pos = 0;
			foreach (string bac in allBacteriaStats.Keys) {
				if (!selectedBacsTest.Contains(bac)) {
					if (GUI.Button (new Rect (Screen.width/8 +pos,Screen.height - Screen.height/4,Screen.width/12,Screen.height/10), bac)) {
						battleTracker.gameObject.GetComponent<BattleStatus>().setSelectedBacTest(bac, clickedIndex);
						bacChooser = false;
					}
					pos += Screen.width/12;
				}
			}
		}

	}
	void bacteriaChooserOn() {
		bacChooser = true;
		allBacteriaStats = battleTracker.gameObject.GetComponent<BattleStatus>().getAllBacteriaStats();
	}
}
