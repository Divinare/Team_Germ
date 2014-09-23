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
	private string storedNode;

	//GUI tools
	private bool drawBattleWindow = false;	

	// Use this for initialization
	void Start () {
		//store all nodes
		foreach (Transform child in transform) {
			allNodes.Add (child);
		}

		//retrieval
		statusTracker = GameObject.Find("StatusTracker").transform;
		retrieveGameStatus();
		setNodeActive(transform.FindChild("Node1"));

		//complete the node that was entered, temporary! needs better way of telling when level is complete
		storedNode = statusTracker.gameObject.GetComponent<storeMapStatus>().getNode();
		//Debug.Log(storedNode);
		if (storedNode != "") {
			setNodeCompleted(transform.FindChild(storedNode));
			setGold(transform.FindChild(storedNode));
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
					}
					//Debug.Log (hit.collider.gameObject.GetComponent<Node>().active);
					if (hit.collider.gameObject.GetComponent<Node>().active == true) {

						//storage
						storeGameStatus();
						statusTracker.SendMessage("storeNode", hit.collider.transform);

						//enter level through GUI window
						drawBattleWindow = true;
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
		if (GUI.Button (new Rect (0,0,100,50), "Shop")) {
			Debug.Log ("Shop");
		}
		if (GUI.Button (new Rect (Screen.width - 100,0,100,50), "Training")) {
			Debug.Log ("Training");
		}

		if (drawBattleWindow) {
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,200), "Level Info");
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 80,250,120), "You will recieve x gold and y xp \n from completion of this level!\n\n Other info from node");
			if (GUI.Button (new Rect (Screen.width /2 - 100, Screen.height / 2 + 50, 100, 50), "Enter Level")) {
				nodeLoadLevel (storedHit.collider.transform);
			}
			if (GUI.Button (new Rect (Screen.width /2 + 50, Screen.height / 2 + 50, 100, 50), "Exit")) {
				drawBattleWindow = false;
			}
		}

	}
	
}
