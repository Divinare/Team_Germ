﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStatus : MonoBehaviour {
	private UnitStats unitStats;
	public Dictionary <string, int[]> allBacteriaStats = new Dictionary<string, int[]>();

	private int currentHealth; // current HP
	private int damage; // how much damage this unit does
	private int speed; // how far this unit can move
	private int maxHealth; // max HP

// This is the attack that we have selected from the attack toolbar
	public int actionCooldown;
	public string selectedAction = "melee";
	public bool selected = false;
	public bool enemy = false; // true = unit is on computer's side, false = unit is on player's side
	public AudioClip[] sounds;
	public GameObject DeathSound;
	public string unitName;
	private GameObject currentSquare; // the square currently occupied by the unit

	//for statusEffects
	private int stunRounds;
	private int poisonRounds;
	private int poisonDmg;
	public bool stunned;
	public bool poisoned;

	//for cooldowns
	public int unitRounds;

	//animations
	private Animator animator;

	//action bar actions
	public bool hasRanged;
	public bool hasMelee;
	public bool hasRangedStun;
	public bool hasPoison;
	public bool hasHeal;
	public bool hasDetox;
	private Dictionary<string, bool> unitActions; // used by battle action bar


	void Start () {
		actionCooldown = 0;
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();
		//this gets stats from BattleStatus
		if (!enemy) {
			currentHealth = unitStats.getUnitHealth(unitName);
			maxHealth = unitStats.getUnitHealth(unitName);
			damage = unitStats.getUnitDamage(unitName);
			speed = unitStats.getUnitSpeed(unitName);
		}
		animator = this.GetComponent<Animator>();

		// these are for Battle Action Bar
		unitActions = new Dictionary<string, bool>();
		listUnitActions ();
	}
	
	public void setActionCooldown(int cooldown) {
		this.actionCooldown = cooldown;
		if (!hasRanged) {
			selectedAction = "melee";
		} else {
			selectedAction = "ranged";
		}
	}
	
	public void countDownAbilityCooldown() {
		if (actionCooldown > 0) {
			actionCooldown--;
		}
	}


	private void listUnitActions() {
		// These values are used by Battle Action Bar - if the key's value is true, the unit is capable of preforming said action
		unitActions.Add ("ranged", hasRanged);
		unitActions.Add ("melee", hasMelee);
		unitActions.Add ("rangedStun", hasRangedStun);
		unitActions.Add ("poison", hasPoison);
		unitActions.Add ("heal", hasHeal);
		unitActions.Add ("detox", hasDetox);
		unitActions.Add ("skipTurn", true);
	}

	// returns a list of actions the unit is allowed to perform
	public Dictionary<string, bool> GetUnitActions() {
		return unitActions;
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


	public void Heal(int heal) {
		//cannot get more hp than maxhp
		if (currentHealth + heal < maxHealth) {
			currentHealth += heal;
		} else {
			currentHealth = maxHealth;
		}
		battlelog (unitName + " has been healed for " + heal + " HP!");
	}

	public void GiveSpeed(int speedPercentage) {
		int oldSpeed = speed;
		speed = speed * (speedPercentage/100);
		if (oldSpeed == speed) {
			speed++; // increasin at least with 1
		}
		battlelog (unitName + "'s speed has increased by " + speed + " units!");
	}

	public void GiveDamage(int damage) {
		this.damage += damage;
		battlelog (unitName + " has been affected by mind-blowing rage, increasing its damage by " + damage + " extra points!");
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
		battlelog (this.getUnitName() + " takes " + damage + " damage!");
		if (currentHealth <= 0) {
			animator.SetTrigger ("dead");
			GameObject deathSound = Instantiate (DeathSound, this.transform.position, this.transform.rotation) as GameObject;
			battlelog (this.getUnitName() + "'s nucleoid explodes into a pile of slimy mush!");
			currentSquare.GetComponent<SquareStatus>().setStatus ("movable",null);
			Destroy (this.gameObject);
			return;
		} else {
			PlaySound (0); // 0 = 'damage taken'-sound
			animator.SetTrigger ("takeDamage");
		}
	}
	
	public void Poisoned(int damage, int rounds) {
		//poisoned units taka damage over time for x rounds
		//Debug.Log (unitName + " poisoned for "+rounds+" rounds and "+damage+" dmg!");
		battlelog (unitName + " has become poisoned for " + rounds + " rounds.");
		poisoned = true;
		animator.SetBool("poisoned", true);
		poisonRounds = rounds;
		poisonDmg = damage / poisonRounds;
	}

	public void countDownPoison() {
		poisonRounds -= 1;
		if (poisonRounds == 0) {
			removePoison();
		}
		Debug.Log (unitName + " takes " + poisonDmg + " damage from poison.");
		TakeDamage(poisonDmg);		
		
	}

	public void Stunned(int rounds) {
		stunned = true;
		animator.SetBool("stunned", true);
		stunRounds = rounds;
		battlelog (unitName + " has become stunned for " + rounds + " rounds.");
	}

	public void countDownStun() {
		stunRounds -= 1;
		Debug.Log (unitName + " STUNNED FOR "+stunRounds);
		if (stunRounds == 0) {
			removeStun ();
		/*
		if (enemy) {
			Deselect();
		}
		*/
		}
	}

	public void removeStun() {
		stunned = false;
		animator.SetBool("stunned", false);
	}

	public void removePoison() {
		poisoned = false;
		animator.SetBool ("poisoned", false);
	}

	public bool IsUnitStunned() {
		return stunned;
	}

	public bool IsUnitPoisoned() {
		return poisoned;
	}

	public void switchSelectedAction(string clickedMenuItem) {
		selectedAction = clickedMenuItem;
	}
	
	public void Select() {
		unitRounds += 1;
		selected = true;
		GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnStartHandler> ().handeTurnStart (this.gameObject);
	}
	
	public void Deselect() {
		if (!hasRanged) {
			selectedAction = "melee";
		}
		selected = false;
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().resetHostileTurn (); // this is used to inform AI that in case a hostile unit was active, its turn has ended
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
		GameObject.FindGameObjectWithTag ("Drawer").GetComponent<BattleMenuBar> ().addToBattleLog (txt);
	}
	
	public int getMaxHp() {
		return this.maxHealth;
	}

	public void setHp(int hp) {
		currentHealth = hp;
		maxHealth = hp;
	}
	
	public int getHp() {
		return this.currentHealth;
	}

	public void setDmg(int dmg) {
		damage = dmg;
	}
	
	public int getDmg() {
		return this.damage;
	}

	public void setSpeed(int spd) {
		speed = spd;
	}
	
	public int getSpeed() {
		return this.speed;
	}

	public string getUnitName() {
		return unitName;
	}
}