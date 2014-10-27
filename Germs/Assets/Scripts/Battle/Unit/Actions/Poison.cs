using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {
	private int poisonDuration;
	private int poisonDamage;

	private UnitStats unitStats;
	private BattleStatus battleStatus;
	private Selector selector;

	public GameObject poisonEffect;
	public ParticleSystem poisonInit;

	// Use this for initialization
	void Start () {
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
	}
	

	public void poisonAttack(GameObject initiator, GameObject target) {
		GameObject.FindGameObjectWithTag ("Drawer").GetComponent<BattleMenuBar> ().addToBattleLog (
			initiator.GetComponent<UnitStatus>().getUnitName() + " uses poison!"
		);
		Destroy(Instantiate(poisonInit, initiator.transform.position, initiator.transform.rotation), 2f); // instantiating the poison effect and destroying it after 2f time
		getPoisonStats(initiator);
		applyPoison(target);
		initiator.GetComponent<UnitStatus>().setActionCooldown(3);
		initiator.GetComponent<UnitStatus>().Deselect();
		selector.SetTargetedUnitToNull();
		selector.unlockInput (); // unlock input before ending turn
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
