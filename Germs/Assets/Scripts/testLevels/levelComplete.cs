using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class levelComplete : MonoBehaviour {
	public List<GameObject> allEnemies = new List<GameObject>();
	public bool enemiesAlive;

	private Transform battleTracker;

	public Texture2D victoryIcon;

	// Use this for initialization
	void Start () {
		battleTracker = GameObject.Find ("BattleTracker").transform;
		enemiesAlive = true;
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
				enemiesAlive = false;
				levelCompleted();
		}
	}

	void OnGUI() {
		if (GUI.Button (new Rect (0,Screen.height - 50,100,50), "Return to Map")) {
			levelInterrupted();
			Application.LoadLevel ("Map");
		}
		if (!enemiesAlive) {
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/4), "");
			GUI.Box ( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/4), victoryIcon);
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, Screen.height/12, Screen.width/2, Screen.height/4), "You have defeated all the enemies and will be greatly rewarded!\n Congratulations and Celebrations!");
			if(GUI.Button(new Rect(Screen.width/2 - Screen.width/14, Screen.height/4, Screen.width/8, Screen.height/16), "Yay!")) {
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
}
