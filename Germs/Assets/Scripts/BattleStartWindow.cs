using UnityEngine;
using System.Collections;

public class BattleStartWindow : MonoBehaviour {
	public string lvlName;
	public string lvlInfo;
	public float rewardGold;
	public float rewardXp;
	public bool rewardSkill;
	public Transform levelNode;

	public Texture2D xpIcon;
	public Texture2D goldIcon;

	private BattleStartWindow battleStartWindow;
	public Rect windowRect;
	public bool drawWindow;
	public GUISkin battleStartWindowBackground;
	
	// Use this for initialization
	void Start () {
		drawWindow = false;
		windowRect = centerRectangle(new Rect( (Screen.width / 2), (Screen.height / 2), (Screen.width / 2.5f), (Screen.height / 1.5f) ));
	}
	
	
	void OnGUI() {
		
		if (drawWindow) {
			
			GUI.skin = battleStartWindowBackground;
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "Level Info: "+lvlName+"\n\n"+lvlInfo+"\n\n"+"Upon victory you will recieve :\n"+rewardGold+" gold and "+rewardXp+"xp!");

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
	}
	
	private void DoMyWindow(int windowID) {
		
		// Centering the button
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button ("", GUILayout.Width(100), GUILayout.Height(50)))
			nodeLoadLevel(levelNode);

		if (GUI.Button (new Rect (25,25,30,30), "", "return")) {
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
