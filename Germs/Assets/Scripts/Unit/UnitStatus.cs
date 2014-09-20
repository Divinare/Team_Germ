using UnityEngine;
using System.Collections;

public class UnitStatus : MonoBehaviour {
	
	public float currentHealth = 100;
	public float damage = 10;
	public float speed = 10;
	
	private float maxHealth = 100;
	
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

	void Heal(float heal) {
		currentHealth += heal;
	}
	
	void TakeDamage(float damage) {
		currentHealth -= damage;
	}
	
	void Poisoned(float damage) {
		transform.FindChild("poisonBubbles").gameObject.SetActive(true);
		//poisoned units taka damage over time, set amount of rounds
	}
}