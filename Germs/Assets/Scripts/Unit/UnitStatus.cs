using UnityEngine;
using System.Collections;

public class UnitStatus : MonoBehaviour {
	
	public int currentHealth = 100;
	public int damage = 10;
	public int speed = 10;
	public int maxHealth = 100;
	public int size = 1;
	public bool selected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			Debug.Log("Unit died");
			Destroy(this.gameObject);
		}
		
	}

	void Heal(int heal) {
		currentHealth += heal;
	}
	
	void TakeDamage(int damage) {
		currentHealth -= damage;
	}
	
	void Poisoned(int damage) {
		transform.FindChild("poisonBubbles").gameObject.SetActive(true);
		//poisoned units taka damage over time, set amount of rounds
	}


	void Select() {
		selected = true;
	}
	
	void Deselect() {
		selected = false;
	}
}