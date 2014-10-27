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
	private List<bool> mapStateBools = new List<bool>();
	private string storedNode;
	public string nodeName;

	//GUI tools
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

	private GameStatus gameStatus;
	private UnitStats unitStats;
	private BattleStatus battleStatus;

	private BattleStartWindow battleStartWindow;

	// Use this for initialization
	void Start () {
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();

		battleStartWindow = GameObject.Find ("Map").GetComponent<BattleStartWindow>();
		clickSound = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioSource> (); 

		//store all nodes
		foreach (Transform child in transform) {
			allNodes.Add (child);
		}

		//retrieve game status
		retrieveGameStatus();

		getStoredNode();
		if (storedNode != null) {
			if (gameStatus.onReturnToMap()) {
				setNodeActive(transform.FindChild(storedNode));
				setNodeCompleted(transform.FindChild(storedNode));
				gameStatus.addGoldFromNode(transform.FindChild(storedNode).GetComponent<Node>().getId());
				gameStatus.addXpFromNode(transform.FindChild(storedNode).GetComponent<Node>().getId());
				gameStatus.clearNode();
				storeGameStatus();
			}
		}

		//first node
		setNodeActive(transform.FindChild("Node1"));
		if (!transform.FindChild("Node1").GetComponent<Node>().isNodeCompleted()) {
			transform.FindChild("Node1").FindChild("yellowArrow").gameObject.SetActive (true);
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
						storedHit = hit;
					
						if (hit.collider.gameObject.GetComponent<Node>().active == true) {
							//storage
							storeGameStatus();
							clickSound.Play();
							getNodeName(hit.collider.transform);

							//enter level through GUI window
							battleStatus.randomEnemiesForLevel();
							drawBattleWindow();
					}
				}
				
			}
			
		}
		
	}
	public string getNodeName(Transform node) {
		nodeName = node.name;
		return nodeName;
	}

	public void setNodeActive(Transform node) {
		node.SendMessage("setNodeActive");
	}
	
	public void setNodeCompleted(Transform node) {
		node.SendMessage("setNodeCompleted");
	}

	public void nodeLoadLevel(Transform node) {
		node.SendMessage("loadLevel");
	}

	public void storeGameStatus() {
		gameStatus.storeGameStatus(allNodes);
	}

	public void retrieveGameStatus() {
		mapStateBools = gameStatus.retrieveMapStateBools();
		for (int i = 0; i < mapStateBools.Count; i++) {
			if (mapStateBools[i]) {
				setNodeCompleted(allNodes[i]);
				setNodeActive(allNodes[i]);
			}
		}
	}

	public void getStoredNode() {
		storedNode = gameStatus.getNode();
	}

	public void drawBattleWindow() {
		lvlName = storedHit.transform.GetComponent<Node>().getLevelName();
		lvlInfo = storedHit.transform.GetComponent<Node>().getLevelInfo();
		gold = gameStatus.getGoldReward (storedHit.transform.GetComponent<Node> ().getId());
		xp = gameStatus.getXpReward (storedHit.transform.GetComponent<Node> ().getId());
		skill = storedHit.transform.GetComponent<Node>().getSkill();
		gameStatus.storeNode(storedHit.collider.transform);

		battleStartWindow.drawStartWindow(lvlName, lvlInfo, gold, xp, skill, storedHit.collider.transform);
	}

}
