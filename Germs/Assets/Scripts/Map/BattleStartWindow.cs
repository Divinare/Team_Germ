using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStartWindow : MonoBehaviour {
	public string lvlName;
	public string lvlInfo;
	public float rewardGold;
	public float rewardXp;
	public bool rewardSkill;
	private Transform levelNode;

	public Texture2D xpIcon;
	public Texture2D goldIcon;

	private BattleStartWindow battleStartWindow;
	private Rect windowRect;
	private bool drawWindow;
	public GUISkin battleStartWindowBackground;

	private BattleStatus battleStatus;
	private List<string> enemiesToSpawn = new List<string>();
	private string enemyList;

	// Use this for initialization
	void Start () {
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		drawWindow = false;
		windowRect = centerRectangle(new Rect( (Screen.width / 2), (Screen.height / 2), (Screen.width / 2.5f), (Screen.height / 1.5f) ));
	}
	
	
	void OnGUI() {
		
		if (drawWindow) {
			GUI.skin = battleStartWindowBackground;
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "Level Info: "+lvlName+"\n\n"+lvlInfo+"\n\n"+"Upon victory you will recieve :\n"+rewardGold+" gold and "+rewardXp+"xp!\n\nYou will face:\n"+enemyList);

		}
		GUI.skin = null;
	}
	
	// Called by TurnHandler - let's this class know that the battle has ended and a window needs to be drawn
	public void drawStartWindow(string levelName, string levelInfo, float gold, float xp, bool skill, Transform node) {
		lvlName = levelName;
		lvlInfo = levelInfo;
		rewardGold = gold;
		rewardXp = xp;
		rewardSkill = skill;
		levelNode = node;
		drawWindow = true;

		enemyList = "";
		enemiesToSpawn = battleStatus.getEnemiesToSpawn();
		foreach (string name in enemiesToSpawn) {
			enemyList += name+" ";
		}
	}
	
	private void DoMyWindow(int windowID) {
		
		// Centering the button
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button ("", GUILayout.Width (128), GUILayout.Height (64))) {
			AudioController.audioController.playBattleMusic ();
			AudioController.audioController.stopMapMusic();
			nodeLoadLevel (levelNode);
		}


		if (GUI.Button (new Rect (Screen.width/3,25,30,30), "", "return")) {
			drawWindow = false;
		}

		// More centering...
		GUILayout.EndVertical ();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
	}
	
	// Centers a rectangle	
	private Rect centerRectangle(Rect  r) {
		r.x = ( Screen.width - r.width ) / 2;
		r.y = ( Screen.height - r.height ) / 2;
		
		return r;
	}

	public void nodeLoadLevel(Transform node) {
		node.SendMessage("loadLevel");
	}
}
