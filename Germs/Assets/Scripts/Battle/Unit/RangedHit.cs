using UnityEngine;
using System.Collections;

public class RangedHit : MonoBehaviour {

	private int attackerGivesDamage;
	private GameObject targetedUnit;
	private Selector selector;
	public ParticleSystem slimeballHit;

	void Start() {
		// If this value is assigned as damage the projectile isn't hitting the attacker (as it should) before it hits the target 
		attackerGivesDamage = 0;
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
		targetedUnit = selector.GetTargetedUnit();
	}

	void OnTriggerEnter(Collider unit) {		               

		// Hitting the enemy
		if (unit.GetComponent<UnitStatus>() == targetedUnit.GetComponent<UnitStatus>()) {

			ParticleSystem explosionEffect = Instantiate(slimeballHit, transform.position, transform.rotation) as ParticleSystem; // instantiating explosion effect
			unit.GetComponent<UnitStatus>().TakeDamage(attackerGivesDamage);
			selector.SetTargetedUnitToNull();
			Destroy (gameObject);

		}
		// Hitting the attacker, as the bullet is spawned inside the attacker's collider
		else if (unit.GetComponent<UnitStatus>().IsSelected()) {		
			attackerGivesDamage = unit.GetComponent<UnitStatus>().damage;
		}
	}
}
