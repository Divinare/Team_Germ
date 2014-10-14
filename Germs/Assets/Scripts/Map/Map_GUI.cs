using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map_GUI : MonoBehaviour {
	public float gold;
	public float xp;
	public bool bacChooser;
	public int clickedIndex;
	
	public Dictionary<string, int[]> allBacteriaStats = new Dictionary<string, int[]>();
	public List<string> selectedUnits = new List<string>();

	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public GUIStyle bigNumbers;
	public GUIStyle trainerHover;
	public GUIStyle shopHover;
	public AudioSource clickSound;

	public GameStatus gameStatus;
	public BattleStatus battleStatus;
 
	// Use this for initialization
	void Start () {
		//sanitycheck
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();

		clickSound = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioSource> (); 

		gold = gameStatus.getGold();
		xp = gameStatus.getXp();
		bacChooser = false;

		allBacteriaStats = battleStatus.getAllBacteriaStats();
		selectedUnits = battleStatus.getSelectedUnits();
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
			if (GUI.Button (new Rect (Screen.width/2 - Screen.width/4,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[0])) {
				clickedIndex = 0;
				bacteriaChooserOn();
			}
			if (GUI.Button (new Rect (Screen.width/2 - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[1])) {
				clickedIndex = 1;
				bacteriaChooserOn();
			}
			if (GUI.Button (new Rect (Screen.width/2 - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[2])) {
				clickedIndex = 2;
				bacteriaChooserOn();
			}
			if (GUI.Button (new Rect (Screen.width/2,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[3])) {
				clickedIndex = 3;
				bacteriaChooserOn();
			}
			if (GUI.Button (new Rect (Screen.width/2 + Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), selectedUnits[4])) {
				clickedIndex = 4;
				bacteriaChooserOn();
			}

		if (bacChooser) {
			var pos = 0;
			foreach (string bac in allBacteriaStats.Keys) {
				if (!selectedUnits.Contains(bac)) {
					if (GUI.Button (new Rect (Screen.width/8 +pos,Screen.height - Screen.height/4,Screen.width/12,Screen.height/10), bac)) {
						battleStatus.setSelectedUnit(bac, clickedIndex);
						bacChooser = false;
					}
					pos += Screen.width/12;
				}
			}
		}

	}
	void bacteriaChooserOn() {
		bacChooser = true;
		allBacteriaStats = battleStatus.getAllBacteriaStats();
	}
}
