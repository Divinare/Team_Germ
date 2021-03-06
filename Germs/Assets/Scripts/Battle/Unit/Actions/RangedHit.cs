﻿using UnityEngine;
using System.Collections;

public class RangedHit : MonoBehaviour {

	private int attackerGivesDamage;
	private GameObject targetedUnit;
	private UnitStatus attackingUnit;
	private Selector selector;
	public ParticleSystem slimeballHit;

	void Start() {
		attackingUnit = GameObject.FindGameObjectWithTag ("TurnHandler").transform.GetComponent<TurnHandler> ().getActiveUnit().GetComponent<UnitStatus>();
		attackerGivesDamage = attackingUnit.getDmg ();
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
		targetedUnit = selector.GetTargetedUnit();
	}

	void OnTriggerEnter(Collider unit) {		               

		// Hitting the enemy
		if (unit.GetComponent<UnitStatus>() == targetedUnit.GetComponent<UnitStatus>()) {

			Destroy(Instantiate(slimeballHit, transform.position, transform.rotation), 2f); // instantiating the explosion and destroying it after 2f time
			unit.GetComponent<UnitStatus>().TakeDamage(attackerGivesDamage);
			selector.SetTargetedUnitToNull();
			GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector>().unlockInput (); // unlock input before ending turn
			attackingUnit.Deselect(); // deselecting the attacker here, so the bullet has time to hit its target before turn is given to another
			Destroy (gameObject);

		}
	}
}
