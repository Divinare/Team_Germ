using UnityEngine;
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			Debug.Log("Unit died");
			Destroy(this.gameObject);
		}
		if (selected) {
			transform.FindChild("selectionCircle").gameObject.active = true;
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
		currentHealth -= damage;
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
}