using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStatus : MonoBehaviour {
	private BattleStatus battleStatus;
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

	//for statusEffects
	public int unitRounds;
	private int stunRounds;
	public bool stunned;
	public bool poisoned;

	//animations
	private Animator animator;

	// Use this for initialization
	void Start () {
		//this gets stats from BattleStatus
		if (!enemy) {
			battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
			currentHealth = battleStatus.getBacteriaHealth(unitName);
			maxHealth = battleStatus.getBacteriaHealth(unitName);
			damage = battleStatus.getBacteriaDamage(unitName);
			speed = battleStatus.getBacteriaSpeed(unitName);
		}
		animator = this.GetComponent<Animator>();
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
		currentHealth -= damage;
		battlelog (gameObject.name + " took " + damage + " damage!");
		if (currentHealth <= 0) {
			animator.SetTrigger ("dead");
			GameObject deathSound = Instantiate (DeathSound, this.transform.position, this.transform.rotation) as GameObject;
			Debug.Log ("Unit died");
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
	}

	public void Stunned(int rounds) {
		stunned = true;
		animator.SetBool("stunned", true);
		stunRounds = rounds;
	}

	public void countDownStun() {
		stunRounds -= 1;
		Debug.Log (unitName + " STUNNED FOR "+stunRounds);
		if (stunRounds == 0) {
			stunned = false;
			animator.SetBool("stunned", false);
			if (enemy) {
				//Debug.Log ("Deselect?");
				//Deselect();
			}
		}
	}

	public bool IsUnitStunned() {
		return stunned;
	}

	public void switchSelectedAction(string clickedMenuItem) {
		selectedAction = clickedMenuItem;
	}
	
	public void Select() {
		unitRounds += 1;
		selected = true;
		GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnStartHandler> ().handeTurnStart ();
	}
	
	public void Deselect() {
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

	public void setHp(int hp) {
		currentHealth = hp;
		maxHealth = hp;
	}

	public void setDmg(int dmg) {
		damage = dmg;
	}

	public void setSpeed(int spd) {
		speed = spd;
	}
}