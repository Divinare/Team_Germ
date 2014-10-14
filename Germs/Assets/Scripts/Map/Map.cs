using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	//selection
	RaycastHit hit;
	RaycastHit storedHit;
	private float raycastLength = 200;

	//checking tools
	private List<Transform> allNodes = new List<Transform>();
	private List<bool> gameBools = new List<bool>();
	private Transform gameStatus;
	private Transform battleTracker;
	private string storedNode;

	//GUI tools
	private bool drawBattleWindow = false;
	public GUIStyle bigNumbers;
	public GUIStyle titleLetters;
	public GUIStyle startHover;
	public GUIStyle returnHover;
	public string lvlInfo;
	public string lvlName;
	public float gold;
	public float xp;
	public bool skill;
	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public Texture2D skillIcon;

	//sound
	public AudioSource clickSound;

	// Use this for initialization
	void Start () {
		//store all nodes
		foreach (Transform child in transform) {
			allNodes.Add (child);
		}

		//recall statustrackers
		gameStatus = GameObject.Find("GameStatus").transform;
		battleTracker = GameObject.Find ("BattleTracker").transform;

		//retrieval
		retrieveGameStatus();

		//first node
		setNodeActive(transform.FindChild("Node1"));
		clickSound = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioSource> (); 

		getStoredNode();
		//Debug.Log(storedNode);
		if (storedNode != null) {
			//Debug.Log ("storedNode != empty");
			if (battleTracker.gameObject.GetComponent<BattleStatus>().onReturnToMap()) {
				setNodeCompleted(transform.FindChild(storedNode));
				setGold(transform.FindChild(storedNode));
				battleTracker.gameObject.GetComponent<BattleStatus>().clearNode();
				storeGameStatus();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//lazor from camera
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.blue);
		
		if (Physics.Raycast(ray, out hit, raycastLength)) {
			//Debug.Log(hit.collider.gameObject.name);
			if (hit.collider.gameObject.tag == "Node") {
				//Debug.Log("Node hit");
				if (Input.GetMouseButtonUp(0)) {
					if (!drawBattleWindow) {
						storedHit = hit;
					
						if (hit.collider.gameObject.GetComponent<Node>().active == true) {
							//storage
							storeGameStatus();
							clickSound.Play();
							//enter level through GUI window
							drawBattleWindow = true;
						}
					}
				}
				
			}
			
		}
		
	}
	
	void setNodeActive(Transform node) {
		node.SendMessage("setNodeActive");
	}
	
	void setNodeCompleted(Transform node) {
		node.SendMessage("setNodeCompleted");
	}

	void nodeLoadLevel(Transform node) {
		node.SendMessage("loadLevel");
	}

	void storeGameStatus() {
		gameStatus.SendMessage("storeGameStatus", allNodes);
	}

	void setGold(Transform node) {
		gameStatus.SendMessage("setGold", node);
	}

	void retrieveGameStatus() {
		gameBools = gameStatus.gameObject.GetComponent<GameStatus>().retrieveGameBools();
		for (int i = 0; i < gameBools.Count; i++) {
			if (gameBools[i]) {
				setNodeActive(allNodes[i]);
				setNodeCompleted(allNodes[i]);
			}
		}
	}

	void getStoredNode() {
		storedNode = battleTracker.gameObject.GetComponent<BattleStatus>().getNode();
	}

	void OnGUI() {
		//level loading window with info
		if (drawBattleWindow) {
			//get level info
			lvlName = storedHit.transform.GetComponent<Node>().getLevelName();
			lvlInfo = storedHit.transform.GetComponent<Node>().getLevelInfo();
			gold = storedHit.transform.GetComponent<Node>().getGold();
			xp = storedHit.transform.GetComponent<Node>().getXp();
			skill = storedHit.transform.GetComponent<Node>().getSkill();

			//huoh miten tota opacityä muutetaan
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/2), "");
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/2), "");
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/2), "");

			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, 0, Screen.width/2, Screen.height/2), "Level Info: "+lvlName, titleLetters);
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, Screen.height/24, Screen.width/2, Screen.height/4), lvlInfo);
			GUI.Box( new Rect(Screen.width/2 - Screen.width/4, Screen.height/3, Screen.width/2, Screen.height/6), "You will recieve:");
			GUI.Box (new Rect (Screen.width/2 - Screen.width/4,Screen.height/3 + Screen.height/16,Screen.width/12,Screen.height/10), xpIcon);
			GUI.Box (new Rect (Screen.width/2 - Screen.width/4,Screen.height/3 + Screen.height/16,Screen.width/12,Screen.height/10), xp.ToString(), bigNumbers);
			GUI.Box (new Rect (Screen.width/2 - Screen.width/6,Screen.height/3 + Screen.height/16,Screen.width/12,Screen.height/10), goldIcon);
			GUI.Box (new Rect (Screen.width/2 - Screen.width/6,Screen.height/3 + Screen.height/16,Screen.width/12,Screen.height/10), gold.ToString(), bigNumbers);
			if (skill) {
				GUI.Box (new Rect (Screen.width/2 - Screen.width/12,Screen.height/3 + Screen.height/16,Screen.width/12,Screen.height/10), skillIcon);
			}
			if(GUI.Button(new Rect(Screen.width/2 - Screen.width/4, Screen.height/2, Screen.width/8, Screen.height/12), "", startHover)) {
				battleTracker.SendMessage("storeNode", storedHit.collider.transform);
				clickSound.Play();
				nodeLoadLevel (storedHit.collider.transform);
			}
			if(GUI.Button(new Rect(Screen.width/2 + Screen.width/8, Screen.height/2, Screen.width/8, Screen.height/12), "", returnHover)) {
				clickSound.Play();
				drawBattleWindow = false;
			}
		}
		
	}
	
}
