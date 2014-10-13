using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStatus : MonoBehaviour {
	public List<GameObject> allBacs = new List<GameObject>();
	public List<GameObject> selectedBacs = new List<GameObject>();

	//Real list is transform or gameobject and will display image not text, this is for testing
	public List<string> allBacsTest = new List<string>();
	public List<string> selectedBacsTest = new List<string>();
	public List<int[]> allBacsStats = new List<int[]>();

	public Dictionary<string, int[]> allBacteriaStats = new Dictionary<string, int[]>();

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

		//test (Health, Dmg, speed, level)
		allBacteriaStats.Add ("Gatbac", new int[] {200, 10, 5, 1});
		allBacteriaStats.Add ("Strep_p", new int[] {100, 10, 8, 1});
		allBacteriaStats.Add ("smallRed", new int[] {100, 10, 6, 1});
		allBacteriaStats.Add ("smallBlue", new int[] {100, 10, 6, 1});
		allBacteriaStats.Add ("smallPurple", new int[] {100, 10, 6, 1});
		allBacteriaStats.Add ("Phage", new int[] {100, 10, 4, 1});
		allBacteriaStats.Add ("blueBac", new int[] {100, 15, 10, 1});

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
		clearLevelBools();
		storedNode = node.transform.name;
	}

	public void clearLevelBools() {
		levelComplete = false;
		levelInterrupt = false;
	}
	
	//get entered node
	public string getNode() {
		return storedNode;
	}

	public void clearNode() {
		storedNode = null;
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
	
	public List<string> getSelectedBacsTest() {
		return selectedBacsTest;
	}

	public void setSelectedBacTest(string bac, int indx) {
		selectedBacsTest[indx] = bac;
	}

	public void removeSelectedBacTest(string bac) {
		selectedBacsTest.Remove(bac);
	}

	public void setAllBacteriaStats(string key, int health, int dmg, int speed, int lvl) {
		allBacteriaStats[key] = new int[] {health, dmg, speed, lvl};
	}

	public Dictionary<string, int[]> getAllBacteriaStats() {
		return allBacteriaStats;
	}
}
