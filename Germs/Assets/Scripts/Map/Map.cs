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
	private Transform statusTracker;
	private Transform battleTracker;
	private string storedNode;

	//GUI tools
	private bool drawBattleWindow = false;
	public GUIStyle bigNumbers;
	public string lvlInfo;
	public string lvlName;
	public float gold;
	public float xp;
	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public Texture2D skillIcon;

	// Use this for initialization
	void Start () {
		//store all nodes
		foreach (Transform child in transform) {
			allNodes.Add (child);
		}

		//recall statustrackers
		statusTracker = GameObject.Find("StatusTracker").transform;
		battleTracker = GameObject.Find ("BattleTracker").transform;

		//retrieval
		retrieveGameStatus();

		//first node
		setNodeActive(transform.FindChild("Node1"));

		//complete the node that was entered, temporary! needs better way of telling when level is complete
		storedNode = battleTracker.gameObject.GetComponent<storeBattleStatus>().getNode();
		//Debug.Log(storedNode);
		if (storedNode != "") {
			if (battleTracker.gameObject.GetComponent<storeBattleStatus>().onReturnToMap()) {
				setNodeCompleted(transform.FindChild(storedNode));
				setGold(transform.FindChild(storedNode));
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
		statusTracker.SendMessage("storeGameStatus", allNodes);
	}

	void setGold(Transform node) {
		statusTracker.SendMessage("setGold", node);
	}

	void retrieveGameStatus() {
		gameBools = statusTracker.gameObject.GetComponent<storeMapStatus>().retrieveGameBools();

		for (int i = 0; i < gameBools.Count; i++) {
			if (gameBools[i]) {
				setNodeActive(allNodes[i]);
				setNodeCompleted(allNodes[i]);
			}
		}
	}

	void OnGUI() {
		//level loading window with info
		if (drawBattleWindow) {
			//get level info
			lvlName = storedHit.transform.GetComponent<Node>().getLevelName();
			lvlInfo = storedHit.transform.GetComponent<Node>().getLevelInfo();
			gold = storedHit.transform.GetComponent<Node>().getGold();
			xp = storedHit.transform.GetComponent<Node>().getXp();
			
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 300,250,400), "Level Info: "+lvlName);
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 270,250,120), lvlInfo);
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 120,250,160), "You will recieve :");
			GUI.Box(new Rect(Screen.width /2 - 80,Screen.height /2 - 80,64,64), goldIcon);
			GUI.Label(new Rect(Screen.width /2 - 80,Screen.height /2 - 80,64,64), gold.ToString(), bigNumbers);
			GUI.Box(new Rect(Screen.width /2 - 10,Screen.height /2 - 80,64,64), xpIcon);
			GUI.Label(new Rect(Screen.width /2 - 10,Screen.height /2 - 80,64,64), xp.ToString(), bigNumbers);
			GUI.Box(new Rect(Screen.width /2 + 60,Screen.height /2 - 80,64,64), skillIcon);
			if (GUI.Button (new Rect (Screen.width /2 - 100, Screen.height / 2 + 50, 100, 50), "Enter Level")) {
				battleTracker.SendMessage("storeNode", storedHit.collider.transform);
				nodeLoadLevel (storedHit.collider.transform);
			}
			if (GUI.Button (new Rect (Screen.width /2 + 50, Screen.height / 2 + 50, 100, 50), "Exit")) {
				drawBattleWindow = false;
			}


			//get level info
			lvlName = storedHit.transform.GetComponent<Node>().getLevelName();
			lvlInfo = storedHit.transform.GetComponent<Node>().getLevelInfo();

			//GUI.Box(new Rect(Screen.width/2 - Screen.width/6, Screen.height/8, Screen.width/3, Screen.width/3), "Level Menu: "+lvlName);
	
			}
		
	}
	
}
