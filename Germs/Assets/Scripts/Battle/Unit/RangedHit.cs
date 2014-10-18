using UnityEngine;
using System.Collections;

public class RangedHit : MonoBehaviour {

	private int attackerGivesDamage;
	private GameObject targetedUnit;
	private UnitStatus attackingUnit;
	private Selector selector;
	public ParticleSystem slimeballHit;

	void Start() {
		// If this value is assigned as damage the projectile isn't hitting the attacker (as it should) before it hits the target 
		attackerGivesDamage = 0;
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
		targetedUnit = selector.GetTargetedUnit();
	}

	void Update() {
	
	}

	void OnTriggerEnter(Collider unit) {		               

		// Hitting the enemy
		if (unit.GetComponent<UnitStatus>() == targetedUnit.GetComponent<UnitStatus>()) {

			Destroy(Instantiate(slimeballHit, transform.position, transform.rotation), 2f); // instantiating the explosion and destroying it after 2f time
			unit.GetComponent<UnitStatus>().TakeDamage(attackerGivesDamage);
			selector.SetTargetedUnitToNull();
			GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector>().unlockInput (); // unlock input before ending turn
			attackingUnit.Deselect(); // deselecting the attacker here, so the bullet has time to hit its target before turn is given to another
			GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().resetHostileTurn (); // in case attack was initiated by AI, resets AI turn status so next unit gets turn correctly
			Destroy (gameObject);


		}
		// Hitting the attacker, as the bullet is spawned inside the attacker's collider
		else if (unit.GetComponent<UnitStatus>().IsSelected()) {	
			attackingUnit = unit.GetComponent<UnitStatus>();
			attackerGivesDamage = attackingUnit.damage;
		}
	}
}
