﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStatus : MonoBehaviour {
	private Transform battleStatus;
	public Dictionary <string, int[]> allBacteriaStats = new Dictionary<string, int[]>();
	public int[] unitStats = new int[4];

	public int currentHealth; // current HP
	public int damage; // how much damage this unit does
	public int speed; // how far this unit can move
	public int maxHealth; // max HP
	public int heal; // how many HP this unit heals

// This is the attack that we have selected from the attack toolbar
	public string selectedAction = "melee";
	public bool selected = false;
	public bool enemy = false; // true = unit is on computer's side, false = unit is on player's side
	public AudioClip[] sounds;
	public GameObject DeathSound;
	public string unitName;
	public int x;
	public int y;
	private GameObject currentSquare; // the square currently occupied by the unit

	// Use this for initialization
	void Start () {
		/*
		 * This gets stat values from battleStatus:
		 * 
		 * 
		battleStatus = GameObject.Find ("BattleTracker").transform;
		currentHealth = battleTracker.gameObject.GetComponent<BattleStatus>().getBacteriaHealth(selectedBacteria);
		maxHealth = battleTracker.gameObject.GetComponent<BattleStatus>().getBacteriaHealth(selectedBacteria);
		damage = battleTracker.gameObject.GetComponent<BattleStatus>().getBacteriaDamage(selectedBacteria);
		speed = battleTracker.gameObject.GetComponent<BattleStatus>().getBacteriaSpeed(selectedBacteria);
		*/
	}

	// Sounds array contains the following sounds for each clipId: 0 = sound of being hit;
	void PlaySound(int clipId) {
		audio.clip = sounds [clipId];
		audio.Play ();
	}

	public void setSquare(GameObject square) {
		this.currentSquare = square;
	}

	public GameObject getSquare() {
		return currentSquare;
	
	}

	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			GameObject deathSound = Instantiate (DeathSound, this.transform.position, this.transform.rotation) as GameObject;
			Debug.Log("Unit died");
			Destroy(this.gameObject);
		}
	}

	public void Heal(int heal) {
		//cannot get more hp than maxhp
		if (currentHealth + heal < maxHealth) {
			currentHealth += heal;
		} else {
			currentHealth = maxHealth;
		}
	}

	public void TakeDamage(int damage) {
		PlaySound (0); // 0 = 'damage taken'-sound
		currentHealth -= damage;
		battlelog (gameObject.name + " took " + damage + " damage!");
		//animTakeDamage.wrapMode = WrapMode.Once;
	}
	
	public void Poisoned(int damage) {
		transform.FindChild("poisonBubbles").gameObject.SetActive(true);
		//poisoned units taka damage over time, set amount of rounds
	}

	public void switchSelectedAction(string clickedMenuItem) {
		selectedAction = clickedMenuItem;
	}
	
	public void Select() {
		selected = true;
		GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnStartHandler> ().handeTurnStart ();
	}
	
	public void Deselect() {
		selected = false;
	}

	public bool IsSelected() {
		return selected;
	}

	public bool IsEnemy() {
		return enemy;
	}

	public void SetAsEnemy() {
		enemy = true;
	}

	public bool isAtTargetSquare(GameObject Square) {
		return false;
	}

	private void battlelog(string txt) {
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Battlelog> ().addToBattleLog(txt);
	}
}