﻿using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {
	private int poisonDuration;
	private int poisonDamage;

	private UnitStats unitStats;
	private BattleStatus battleStatus;
	private Selector selector;

	public GameObject poisonEffect;
	public ParticleSystem poisonInit;
	public AudioClip poisoningSound;
	public AudioClip gettingPoisonedSound;

	// Use this for initialization
	void Start () {
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
	}
	

	public void poisonAttack(GameObject initiator, GameObject target) {

		StartCoroutine (DelayedPoisonedEffect(initiator, target));
	}

	IEnumerator DelayedPoisonedEffect(GameObject initiator, GameObject target) {

		AudioSource.PlayClipAtPoint(poisoningSound, transform.position);
		float x = initiator.transform.position.x;
		float y = initiator.transform.position.y;
		float z = initiator.transform.position.z;
		z = -3f;
		Instantiate(poisonInit, new Vector3(x,y,z), initiator.transform.rotation); // instantiating the poison effect, is destroyed by DestroyObject script attached to prefab
		yield return new WaitForSeconds (2f);
		getPoisonStats(initiator);
		AudioSource.PlayClipAtPoint(gettingPoisonedSound, transform.position);
		applyPoison(target);
		yield return new WaitForSeconds (1);
		initiator.GetComponent<UnitStatus>().setActionCooldown(3);
		initiator.GetComponent<UnitStatus>().Deselect();
		selector.SetTargetedUnitToNull();
		selector.unlockInput (); // unlock input before ending turn
		yield break;
	}

	void getPoisonStats(GameObject initiator) {
		//int specialAttack = battleStatus.getBacteriaSpecialAttack(initiator.GetComponent<UnitStatus>().getUnitName());
		/*
		poisonDamage = unitStats.getSpecialAttackDamage(specialAttack);
		poisonDuration = unitStats.getSpecialAttackRounds(specialAttack);
		*/
		poisonDamage = initiator.GetComponent<UnitStatus>().getDmg () * 2;
		poisonDuration = 2;
	}

	void applyPoison(GameObject target) {
		target.GetComponent<UnitStatus>().Poisoned(poisonDamage, poisonDuration);
		//poison effect
		float x = target.transform.position.x;
		float y = target.transform.position.y;
		float z = target.transform.position.z;
		z = -3f;
		Destroy(Instantiate(poisonEffect, new Vector3(x,y,z), Quaternion.Euler(new Vector3(270,90,0))), 3f);
	}
}
