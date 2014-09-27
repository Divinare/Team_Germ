using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class storeBattleStatus : MonoBehaviour {
	public List<GameObject> allBacs = new List<GameObject>();
	public List<GameObject> selectedBacs = new List<GameObject>();

	//Real list is transform or gameobject and will display image not text, this is for testing
	public List<string> allBacsTest = new List<string>();
	public List<string> selectedBacsTest = new List<string>();

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

		//test
		allBacsTest.Add ("A");
		allBacsTest.Add ("B");
		allBacsTest.Add ("C");
		allBacsTest.Add ("D");
		allBacsTest.Add ("E");
		allBacsTest.Add ("F");
		allBacsTest.Add ("G");
		allBacsTest.Add ("");
		selectedBacsTest.Add ("");
		selectedBacsTest.Add ("");
		selectedBacsTest.Add ("");
		selectedBacsTest.Add ("");
		selectedBacsTest.Add ("");

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

	//testing methods
	public List<string> getAllBacsTest() {
		return allBacsTest;
	}
	
	public List<string> getSelectedBacsTest() {
		return selectedBacsTest;
	}

	public void setSelectedBacTest(string bac, int indx) {
		selectedBacsTest[indx] = bac;
	}

	public void removeSelectedBacTest(string bac) {
		selectedBacsTest.Remove(bac);
	}
}
