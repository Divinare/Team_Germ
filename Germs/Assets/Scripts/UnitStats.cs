using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStats : MonoBehaviour {
	public static UnitStats unitStats;

	private BattleStatus battleStatus;
	private GameStatus gameStatus;

	private Dictionary<string, int[]> baseUnitStats = new Dictionary<string, int[]>();
	private Dictionary<string, int[]> playerUnitStats = new Dictionary<string, int[]>();
	public Dictionary<string, int[]> enemyWithStats = new Dictionary<string, int[]>();
	private Dictionary<int, string[]> unitSpecialAttacks = new Dictionary<int, string[]>();
	private Dictionary<string, string> unitDescriptions = new Dictionary<string, string>();
	private Dictionary<string, int[]> unitUnlock = new Dictionary<string, int[]>();
	private int[] unlockArray = new int[2];
	private string[] unravelArray = new string[4];
	private List<string> enemiesToSpawn = new List<string>();
	private int[] unravelStatArray = new int[7];
	private int[] enemyStatArray = new int[7];

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
	private Dictionary<string, Texture2D> allUnitImages = new Dictionary<string, Texture2D>();

	//levelup
	public int lvlUpHpIncrease = 4;
	//speed is set in a method, everything else incraeses 25%
	public int lvlUpDmgIncrease = 4;
	public int lvlUpCost = 25;

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

		//test int[] {Health, Dmg, speed, level, melee, ranged, special}
		baseUnitStats.Add ("Gatbac", new int[] {150, 20, 2, 1, 1, 1, 3});
		baseUnitStats.Add ("Strepto", new int[] {70, 10, 6, 1, 1, 0, 3});
		baseUnitStats.Add ("Haemophilus", new int[] {80, 15, 9, 1, 1, 0, 3});
		baseUnitStats.Add ("Salmonella", new int[] {100, 13, 6, 1, 1, 0, 3});
		baseUnitStats.Add ("Bacillus", new int[] {90, 15, 6, 1, 0, 1, 3});
		baseUnitStats.Add ("Phage", new int[] {40, 14, 4, 1, 1, 0, 3});

		//example unitSpecialAttack entry : [0]=SkillName, [1]=SkillType, [2] = total dmg, [3]=duration, [4]=skillDescription (for tooltip)}
		unitSpecialAttacks.Add(2, new string[] {"Stunball", "rangedStun", "0", "2", "Bacteria launches a powerful ball that stuns the target."});
		unitSpecialAttacks.Add(3, new string[] {"Poison Splash", "poison", "30", "2", "Bacteria summons a splash of poison that infects the target dealing damage over time."});
		unitSpecialAttacks.Add(4, new string[] {"Healing Mist", "heal", "50", "0", "Bacteria brings forth the powers of healing to aid a wounded ally."});

		//set image dict for all units
		allUnitImages.Add ("Strepto", strepto);
		allUnitImages.Add ("Salmonella", smallBlue);
		allUnitImages.Add ("Gatbac", gatbac);
		allUnitImages.Add ("Haemophilus", smallRed);
		allUnitImages.Add ("Bacillus", smallPurple);
		allUnitImages.Add ("Phage", phage);
		allUnitImages.Add ("empty", empty);

		//set stories for all units
		unitDescriptions.Add ("Gatbac", "Gatbac is a very fat Epstein-Barr virus, that causes mononucleosis, also known as the kissing disease.");
		unitDescriptions.Add ("Phage", "A Bacteriophage is a virus that infects and replicates within a bacterium. Bacteriophages are composed of proteins that encapsulate a DNA or RNA genome.");
		unitDescriptions.Add ("Strepto", "Streptococcus pneumoniae, or pneumococcus, is a significant human pathogenic bacterium and is the cause of pneumonia.");
		unitDescriptions.Add ("Haemophilus", "...");
		unitDescriptions.Add ("Salmonella", "...");
		unitDescriptions.Add ("Bacillus", "...");

		//unlock bacteria in trainer stuff
		//name: lvlsCompleted, xpCost
		unitUnlock.Add ("Strepto", new int[] {0,0});
		unitUnlock.Add ("Salmonella", new int[] {0,0});
		unitUnlock.Add ("Gatbac", new int[] {1,50});
		unitUnlock.Add ("Haemophilus", new int[] {2,100});
		unitUnlock.Add ("Bacillus", new int[] {3,150});
		unitUnlock.Add ("Phage", new int[] {4,200});

		//initial bacteria?
		setPlayerUnit("Salmonella");
		setPlayerUnit("Strepto");
	}

	public int[] getEnemyUnitStats(string enemyName) {
		levelsCompleted = gameStatus.getCompletedLevels();
		if (levelsCompleted == 0) {
			levelsCompleted = 1;
		}

		//this is the factor with which all the enemy stats are multiplied, needs to be balanced.
		baseStatIncreaseFactor = levelsCompleted;
		// currently multiplies hp, dmg with levels completed and adds levels completed to speed (so 1 increase per level completed)

		enemyWithStats.Clear();
		unravelStatArray = baseUnitStats[enemyName];
		enemyStatArray = new int[] {unravelStatArray[0]*baseStatIncreaseFactor,unravelStatArray[1]*baseStatIncreaseFactor, unravelStatArray[2]+baseStatIncreaseFactor, unravelStatArray[3], unravelStatArray[4], unravelStatArray[5], unravelStatArray[6]};

		return enemyStatArray;
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

	//int[] {Health, Dmg, speed, level}
	public int getUnitHealth(string key) {
		unravelStatArray = playerUnitStats[key];
		return unravelStatArray[0];
	}
	
	public int getUnitDamage(string key) {
		unravelStatArray = playerUnitStats[key];
		return unravelStatArray[1];
	}
	
	public int getUnitSpeed(string key) {
		unravelStatArray = playerUnitStats[key];
		return unravelStatArray[2];
	}
	
	public int getUnitLevel(string key) {
		unravelStatArray = playerUnitStats[key];
		return unravelStatArray[3];
	}

	public Dictionary<string, int[]> getPlayerUnitStats() {
		return playerUnitStats;
	}

	public void setPlayerUnit(string unitName) {
		if (!playerUnitStats.ContainsKey(unitName)) {
			Debug.Log ("adding "+unitName+" into player units");
			playerUnitStats[unitName] = baseUnitStats[unitName];
		}
	}

	public Dictionary<string, int[]> getBaseUnitStats() {
		return baseUnitStats;
	}

	public void setPlayerUnitStats(string key, int health, int dmg, int speed, int lvl) {
		playerUnitStats[key] = new int[] {health, dmg, speed, lvl};
	}

	public int getLevelUpCost(string unitName) {
		int unitLevel = getUnitLevel(unitName);
		return lvlUpCost * unitLevel;
	}

	public int getUnitUnlockLvls(string unitName) {
		unlockArray = unitUnlock[unitName];
		return unlockArray[0];
	}

	public int getUnitUnlockXpCost(string unitName) {
		unlockArray = unitUnlock[unitName];
		return unlockArray[1];
	}

	public int calculateUnitSpeed(string unitName) {
		int unitSpeed = getUnitSpeed(unitName);
		if (unitSpeed < 4) {
			return 2;
		}
		else if (unitSpeed >= 4 && unitSpeed < 6) {
			return 4;
		}
		else if (unitSpeed >= 6 && unitSpeed < 10) {
			return 6;
		} else {
			return 10;
		}
	}
}
