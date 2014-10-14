using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStatus : MonoBehaviour {
	public static BattleStatus battleStatus;

	public List<GameObject> allUnits = new List<GameObject>();
	public List<string> selectedUnits = new List<string>();
	public Dictionary<string, int[]> allBacteriaStats = new Dictionary<string, int[]>();
	public int[] unravelArray = new int[4];

	// Use this for initialization
	void Start () {
		if (battleStatus == null) {
			DontDestroyOnLoad (gameObject);
			battleStatus = this;
		} else if (battleStatus != this) {
			Destroy (gameObject);
		}

		//test int[] {Health, Dmg, speed, level}
		allBacteriaStats.Add ("Gatbac", new int[] {200, 10, 5, 1});
		allBacteriaStats.Add ("Strepto", new int[] {100, 10, 8, 1});
		allBacteriaStats.Add ("smallRed", new int[] {100, 10, 6, 1});
		allBacteriaStats.Add ("smallBlue", new int[] {100, 10, 6, 1});
		allBacteriaStats.Add ("smallPurple", new int[] {100, 10, 6, 1});
		allBacteriaStats.Add ("Phage", new int[] {100, 10, 4, 1});
		allBacteriaStats.Add ("blueBac", new int[] {100, 15, 10, 1});

		selectedUnits.Add ("");
		selectedUnits.Add ("");
		selectedUnits.Add ("");
		selectedUnits.Add ("");
		selectedUnits.Add ("");
	}

	//testing methods
	
	public List<string> getSelectedUnits() {
		return selectedUnits;
	}

	public void setSelectedUnit(string bac, int indx) {
		selectedUnits[indx] = bac;
	}

	public void removeSelectedUnit(string bac) {
		selectedUnits.Remove(bac);
	}

	public void setAllBacteriaStats(string key, int health, int dmg, int speed, int lvl) {
		allBacteriaStats[key] = new int[] {health, dmg, speed, lvl};
	}

	//int[] {Health, Dmg, speed, level}
	public int getBacteriaHealth(string key) {
		unravelArray = allBacteriaStats[key];
		return unravelArray[0];
	}

	public int getBacteriaDamage(string key) {
		unravelArray = allBacteriaStats[key];
		return unravelArray[1];
	}

	public int getBacteriaSpeed(string key) {
		unravelArray = allBacteriaStats[key];
		return unravelArray[2];
	}

	public int getBacteriaLevel(string key) {
		unravelArray = allBacteriaStats[key];
		return unravelArray[3];
	}

	public Dictionary<string, int[]> getAllBacteriaStats() {
		return allBacteriaStats;
	}
}
