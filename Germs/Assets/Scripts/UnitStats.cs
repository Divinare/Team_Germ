using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStats : MonoBehaviour {
	public static UnitStats unitStats;

	public BattleStatus battleStatus;

	public Dictionary<string, int[]> baseUnitStats = new Dictionary<string, int[]>();
	public Dictionary<int, string[]> unitSpecialAttacks = new Dictionary<int, string[]>();
	private string[] unravelArray = new string[4];


	// Use this for initialization
	void Start () {
		if (unitStats == null) {
			DontDestroyOnLoad (gameObject);
			unitStats = this;
		} else if (unitStats != this) {
			Destroy (gameObject);
		}

		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		baseUnitStats = battleStatus.getAllBacteriaStats();

		//example unitSpecialAttack entry : [0]=SkillName, [1]=SkillDescription(for tooltips/similar), [2] damage return 0 if no dmg, [3]  duration(rounds) 0 instant
		unitSpecialAttacks.Add(2, new string[] {"Stun", "Stun incapacitates the target for one round.", "0", "1"});
		unitSpecialAttacks.Add(3, new string[] {"Poison", "Poison deals 50 damage over 2 rounds.", "50", "2"});
	}

	public Dictionary<string, int[]> getBaseUnitStats() {
		//modifier here?
		return baseUnitStats;
	}

	public string getSpecialAttackName(int key) {
		unravelArray = unitSpecialAttacks[key];
		return unravelArray[0];
	}

	public string getSpecialAttackDescription(int key) {
		unravelArray = unitSpecialAttacks[key];
		return unravelArray[1];
	}

	public int getSpecialAttackDamage(int key) {
		unravelArray = unitSpecialAttacks[key];
		return int.Parse(unravelArray[2]);
	}

	public int getSpecialAttackRounds(int key) {
		unravelArray = unitSpecialAttacks[key];
		return int.Parse(unravelArray[3]);
	}
}
