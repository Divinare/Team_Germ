using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStatus : MonoBehaviour {
	private Transform battleStatus;
	public Dictionary <string, int[]> allBacteriaStats = new Dictionary<string, int[]>();
	public int[] unitStats = new int[4];

	public int currentHealth;
	public int damage;
	public int speed;
	public int maxHealth;
	public int heal;

// This is the attack that we have selected from the attack toolbar
	public string selectedAction = "melee";
	public bool selected = false;
	public bool enemy = false;
	public AudioClip[] sounds;
	public GameObject DeathSound;
	public string unitName;
	public int x;
	public int y;
	private GameObject currentSquare; // the square currently occupied by the unit

	// Use this for initialization
	void Start () {
		/*
		allBacteriaStats = battleStatus.gameObject.GetComponent<BattleStatus>().getAllBacteriaStats();
		unitStats = allBacteriaStats[unitName];

		//test (Health, Dmg, speed, level)
		currentHealth = unitStats[0];
		maxHealth = unitStats[0];
		damage = unitStats[1];
		speed = unitStats[2];
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