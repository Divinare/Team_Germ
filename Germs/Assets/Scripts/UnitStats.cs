using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStats : MonoBehaviour {
	public static UnitStats unitStats;
	private BattleStatus battleStatus;
	private GameStatus gameStatus;

	private Dictionary<string, int[]> baseUnitStats = new Dictionary<string, int[]>();

	public Dictionary<string, int[]> enemiesForLevel = new Dictionary<string, int[]>();
	public Dictionary<int, string[]> unitSpecialAttacks = new Dictionary<int, string[]>();
	private string[] unravelArray = new string[4];
	private string[] enemiesInLvl = new string[5];
	private int[] unravelStatsArray = new int[7];
	public int levelsCompleted;
	public int baseStatIncreaseFactor;


	// Use this for initialization
	void Start () {
		if (unitStats == null) {
			DontDestroyOnLoad (gameObject);
			unitStats = this;
		} else if (unitStats != this) {
			Destroy (gameObject);
		}

		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();
		baseUnitStats = battleStatus.getAllBacteriaStats();

		//example unitSpecialAttack entry : [0]=SkillName, [1]=SkillDescription(for tooltips/similar), [2] damage return 0 if no dmg, [3]  duration(rounds) 0 instant
		unitSpecialAttacks.Add(2, new string[] {"Stun", "Stun incapacitates the target for one round.", "0", "1"});
		unitSpecialAttacks.Add(3, new string[] {"Poison", "Poison deals 50 damage over 2 rounds.", "50", "2"});
	}

	public void storeEnemyUnitStats(string levelName) {
		levelsCompleted = gameStatus.getCompletedLevels();
		enemiesInLvl = battleStatus.getEnemiesToLoad(levelName);

		if (levelsCompleted == 0) {
			levelsCompleted = 1;
		}

		baseStatIncreaseFactor = (3/2)*levelsCompleted;

		enemiesForLevel.Clear();

		foreach (string unitName in enemiesInLvl) {
			unravelStatsArray = baseUnitStats[unitName];
			enemiesForLevel[unitName] = new int[] {unravelStatsArray[0]*baseStatIncreaseFactor,unravelStatsArray[1]*baseStatIncreaseFactor, unravelStatsArray[2]*baseStatIncreaseFactor, unravelStatsArray[3], unravelStatsArray[4], unravelStatsArray[5], unravelStatsArray[6]};
		}
	}

	public Dictionary<string, int[]> getEnemyUnitStats() {
		return enemiesForLevel;
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
