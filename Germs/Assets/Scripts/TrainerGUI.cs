﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public class TrainerGUI : MonoBehaviour {
	//GUI Stuff
	public GUIStyle trainerBox;
	public GUIStyle trainerText;
	public GUIStyle yellowText;
	public GUIStyle blueText;
	public GUIStyle orangeText;
	public GUIStyle bigNumbers;
	public GUIStyle lvlUpButton;
	public GUIStyle deactiveLvlUpButton;
	public GUIStyle shopHover;
	public GUIStyle mapHover;
	public GUIStyle levelHover;
	public GUIStyle skillsHover;

	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public Texture2D upgradeIcon;
	public Texture2D upgradeDeactIcon;
	public Texture2D gatbac;
	public Texture2D smallRed;
	public Texture2D phage;
	public Texture2D strep_p;
	public Texture2D smallBlue;
	public Texture2D smallPurple;
	public Texture2D blueBac;

	private bool levelMenu;
	private bool skillMenu;
	
	private Vector2 windowSize = new Vector2 (Screen.width/2, (Screen.height - 2*Screen.height/10));
	private Vector2 buttonSize = new Vector2 (Screen.width/16, Screen.width/16);
	//private Vector2 imgSize = new Vector2 (Screen.width/6, Screen.width/6);

	//Knowledge Stuff
	private Transform battleTracker;
	private Transform gameStatus;

	public float gold;
	public float xp;
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
	public string[] selGridStr;

	// Use this for initialization
	void Start () {
		gameStatus = GameObject.Find("GameStatus").transform;
		battleTracker = GameObject.Find ("BattleTracker").transform;

		gold = gameStatus.gameObject.GetComponent<GameStatus>().getGold();
		xp = gameStatus.gameObject.GetComponent<GameStatus>().getXp();

		allBacteriaStats = battleTracker.gameObject.GetComponent<BattleStatus>().getAllBacteriaStats();
		selGridStr = new string[allBacteriaStats.Keys.Count];

		var temp = 0;
		foreach (string key in allBacteriaStats.Keys) {
			selGridStr[temp] = key;
			temp += 1;
		}

		setBacsAndImages();
		setLevelMenu();
	}

	void OnGUI() {
		drawTop();

		drawSelectionMenu();

		if (levelMenu == true) {
			drawBacteriaLevelMenu();
		}

		if (skillMenu == true) {
			drawBacteriaSkillMenu();
		}
		drawBottom();
	}

	void drawSelectionMenu() {
		//left
		GUI.Box (new Rect (0,Screen.height/10,Screen.width/2, (Screen.height - 2*Screen.height/10)), "", trainerBox);
		
		//selection grid
		selGridInt = GUI.SelectionGrid(new Rect(0,Screen.height/10,Screen.width/2, (Screen.height-2*(Screen.height/10))/4), selGridInt, selGridStr, 4);
	
		if (selGridInt != prevGridInt) {
			setLevelMenu();
		}
	}

	void drawBacteriaLevelMenu() {

		tempStats = allBacteriaStats[selGridStr[selGridInt]];
		//right
		GUI.Box (new Rect (Screen.width/2,Screen.height/10,Screen.width/2, (Screen.height - 2*Screen.height/10)), "", trainerBox);
		GUI.Box (new Rect (Screen.width/2,Screen.height/10,Screen.width/6,Screen.width/6), allBacsImages[selGridStr[selGridInt]]);
		GUI.Box (new Rect (Screen.width/2+Screen.width/6,Screen.height/10,2*(Screen.width/6),(Screen.width/6)/2), " "+selGridStr[selGridInt]+"\n Level "+tempStats[3], orangeText);
		GUI.Box (new Rect (Screen.width/2+Screen.width/6,Screen.height/10+(Screen.width/6)/2,2*(Screen.width/6),(Screen.width/6)/2), allBacsStories[selGridStr[selGridInt]]);
		//Statbox
		GUI.Box (new Rect (Screen.width/2+50,Screen.height/10+Screen.width/6,Screen.width/6,Screen.height/8), "Level "+tempStats[3]+" Stats: \nHealth : "+tempStats[0]+"\nDamage : "+tempStats[1]+"\nSpeed : "+tempStats[2], blueText);
		
		//NextLevelBox
		GUI.Box (new Rect (Screen.width/2+50+Screen.width/6,Screen.height/10+Screen.width/6,Screen.width/6,Screen.height/8), "Next Level : "+(tempStats[3]+1)+"\nHealth : "+(tempStats[0]+lvlUpHealth)+"\nDamage : "+(tempStats[1]+lvlUpDmg)+"\nSpeed : "+(tempStats[2]+lvlUpSpeed)+"\nXP required to level : "+lvlUpXp*tempStats[3], yellowText);
		
		//LvlUpButton
		if (xp >= lvlUpXp*tempStats[3]) {
			if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/6+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", lvlUpButton)) {
				//Debug.Log ("lvlUpButtonPress");
				xp -= lvlUpXp*tempStats[3];
				gameStatus.SendMessage("setXp", xp);
				battleTracker.gameObject.GetComponent<BattleStatus>().setAllBacteriaStats(selGridStr[selGridInt], tempStats[0]+lvlUpHealth, tempStats[1]+lvlUpDmg, tempStats[2]+lvlUpSpeed, tempStats[3]+1);
			}
		} else {
			if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/6+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", deactiveLvlUpButton)) {
				//Debug.Log ("nothing happens");
			}
		}

		prevGridInt = selGridInt;
	}

	void drawBacteriaSkillMenu() {
		GUI.Box (new Rect (Screen.width/2,Screen.height/10,Screen.width/2,(Screen.height - 2*Screen.height/10)), "", trainerBox);

		GUI.Box (new Rect (Screen.width/2, Screen.height/10, Screen.width/2, buttonSize.y), "Here's some text about leveling up skills for bacteria: "+selGridStr[selGridInt]);

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

	void drawTop() {
		GUI.Box (new Rect (0,0,Screen.width/4,Screen.height/10), "", trainerText);

		if (GUI.Button (new Rect (Screen.width/2,0,Screen.width/10,Screen.height/10), "", levelHover)) {
			setLevelMenu();
		}
		if (GUI.Button(new Rect (Screen.width/2+Screen.width/10,0,Screen.width/10,Screen.height/10), "", skillsHover)) {
			skillMenu = true;
			levelMenu = false;
		}
	}
	void drawBottom() {
		if (GUI.Button (new Rect (0 + Screen.height/6,Screen.height - Screen.height/12,Screen.height/6,Screen.height/12), "", shopHover)) {
			//clickSound.Play ();	
			Application.LoadLevel ("Shop");
			Debug.Log ("Shop");
		}
		if (GUI.Button (new Rect (0,Screen.height - Screen.height/12,Screen.height/6,Screen.height/12), "", mapHover)) {
			//clickSound.Play ();	
			Application.LoadLevel ("Map");
			Debug.Log ("Map");
		}
		
		GUI.Box (new Rect (Screen.width - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), xpIcon);
		GUI.Label(new Rect(Screen.width - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), xp.ToString(), bigNumbers);
		
		GUI.Box (new Rect (Screen.width - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), goldIcon);
		GUI.Label(new Rect(Screen.width - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), gold.ToString(), bigNumbers);

	}

	void setBacsAndImages() {
		allBacsImages.Add ("Phage", phage);
		allBacsImages.Add ("Gatbac", gatbac);
		allBacsImages.Add ("Strepto", strep_p);
		allBacsImages.Add ("smallRed", smallRed);
		allBacsImages.Add ("blueBac", blueBac);
		allBacsImages.Add ("smallPurple", smallPurple);
		allBacsImages.Add ("smallBlue", smallBlue);
		
		allBacsStories.Add ("Gatbac", "Gatbac is a very fat Epstein-Barr virus,\nthat causes mononucleosis, also known as the \nkissing disease.");
		allBacsStories.Add ("Phage", "A Bacteriophage is a virus that infects and \nreplicates within a bacterium.Bacteriophages are \ncomposed of proteins that encapsulate a \nDNA or RNA genome.");
		allBacsStories.Add ("Strepto", "Streptococcus pneumoniae, or pneumococcus, is \na significant human pathogenic bacterium and is \nthe cause of pneumonia.");
		allBacsStories.Add ("smallRed", "...");
		allBacsStories.Add ("smallBlue", "...");
		allBacsStories.Add ("smallPurple", "...");
		allBacsStories.Add ("blueBac", "...");
	}

	void setLevelMenu() {
		levelMenu = true;
		skillMenu = false;
	}
}
