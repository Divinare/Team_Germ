using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	//selection
	RaycastHit hit;
	RaycastHit storeHit;
	private float raycastLength = 200;
	private Vector3 clickPoint;
	
	//currency
	public float gold;
	public float xp;


	//checking tools
	private List<Transform> allNodes = new List<Transform>();
	private Transform statusTracker;

	// Use this for initialization
	void Start () {
		//vars
		gold = 0;
		xp = 0;
		
		//List<Transform> allNodes = new List<Transform>();
		
		foreach (Transform child in transform) {
			allNodes.Add (child);
		}

		//on start activate first node
		setNodeActive(transform.FindChild("Node1"));


		//store game status
		/*
		statusTracker = transform.FindChild("StatusTracker");
		storeGameStatus();
		retrieveGameStatus();
		*/
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
					//Debug.Log (hit.collider.gameObject.GetComponent<Node>().active);
					if (hit.collider.gameObject.GetComponent<Node>().active == true) {
						//enter level
						setNodeCompleted(hit.collider.transform);
						//storeGameStatus();
						//nodeLoadLevel (hit.collider.transform);

						//level completed test
						Debug.Log ("Let's pretend the level is complete");
						setNodeCompleted(hit.collider.transform);

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
		Debug.Log ("Storing");
		statusTracker.SendMessage("storeGameStatus", allNodes);
	}
	/*
	void retrieveGameStatus() {
		statusTracker.SendMessage("retrieveGameStatus", allNodes);
	}

	void setGameStatus(Dictionary <Transform, bool> gameStatus) {
		foreach(KeyValuePair<Transform, bool> entry in gameStatus) {
			if (entry.Value) {
				setNodeActive(entry.Key);
				setNodeCompleted(entry.Key);
			}
		}
	}
	*/

	void checkActiveConditions() {
		
	}
}
