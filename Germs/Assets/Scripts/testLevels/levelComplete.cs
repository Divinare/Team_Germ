using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelComplete : MonoBehaviour {
	public List<GameObject> allEnemies = new List<GameObject>();
	public bool enemiesAlive;

	private Transform battleTracker;

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
			if(GUI.Button(new Rect(240, 20, 160, 40), "Complete level")) {
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
