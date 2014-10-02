﻿using UnityEngine;
using System.Collections;

public class UnitStatus : MonoBehaviour {
	
	public int currentHealth = 100;
	public int damage = 10;
	public int speed = 10;
	public int maxHealth = 100;
	public int size = 1;
// This is the attack that we have selected from the attack toolbar
	public string selectedAction = "melee";
	public bool selected = false;
	public bool enemy = false;
	public AudioClip[] sounds;
	public GameObject DeathSound;
	public int x;
	public int y;

	// Use this for initialization
	void Start () {


	}

	// Sounds array contains the following sounds for each clipId: 0 = sound of being hit;
	void PlaySound(int clipId) {
		audio.clip = sounds [clipId];
		audio.Play ();
	}

	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			GameObject deathSound = Instantiate (DeathSound, this.transform.position, this.transform.rotation) as GameObject;
			Debug.Log("Unit died");
			Destroy(this.gameObject);
		}
		if (selected) {
			transform.FindChild("selectionCircle").gameObject.active = true;
		}
		else {
			transform.FindChild("selectionCircle").gameObject.active = false;
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
		battlelog ("taken " + damage + " dmg");
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
	}
	
	public void Deselect() {
		selected = false;
	}

	public bool isAtTargetSquare(GameObject Square) {
		return false;
	}

	private void battlelog(string txt) {
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Battlelog> ().addToBattleLog(txt);
	}
}