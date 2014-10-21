﻿using UnityEngine;
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
	private BattleStartWindow battleStartWindow;

	// Use this for initialization
	void Start () {
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();
		battleStartWindow = GameObject.Find ("Map").GetComponent<BattleStartWindow>();

		//store all nodes
		foreach (Transform child in transform) {
			allNodes.Add (child);
		}

		//retrieve game status
		retrieveGameStatus();

		//first node
		setNodeActive(transform.FindChild("Node1"));
		if (!transform.FindChild("Node1").GetComponent<Node>().isNodeCompleted()) {
			transform.FindChild("Node1").FindChild("yellowArrow").gameObject.SetActive (true);
		}

		clickSound = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioSource> (); 

		getStoredNode();
		if (storedNode != null) {
			if (gameStatus.onReturnToMap()) {
				setNodeCompleted(transform.FindChild(storedNode));
				setGold(transform.FindChild(storedNode));
				gameStatus.clearNode();
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
						storedHit = hit;
					
						if (hit.collider.gameObject.GetComponent<Node>().active == true) {
							//storage
							storeGameStatus();
							clickSound.Play();
							getNodeName(hit.collider.transform);

							//enter level through GUI window
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

	public void setGold(Transform node) {
		gameStatus.setGold(node);
	}

	public void retrieveGameStatus() {
		mapStateBools = gameStatus.retrieveMapStateBools();
		for (int i = 0; i < mapStateBools.Count; i++) {
			if (mapStateBools[i]) {
				setNodeActive(allNodes[i]);
				setNodeCompleted(allNodes[i]);
			}
		}
	}

	public void getStoredNode() {
		storedNode = gameStatus.getNode();
	}

	public void drawBattleWindow() {
		lvlName = storedHit.transform.GetComponent<Node>().getLevelName();
		lvlInfo = storedHit.transform.GetComponent<Node>().getLevelInfo();
		gold = storedHit.transform.GetComponent<Node>().getGold();
		xp = storedHit.transform.GetComponent<Node>().getXp();
		skill = storedHit.transform.GetComponent<Node>().getSkill();
		gameStatus.storeNode(storedHit.collider.transform);

		battleStartWindow.drawStartWindow(lvlName, lvlInfo, gold, xp, skill, storedHit.collider.transform);
	}

}
