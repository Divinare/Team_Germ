using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {
	private int poisonDuration;
	private int poisonDamage;

	private UnitStats unitStats;
	private BattleStatus battleStatus;
	private Selector selector;

	// Use this for initialization
	void Start () {
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void poisonAttack(GameObject initiator, GameObject target) {
		getPoisonStats(initiator);
		applyPoison(target);
		initiator.GetComponent<UnitStatus>().Deselect();
	}

	void getPoisonStats(GameObject initiator) {
		int specialAttack = battleStatus.getBacteriaSpecialAttack(initiator.GetComponent<UnitStatus>().getUnitName());
		poisonDamage = unitStats.getSpecialAttackDamage(specialAttack);
		poisonDuration = unitStats.getSpecialAttackRounds(specialAttack);
	}

	void applyPoison(GameObject target) {
		target.GetComponent<UnitStatus>().Poisoned(poisonDamage, poisonDuration);
		selector.SetTargetedUnitToNull();
		selector.unlockInput (); // unlock input before ending turn
	}
}
