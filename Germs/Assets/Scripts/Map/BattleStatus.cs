﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleStatus : MonoBehaviour {
	public static BattleStatus battleStatus;

	public List<GameObject> allUnits = new List<GameObject>();
	public List<string> selectedUnits = new List<string>();
	public Dictionary<string, int[]> currentUnitStats = new Dictionary<string, int[]>();
	public List<string> enemiesToSpawn = new List<string>();
	private int[] unravelArray = new int[7];
	private int spawnNum;

	// Use this for initialization
	void Start () {
		if (battleStatus == null) {
			DontDestroyOnLoad (gameObject);
			battleStatus = this;
		} else if (battleStatus != this) {
			Destroy (gameObject);
		}
		//testing poison everyone has poison (3) equipped!

		//test int[] {Health, Dmg, speed, level, melee, ranged, special}
		currentUnitStats.Add ("Gatbac", new int[] {200, 10, 5, 1, 1, 1, 3});
		currentUnitStats.Add ("Strepto", new int[] {100, 10, 8, 1, 1, 0, 3});
		currentUnitStats.Add ("smallRed", new int[] {100, 10, 6, 1, 1, 0, 3});
		currentUnitStats.Add ("smallBlue", new int[] {100, 10, 6, 1, 1, 0, 3});
		currentUnitStats.Add ("smallPurple", new int[] {100, 10, 6, 1, 0, 1, 3});
		currentUnitStats.Add ("Phage", new int[] {100, 10, 4, 1, 1, 0, 3});
		//currentUnitStats.Add ("blueBac", new int[] {100, 15, 10, 1, 1, 1, 8});

		//initial selection
		selectedUnits.Add ("Gatbac");
		selectedUnits.Add ("Strepto");
		selectedUnits.Add ("smallRed");
		selectedUnits.Add ("smallBlue");
		selectedUnits.Add ("Phage");
	}

	public void randomEnemiesForLevel() {
		enemiesToSpawn.Clear();
		spawnNum = 5;
		for (int i=0; i<spawnNum; i++) {
			var randomKey = currentUnitStats.Keys.ToArray()[(int)Random.Range(0,currentUnitStats.Keys.Count)];
			enemiesToSpawn.Add (randomKey);
		}
	}

	public List<string> getEnemiesToSpawn() {
		return enemiesToSpawn;
	}
	
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
}
