using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class storeMapStatus : MonoBehaviour {
	//currency
	public float gold;
	public float xp;

	//storage units
	public List<bool> gameBools = new List<bool>();
	public string nodeFrom;

	// Use this for initialization
	void Start () {
		gold = 0;
		xp = 0;

		DontDestroyOnLoad(this);

		//remove duplicates
		if (FindObjectsOfType(GetType()).Length > 1) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void storeGameStatus(List<Transform> allNodes) {
		gameBools.Clear();
		foreach (Transform node in allNodes) {
			gameBools.Add(node.gameObject.GetComponent<Node>().completed);
		}
		//dictCheck(gameState);
	}
	
	public List<bool> retrieveGameBools() {
		return gameBools;
	}

	void storeNode(Transform node) {
		nodeFrom = node.transform.name;
	}

	public string getNode() {
		return nodeFrom;
	}

	void setGold(Transform node) {
		Debug.Log (node.gameObject.GetComponent<Node>().getGold());
		gold += node.gameObject.GetComponent<Node>().getGold();
		xp += node.gameObject.GetComponent<Node>().getXp();

	}

	void OnGUI() {
		GUI.Box (new Rect (Screen.width - 200,Screen.height - 50,200,50), "Gold: "+gold+" XP: "+xp);
	}


}
