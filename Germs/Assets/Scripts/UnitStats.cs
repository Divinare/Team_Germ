using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStats : MonoBehaviour {
	public static UnitStats unitStats;
	private BattleStatus battleStatus;
	private GameStatus gameStatus;

	private Dictionary<string, int[]> baseUnitStats = new Dictionary<string, int[]>();

	public Dictionary<string, int[]> enemyWithStats = new Dictionary<string, int[]>();
	public Dictionary<int, string[]> unitSpecialAttacks = new Dictionary<int, string[]>();
	private string[] unravelArray = new string[4];
	private List<string> enemiesToSpawn = new List<string>();
	private int[] unravelStatsArray = new int[7];
	private int[] enemyStatsArray = new int[7];
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

		//example unitSpecialAttack entry : [0]=SkillName, [1]=SkillType, [2] = total dmg, [3]=duration, [4]=skillDescription (for tooltip)}
		unitSpecialAttacks.Add(2, new string[] {"Stunball", "rangedStun", "0", "2", "Bacteria launches a powerful ball that stuns the target for 2 sec."});
		unitSpecialAttacks.Add(3, new string[] {"Poison", "poison", "30", "2", "Bacteria makes a powerful swing that stuns the target for 2sec."});
	}

	public int[] getEnemyUnitStats(string enemyName) {
		levelsCompleted = gameStatus.getCompletedLevels();

		if (levelsCompleted == 0) {
			levelsCompleted = 1;
		}

		baseStatIncreaseFactor = levelsCompleted;
		enemyWithStats.Clear();

		unravelStatsArray = baseUnitStats[enemyName];
		enemyStatsArray = new int[] {unravelStatsArray[0]*baseStatIncreaseFactor,unravelStatsArray[1]*baseStatIncreaseFactor, unravelStatsArray[2]*baseStatIncreaseFactor, unravelStatsArray[3], unravelStatsArray[4], unravelStatsArray[5], unravelStatsArray[6]};

		return enemyStatsArray;
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
