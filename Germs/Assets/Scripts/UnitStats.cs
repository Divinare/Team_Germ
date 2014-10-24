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
	public Dictionary<string, string> unitDescriptions = new Dictionary<string, string>();
	private string[] unravelArray = new string[4];
	private List<string> enemiesToSpawn = new List<string>();
	private int[] unravelStatsArray = new int[7];
	private int[] enemyStatsArray = new int[7];

	public int levelsCompleted;
	public int baseStatIncreaseFactor;

	//imagedict
	public Texture2D gatbac;
	public Texture2D smallRed;
	public Texture2D phage;
	public Texture2D strepto;
	public Texture2D smallBlue;
	public Texture2D smallPurple;
	public Texture2D empty;
	public Dictionary<string, Texture2D> allUnitImages = new Dictionary<string, Texture2D>();

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
		unitSpecialAttacks.Add(2, new string[] {"Stunball", "rangedStun", "0", "2", "Bacteria launches a powerful ball that stuns the target."});
		unitSpecialAttacks.Add(3, new string[] {"Poison Splash", "poison", "30", "2", "Bacteria summons a splash of poison that infects the target dealing damage over time."});
		unitSpecialAttacks.Add(4, new string[] {"Healing Mist", "heal", "50", "0", "Bacteria brings forth the powers of healing to aid a wounded ally."});

		//set image dict for all units
		allUnitImages.Add ("Phage", phage);
		allUnitImages.Add ("Gatbac", gatbac);
		allUnitImages.Add ("Strepto", strepto);
		allUnitImages.Add ("Haemophilus", smallRed);
		allUnitImages.Add ("Salmonella", smallBlue);
		allUnitImages.Add ("Bacillus", smallPurple);
		allUnitImages.Add ("empty", empty);

		//set stories for all units
		unitDescriptions.Add ("Gatbac", "Gatbac is a very fat Epstein-Barr virus, that causes mononucleosis, also known as the kissing disease.");
		unitDescriptions.Add ("Phage", "A Bacteriophage is a virus that infects and replicates within a bacterium. Bacteriophages are composed of proteins that encapsulate a DNA or RNA genome.");
		unitDescriptions.Add ("Strepto", "Streptococcus pneumoniae, or pneumococcus, is a significant human pathogenic bacterium and is the cause of pneumonia.");
		unitDescriptions.Add ("Haemophilus", "...");
		unitDescriptions.Add ("Salmonella", "...");
		unitDescriptions.Add ("Bacillus", "...");
	}

	public int[] getEnemyUnitStats(string enemyName) {
		levelsCompleted = gameStatus.getCompletedLevels();
		if (levelsCompleted == 0) {
			levelsCompleted = 1;
		}

		//this is the factor with which all the enemy stats are multiplied, needs to be balanced.
		baseStatIncreaseFactor = levelsCompleted;

		enemyWithStats.Clear();
		unravelStatsArray = baseUnitStats[enemyName];
		enemyStatsArray = new int[] {unravelStatsArray[0]*baseStatIncreaseFactor,unravelStatsArray[1]*baseStatIncreaseFactor, unravelStatsArray[2]*baseStatIncreaseFactor, unravelStatsArray[3], unravelStatsArray[4], unravelStatsArray[5], unravelStatsArray[6]};

		return enemyStatsArray;
	}

	public Dictionary<string, string> getUnitDescriptions() {
		return unitDescriptions;
	}

	public Dictionary<string, Texture2D> getImageDict() {
		return allUnitImages;
	}

	public string getSpecialAttackName(int key) {
		unravelArray = unitSpecialAttacks[key];
		return unravelArray[0];
	}

	public string getSpecialAttackType(int key) {
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

	public string getSpecialAttackTooltip(int key) {
		unravelArray = unitSpecialAttacks[key];
		return unravelArray[4];
	}
}
