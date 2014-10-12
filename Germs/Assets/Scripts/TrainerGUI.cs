using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainerGUI : MonoBehaviour {
	//GUI Stuff
	public GUIStyle trainerBox;
	public GUIStyle trainerText;
	public GUIStyle yellowText;
	public GUIStyle blueText;
	public GUIStyle bigNumbers;
	public GUIStyle lvlUpButton;
	public GUIStyle deactiveLvlUpButton;
	public GUIStyle shopHover;
	public GUIStyle mapHover;

	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public Texture2D gatbac;
	public Texture2D smallRed;
	public Texture2D phage;
	public Texture2D strep_p;
	public Texture2D smallBlue;
	public Texture2D smallPurple;
	public Texture2D blueBac;

	//Knowledge Stuff
	private Transform battleTracker;
	private Transform gameStatus;

	public float gold;
	public float xp;
	public int lvlUpHealth;
	public int lvlUpDmg;
	public int lvlUpSpeed;
	public int lvlUpXp;


	public List<int[]> allBacsStats = new List<int[]>();
	public int[] tempStats = new int[4];

	//Selection grid stuff
	public List<string> allBacsTest = new List<string>();
	public Dictionary<string, Texture2D> allBacsImages = new Dictionary<string, Texture2D>();
	public int selGridInt = 0;

	// Use this for initialization
	void Start () {
		gameStatus = GameObject.Find("GameStatus").transform;
		battleTracker = GameObject.Find ("BattleTracker").transform;

		gold = gameStatus.gameObject.GetComponent<GameStatus>().getGold();
		xp = gameStatus.gameObject.GetComponent<GameStatus>().getXp();

		allBacsTest = battleTracker.gameObject.GetComponent<BattleStatus>().getAllBacsTest();
		allBacsStats = battleTracker.gameObject.GetComponent<BattleStatus>().getAllBacsStats();
		setBacsAndImages();
	}

	void setBacsAndImages() {
		allBacsImages.Add ("Phage", phage);
		allBacsImages.Add ("Gatbac", gatbac);
		allBacsImages.Add ("Strep_p", strep_p);
		allBacsImages.Add ("smallRed", smallRed);
		allBacsImages.Add ("blueBac", blueBac);
		allBacsImages.Add ("smallPurple", smallPurple);
		allBacsImages.Add ("smallBlue", smallBlue);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//top
		GUI.Box (new Rect (0,0,Screen.width/4,Screen.height/10), "", trainerText);

		//left
		GUI.Box (new Rect (0,Screen.height/10,Screen.width/2,Screen.height-Screen.height/10-Screen.height/10), "", trainerBox);
		selGridInt = GUI.SelectionGrid(new Rect(0,Screen.height/10,Screen.width/2, (Screen.height-Screen.height/10-Screen.height/10)/4), selGridInt, allBacsTest.ToArray(), 6);
		//Debug.Log (selGridInt);


		//right
		GUI.Box (new Rect (Screen.width/2,Screen.height/10,Screen.width/2,Screen.height-Screen.height/10-Screen.height/10), "", trainerBox);

		var imgVar = allBacsTest[selGridInt];
		GUI.Box (new Rect (Screen.width/2+Screen.width/8,Screen.height/10,Screen.width/4,Screen.width/4), allBacsImages[allBacsTest[selGridInt]]);

		tempStats = allBacsStats[selGridInt];
		//Statbox
		GUI.Box (new Rect (Screen.width/2+50,Screen.height/10+Screen.width/4,Screen.width/4,Screen.height/8), "Level "+tempStats[3]+" Stats: \nHealth : "+tempStats[0]+"\nDamage : "+tempStats[1]+"\nSpeed : "+tempStats[2], blueText);

		//NextLevelBox
		GUI.Box (new Rect (Screen.width/2+Screen.width/4,Screen.height/10+Screen.width/4,Screen.width/4,Screen.height/8), "Next Level : "+(tempStats[3]+1)+"\nHealth : "+(tempStats[0]+lvlUpHealth)+"\nDamage : "+(tempStats[1]+lvlUpDmg)+"\nSpeed : "+(tempStats[2]+lvlUpSpeed)+"\nXP required to level : "+lvlUpXp*tempStats[3], yellowText);

		//LvlUpButton
		if (xp >= lvlUpXp*tempStats[3]) {
			if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/4+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", lvlUpButton)) {
				//Debug.Log ("lvlUpButtonPress");
				xp -= lvlUpXp*tempStats[3];
				gameStatus.SendMessage("setXp", xp);
				battleTracker.gameObject.GetComponent<BattleStatus>().setAllBacsStats(selGridInt, tempStats[0]+lvlUpHealth, tempStats[1]+lvlUpDmg, tempStats[2]+lvlUpSpeed, tempStats[3]+1);
			}
		} else {
			if (GUI.Button(new Rect (Screen.width/2+Screen.width/8,Screen.height/10+Screen.width/4+Screen.height/8+Screen.height/8,Screen.width/4,Screen.height/8), "", deactiveLvlUpButton)) {
				//Debug.Log ("nothing happens");
			}
		}
		//bottom
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
}
