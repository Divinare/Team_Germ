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
	public int lvlUpHealth;
	public int lvlUpDmg;
	public int lvlUpSpeed;
	public int lvlUpXp;
	
	public Dictionary <string, int[]> allBacteriaStats = new Dictionary<string, int[]>();
	public int[] tempStats = new int[4];
	
	//Selection grid stuff
	public Dictionary<string, Texture2D> allBacsImages = new Dictionary<string, Texture2D>();
	public Dictionary<string, string> allBacsStories = new Dictionary<string, string>();
	public int selGridInt = 0;
	private int prevGridInt;
	public string selectedBacteria;
	
	// Use this for initialization
	void Start () {
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();

		gold = gameStatus.getGold();
		xp = gameStatus.getXp();
		setBacsAndImages();
		
		/*allBacteriaStats = battleStatus.getAllBacteriaStats();
		
		var temp = 0;
		foreach (string key in allBacteriaStats.Keys) {
			selGridStr[temp] = key;
			temp += 1;
		}
		*/
		setLevelMenu();
	}
	
	void OnGUI() {
		drawTop();
		
		drawSelectionMenu();
		
		if (levelMenu == true) {
			drawBacteriaLevelMenu();
		}
		/*
		if (skillMenu == true) {
			drawBacteriaSkillMenu();
		}
		*/
	}
	
	void drawSelectionMenu() {
		//left

		GUI.Box (new Rect (0,Screen.height/10,Screen.width/2, (Screen.height - 2*Screen.height/10)), "", trainerBox);
		
		//selection grid
		selGridInt = GUI.SelectionGrid(new Rect(0,Screen.height/10,Screen.width/2, (Screen.height-2*(Screen.height/10))/4), selGridInt, allBacsImages.Values.ToArray(), 4);
		selectedBacteria = allBacsImages.Keys.ToArray()[selGridInt];
		
		if (selGridInt != prevGridInt) {
			setLevelMenu();
		}
	}
	
	void drawBacteriaLevelMenu() {
		
		bacHealth = battleStatus.getBacteriaHealth(selectedBacteria);
		bacDmg = battleStatus.getBacteriaDamage(selectedBacteria);
		bacSpeed = battleStatus.getBacteriaSpeed(selectedBacteria);
		bacLevel = battleStatus.getBacteriaLevel(selectedBacteria);
		
		//right
		GUI.Box (new Rect (Screen.width/2,Screen.height/10,Screen.width/2, (Screen.height - 2*Screen.height/10)), "", trainerBox);
		GUI.Box (new Rect (Screen.width/2,Screen.height/10,Screen.width/6,Screen.width/6), allBacsImages[selectedBacteria], yellowText);
		GUI.Box (new Rect (Screen.width/2+Screen.width/6,Screen.height/10,2*(Screen.width/6),(Screen.width/6)/2), " "+selectedBacteria+"\n Level "+bacLevel, orangeText);
		GUI.Box (new Rect (Screen.width/2+Screen.width/6,Screen.height/10+(Screen.width/6)/2,2*(Screen.width/6),(Screen.width/6)/2), allBacsStories[selectedBacteria], yellowText);

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
				battleStatus.setAllBacteriaStats(selectedBacteria, bacHealth+lvlUpHealth, bacDmg+lvlUpDmg, bacSpeed+lvlUpSpeed, bacLevel+1);
			}
		} else {
			if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/6+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", deactiveLvlUpButton)) {
				//Debug.Log ("nothing happens");
			}
		}
		
		prevGridInt = selGridInt;
	}
	/*
	void drawBacteriaSkillMenu() {
		GUI.Box (new Rect (Screen.width/2,Screen.height/10,Screen.width/2,(Screen.height - 2*Screen.height/10)), "", trainerBox);
		
		GUI.Box (new Rect (Screen.width/2, Screen.height/10, Screen.width/2, buttonSize.y), "Here's some text about leveling up skills for bacteria: "+selectedBacteria);
		
		//skill slots
		GUI.Box (new Rect (Screen.width/2, Screen.height/10+buttonSize.y, buttonSize.x, buttonSize.y), "Icon 1");
		GUI.Box (new Rect (Screen.width/2+buttonSize.x, Screen.height/10+buttonSize.y, Screen.width/2-2*buttonSize.x, buttonSize.y), "Text about skill");
		GUI.Box (new Rect (Screen.width-buttonSize.x, Screen.height/10+buttonSize.y, buttonSize.x, buttonSize.y), upgradeIcon);
		
		GUI.Box (new Rect (Screen.width/2, Screen.height/10+2*buttonSize.y, buttonSize.x, buttonSize.y), "Icon 2");
		GUI.Box (new Rect (Screen.width/2+buttonSize.x, Screen.height/10+2*buttonSize.y, Screen.width/2-2*buttonSize.x, buttonSize.y), "Text about skill");
		GUI.Box (new Rect (Screen.width-buttonSize.x, Screen.height/10+2*buttonSize.y, buttonSize.x, buttonSize.y), upgradeDeactIcon);
		
		GUI.Box (new Rect (Screen.width/2, Screen.height/10+3*buttonSize.y, buttonSize.x, buttonSize.y), "Icon 3");
		GUI.Box (new Rect (Screen.width/2+buttonSize.x, Screen.height/10+3*buttonSize.y, Screen.width/2-2*buttonSize.x, buttonSize.y), "Text about skill");
		GUI.Box (new Rect (Screen.width-buttonSize.x, Screen.height/10+3*buttonSize.y, buttonSize.x, buttonSize.y), upgradeDeactIcon);
		
		GUI.Box (new Rect (Screen.width/2, Screen.height/10+4*buttonSize.y, buttonSize.x, buttonSize.y), "Icon 4");
		GUI.Box (new Rect (Screen.width/2+buttonSize.x, Screen.height/10+4*buttonSize.y, Screen.width/2-2*buttonSize.x, buttonSize.y), "Text about skill");
		GUI.Box (new Rect (Screen.width-buttonSize.x, Screen.height/10+4*buttonSize.y, buttonSize.x, buttonSize.y), upgradeIcon);
	}
	*/
	
	void drawTop() {
		GUI.Box (new Rect (0,0,Screen.width/4,Screen.height/10), "", trainerText);
		/*
		if (GUI.Button (new Rect (Screen.width/2,0,Screen.width/10,Screen.height/10), "", levelHover)) {
			setLevelMenu();
		}
		if (GUI.Button(new Rect (Screen.width/2+Screen.width/10,0,Screen.width/10,Screen.height/10), "", skillsHover)) {
			skillMenu = true;
			levelMenu = false;
		}
		*/
	}
	
	void setBacsAndImages() {
		allBacsStories.Add ("Gatbac", "Gatbac is a very fat Epstein-Barr virus, that causes mononucleosis, also known as the kissing disease.");
		allBacsStories.Add ("Phage", "A Bacteriophage is a virus that infects and replicates within a bacterium. Bacteriophages are composed of proteins that encapsulate a DNA or RNA genome.");
		allBacsStories.Add ("Strepto", "Streptococcus pneumoniae, or pneumococcus, is a significant human pathogenic bacterium and is the cause of pneumonia.");
		allBacsStories.Add ("smallRed", "...");
		allBacsStories.Add ("smallBlue", "...");
		allBacsStories.Add ("smallPurple", "...");
		allBacsStories.Add ("blueBac", "...");

		allBacsImages = unitStats.getImageDict();
		allBacsImages.Remove("empty");
	}
	
	void setLevelMenu() {
		levelMenu = true;
		skillMenu = false;
	}
}
