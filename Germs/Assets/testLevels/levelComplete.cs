using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class levelComplete : MonoBehaviour {
	public List<GameObject> allEnemies = new List<GameObject>();
	public List<string> selectedBacsTest = new List<string>();
	public bool enemiesAlive;
	public bool unitsAlive;

	private Transform battleTracker;

	public Texture2D victoryIcon;
	public Texture2D defeatIcon;

	// Use this for initialization
	void Start () {
		battleTracker = GameObject.Find ("BattleTracker").transform;
		selectedBacsTest = battleTracker.gameObject.GetComponent<BattleStatus>().getSelectedBacsTest();
		enemiesAlive = true;
		unitsAlive = true;
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && enemiesAlive == true) {
				enemiesAlive = false;
				levelCompleted();
		}
		if (GameObject.FindGameObjectsWithTag("Unit").Length == 0 && unitsAlive == true) {
			unitsAlive = false;
			levelFailed();
		}
	}

	void OnGUI() {
		GUI.Label( new Rect(0, 0, 100, 100), "Your bacteria: "+selectedBacsTest[0]+selectedBacsTest[1]+selectedBacsTest[2]+selectedBacsTest[3]+selectedBacsTest[4]);
		if (GUI.Button (new Rect (0,Screen.height - 50,100,50), "Return to Map")) {
			levelInterrupted();
			Application.LoadLevel ("Map");
		}
		if (!enemiesAlive) {
			//win
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/4), "");
			GUI.Box ( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/4), victoryIcon);
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, Screen.height/12, Screen.width/2, Screen.height/4), "You have defeated all the enemies and will be greatly rewarded!\n Congratulations and Celebrations!");
			if(GUI.Button(new Rect(Screen.width/2 - Screen.width/14, Screen.height/4, Screen.width/8, Screen.height/16), "Yay!")) {
				Application.LoadLevel ("Map");
			}
		} 
		if (!unitsAlive) {
			//fail
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/4), "");
			GUI.Box ( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/4), defeatIcon);
			//GUI.Box( new Rect(Screen.width/2 - Screen.width/4, Screen.height/12, Screen.width/2, Screen.height/4), "You have been deafeated. \n How could this happen?!");
			if(GUI.Button(new Rect(Screen.width/2 - Screen.width/14, Screen.height/4, Screen.width/8, Screen.height/16), "Oh woe is me")) {
				Application.LoadLevel ("Map");
			}
		}
	}
	void levelCompleted() {
		battleTracker.SendMessage("levelCompleted");
	}

	void levelInterrupted() {
		battleTracker.SendMessage("levelInterrupted");
	}

	void levelFailed() {
		battleTracker.SendMessage("levelFailed");
	}
}
