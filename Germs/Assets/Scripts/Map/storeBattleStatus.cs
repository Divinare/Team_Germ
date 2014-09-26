using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class storeBattleStatus : MonoBehaviour {
	public List<GameObject> allBacs = new List<GameObject>();
	public List<GameObject> selectedBacs = new List<GameObject>();

	public string storedNode;
	
	//level status/count
	public bool levelComplete;
	public bool levelInterrupt;
	public int levelsCompleted = 0;
	public int levelsFailed = 0;

	// Use this for initialization
	void Start () {
		//muy importanto!
		DontDestroyOnLoad(this);
		
		//remove duplicates
		if (FindObjectsOfType(GetType()).Length > 1) {
			Destroy(gameObject);
		}
	}

	//the level that was entered was...
	void levelCompleted() {
		levelsCompleted += 1;
		levelComplete = true;
	}

	void levelInterrupted() {
		levelInterrupt = true;
	}

	void levelFailed() {
		levelsFailed += 1;
	}

	//called on Start in map
	public bool onReturnToMap() {
		if (levelComplete) {
			return true;
		} else if (levelInterrupt) {
			return false;
		} else {
			return false;
		}
	}

	//store entered node
	void storeNode(Transform node) {
		levelComplete = false;
		levelInterrupt = false;
		storedNode = node.transform.name;
	}
	
	//get entered node
	public string getNode() {
		return storedNode;
	}

	void setBacSelected(GameObject bac) {
	}

	public List<GameObject> getSelectedBacs() {
		return selectedBacs;
	}

	void removeSelectedBac(GameObject bac) {
	}

	void setAllBacs(List<GameObject> allBacteria) {
		allBacs = allBacteria;
	}
}
