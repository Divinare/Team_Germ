using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleStatus : MonoBehaviour {
	public static BattleStatus battleStatus;

	public List<GameObject> allUnits = new List<GameObject>();
	public List<string> selectedUnits = new List<string>();
	public Dictionary<string, int[]> baseUnitStats = new Dictionary<string, int[]>();
	public List<string> enemiesToSpawn = new List<string>();
	private int[] unravelArray = new int[7];
	private int spawnNum;

	private GameStatus gameStatus;
	private UnitStats unitStats;

	// Use this for initialization
	void Start () {
		if (battleStatus == null) {
			DontDestroyOnLoad (gameObject);
			battleStatus = this;
		} else if (battleStatus != this) {
			Destroy (gameObject);
		}
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();
		baseUnitStats = unitStats.getBaseUnitStats();

		//initial selection
		selectedUnits.Add ("Salmonella");
		selectedUnits.Add ("Strepto");
		selectedUnits.Add ("empty");
		selectedUnits.Add ("empty");
		selectedUnits.Add ("empty");

	}

	public void randomEnemiesForLevel() {

		enemiesToSpawn.Clear();
		int levelsCompleted = gameStatus.getCompletedLevels();

		//limit amount of enemies based on what level is underway
		if (levelsCompleted <= 1) {
			spawnNum = 2;
		}
		else if (levelsCompleted > 1 && levelsCompleted <= 3) {
			spawnNum = 3;
		}
		else if (levelsCompleted > 3 && levelsCompleted <=5) {
			spawnNum = 4;
		} else {
			spawnNum = 5;
		}

		//randoms enemies
		for (int i=0; i<spawnNum; i++) {
			var randomKey = baseUnitStats.Keys.ToArray()[(int)Random.Range(0,baseUnitStats.Keys.Count)];
			enemiesToSpawn.Add (randomKey);
		}
	}

	public List<string> getEnemiesToSpawn() {
		return enemiesToSpawn;
	}
	
	public List<string> getSelectedUnits() {
		List<string> noEmptySelectedUnits = new List<string>();
		for (int i = 0; i < selectedUnits.Count; i++) {
			if(!selectedUnits[i].Equals("empty")) {
				noEmptySelectedUnits.Add(selectedUnits[i]);
			}
		}
		return selectedUnits;
	}

	public void setSelectedUnit(string bac, int indx) {
		selectedUnits[indx] = bac;
	}

	public void removeSelectedUnit(string bac) {
		selectedUnits.Remove(bac);
	}

	/*
	public void setAllBacteriaStats(string key, int health, int dmg, int speed, int lvl) {
		currentUnitStats[key] = new int[] {health, dmg, speed, lvl};
	}
	
	//int[] {Health, Dmg, speed, level}
	public int getBacteriaHealth(string key) {
		unravelArray = currentUnitStats[key];
		return unravelArray[0];
	}

	public int getBacteriaDamage(string key) {
		unravelArray = currentUnitStats[key];
		return unravelArray[1];
	}

	public int getBacteriaSpeed(string key) {
		unravelArray = currentUnitStats[key];
		return unravelArray[2];
	}

	public int getBacteriaLevel(string key) {
		unravelArray = currentUnitStats[key];
		return unravelArray[3];
	}

	public bool isThisBacteriaMelee(string key) {
		unravelArray = currentUnitStats[key];
		if (unravelArray[4] == 1) {
			return true;
		} else {
			return false;
		}
	}

	public bool isThisBacteriaRanged(string key) {
		unravelArray = currentUnitStats[key];
		if (unravelArray[5] == 1) {
			return true;
		} else {
			return false;
		}
	}

	public int getBacteriaSpecialAttack(string key) {
		unravelArray = currentUnitStats[key];
		return unravelArray[6];
	}

	public Dictionary<string, int[]> getAllBacteriaStats() {
		return currentUnitStats;
	}
	*/
}
