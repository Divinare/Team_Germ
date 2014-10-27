using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TrainerGUI : MonoBehaviour {

	//GUI Stuff
	public GUIStyle trainerBox;
	public GUIStyle trainerText;
	public GUIStyle yellowText;
	public GUIStyle blueText;
	public GUIStyle orangeText;
	public GUIStyle lvlUpButton;
	public GUIStyle deactiveLvlUpButton;
	public GUIStyle unlockButton;
	public GUIStyle deactiveUnlockButton;

	private bool levelMenu;
	private bool skillMenu;
	
	private Vector2 windowSize = new Vector2 (Screen.width/2, (Screen.height - 2*Screen.height/10));
	private Vector2 buttonSize = new Vector2 (Screen.width/16, Screen.width/16);
	//private Vector2 imgSize = new Vector2 (Screen.width/6, Screen.width/6);
	
	//Knowledge Stuff
	private BattleStatus battleStatus;
	private GameStatus gameStatus;
	private UnitStats unitStats;
	
	public float gold;
	public float xp;
	private int bacHealth;
	private int bacDmg;
	private int bacSpeed;
	private int bacLevel;
	private int lvlUpFactor;
	private int lvlUpHealth;
	private int lvlUpDmg;
	private int lvlUpSpeed;
	private int lvlUpXp;
	
	public Dictionary <string, int[]> playerUnitStats = new Dictionary<string, int[]>();
	public int[] tempStats = new int[4];
	
	//Selection grid stuff
	public Dictionary<string, Texture2D> allBacsImages = new Dictionary<string, Texture2D>();
	public Dictionary<string, string> allBacsStories = new Dictionary<string, string>();
	public int selGridInt = 0;
	private int prevGridInt;
	public string selectedBacteria;
	private bool unlocked;

	private Vector2 topBar;
	private Vector2 leftBox;
	private Vector2 rightBox;
	
	// Use this for initialization
	void Start () {
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();

		topBar = new Vector2 (0, Screen.height/10);
		leftBox = new Vector2 (0, Screen.width/2);

		gold = gameStatus.getGold();
		xp = gameStatus.getXp();
		setBacsAndImages();
		setLevelMenu();

		playerUnitStats = unitStats.getPlayerUnitStats();
	}
	
	void OnGUI() {
		drawTop();
		
		drawSelectionMenu();
		
		if (levelMenu == true) {
			if (selectedBacteria != "empty") {
				drawBacteriaLevelMenu();
			}
		}
	}
	
	void drawSelectionMenu() {
		//left
		GUI.Box (new Rect (leftBox.x,topBar.y,leftBox.y, (Screen.height - 2*topBar.y)), "", trainerBox);
		
		//selection grid
		var tempArray = new List <Texture2D> (allBacsImages.Values.ToArray());
		tempArray.Remove(null);
		selGridInt = GUI.SelectionGrid(new Rect(0,topBar.y,leftBox.y, (Screen.height-2*(topBar.y))/4), selGridInt, tempArray.ToArray(), 4);
		selectedBacteria = allBacsImages.Keys.ToArray()[selGridInt];

		if (playerUnitStats.ContainsKey(selectedBacteria)) {
			unlocked = true;
			//stats for unit
			bacHealth = unitStats.getUnitHealth(selectedBacteria);
			bacDmg = unitStats.getUnitDamage(selectedBacteria);
			bacSpeed = unitStats.getUnitSpeed(selectedBacteria);
			bacLevel = unitStats.getUnitLevel(selectedBacteria);
			
			//stats that are added on levelup
			lvlUpHealth = bacHealth/unitStats.lvlUpHpIncrease;
			lvlUpDmg = bacDmg/unitStats.lvlUpDmgIncrease;
			lvlUpSpeed = bacSpeed/unitStats.calculateUnitSpeed(selectedBacteria);
			lvlUpXp = unitStats.getLevelUpCost(selectedBacteria);
			
		} else {
			bacLevel = 1;
			unlocked = false;
		}
	}
	
	void drawBacteriaLevelMenu() {
		//test
		int unlockXp = unitStats.getUnitUnlockXpCost(selectedBacteria);
		int levelsToUnlock = unitStats.getUnitUnlockLvls(selectedBacteria);
		int completedLevels = gameStatus.GetComponent<GameStatus>().getCompletedLevels();
		
		//right: Name, Level, Info
		GUI.Box (new Rect (leftBox.y,topBar.y,Screen.width/2, (Screen.height - 2*topBar.y)), "", trainerBox);
		GUI.Box (new Rect (leftBox.y,topBar.y,Screen.width/6,Screen.width/6), allBacsImages[selectedBacteria], yellowText);
		GUI.Box (new Rect (leftBox.y+Screen.width/6,topBar.y,2*(Screen.width/6),(Screen.width/6)/2), " "+selectedBacteria+"\n Level "+bacLevel, orangeText);
		GUI.Box (new Rect (leftBox.y+Screen.width/6,topBar.y+(Screen.width/6)/2,2*(Screen.width/6),(Screen.width/6)/2), allBacsStories[selectedBacteria], yellowText);

		if (unlocked) {
			//Statbox
			GUI.Label(new Rect (Screen.width/2+Screen.width/6,Screen.height/10+Screen.width/6,Screen.width/6,Screen.height/8), "Level "+bacLevel+" Stats: \nHealth : "+bacHealth+"\nDamage : "+bacDmg+"\nSpeed : "+bacSpeed, blueText);
			
			//NextLevelBox
			GUI.Label (new Rect (Screen.width/2+2*Screen.width/6,Screen.height/10+Screen.width/6,Screen.width/6,Screen.height/8), "Next Level : "+(bacLevel+1)+"\nHealth : "+(bacHealth+lvlUpHealth)+"\nDamage : "+(bacDmg+lvlUpDmg)+"\nSpeed : "+(bacSpeed+lvlUpSpeed)+"\nXP required to level : "+lvlUpXp*bacLevel, blueText);
			
			//LvlUpButton
			if (xp >= lvlUpXp*bacLevel) {
				if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/6+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", lvlUpButton)) {
					//Debug.Log ("lvlUpButtonPress");
					xp -= lvlUpXp*bacLevel;
					gameStatus.SendMessage("setXp", xp);
					xp = gameStatus.getXp();
					unitStats.setPlayerUnitStats(selectedBacteria, bacHealth+lvlUpHealth, bacDmg+lvlUpDmg, bacSpeed+lvlUpSpeed, bacLevel+1);
				}
			} else {
				if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/6+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", deactiveLvlUpButton)) {
					//Debug.Log ("nothing happens");
				}
			}
		} else {
			GUI.Label(new Rect (Screen.width/2+Screen.width/6,Screen.height/10+Screen.width/6,Screen.width/6,Screen.height/8), "This bacteria has not been unlocked yet. To unlock you need to complete "+levelsToUnlock+ " levels and have "+unlockXp+" xp.", blueText);

			//Unlock Button
			if (xp >= unlockXp && completedLevels >= levelsToUnlock) {
				if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/6+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", unlockButton)) {
					//Debug.Log ("unlock");
					xp -= unlockXp;
					gameStatus.SendMessage("setXp", xp);
					xp = gameStatus.getXp();
					unitStats.setPlayerUnit(selectedBacteria);
				}
			} else {
				if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/6+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", deactiveUnlockButton)) {
					//Debug.Log ("nothing happens");
				}
			}
		}


		prevGridInt = selGridInt;
	}

	void drawTop() {
		GUI.Box (new Rect (0,0,Screen.width/4,Screen.height/10), "", trainerText);
	}
	
	void setBacsAndImages() {
		allBacsStories = unitStats.getUnitDescriptions();
		allBacsImages = unitStats.getImageDict();
		//allBacsImages.Remove("empty");
	}
	
	void setLevelMenu() {
		levelMenu = true;
		skillMenu = false;
	}
}
