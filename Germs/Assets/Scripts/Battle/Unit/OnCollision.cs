using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour {

	private int attackerGivesDamage;

	void Start() {
		// If this value is assigned as damage the projectile isn't hitting the attacker (as it should) before it hits the target 
		attackerGivesDamage = 0;
	}

	void OnTriggerEnter(Collider unit) {

		// Hitting the enemy (or anything in the bullet's path)
		if (unit.GetComponent<UnitStatus> ().selected == false) {
			Destroy (gameObject);
			unit.GetComponent<UnitStatus>().TakeDamage(attackerGivesDamage);
		} 
		// Hitting the attacker, as the bullet is spawned inside the attacker's collider
		else {
			attackerGivesDamage = unit.GetComponent<UnitStatus>().damage;
		}
	}
}
