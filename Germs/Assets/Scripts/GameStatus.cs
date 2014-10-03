using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStatus : MonoBehaviour {
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

		//muy importanto!
		DontDestroyOnLoad(this);

		//remove duplicates
		if (FindObjectsOfType(GetType()).Length > 1) {
			Destroy(gameObject);
		}
	}


	//stores map status
	void storeGameStatus(List<Transform> allNodes) {
		gameBools.Clear();
		foreach (Transform node in allNodes) {
			gameBools.Add(node.gameObject.GetComponent<Node>().completed);
		}
	}

	//get map status
	public List<bool> retrieveGameBools() {
		return gameBools;
	}

	//store gold
	void setGold(Transform node) {
		gold += node.gameObject.GetComponent<Node>().getGold();
		xp += node.gameObject.GetComponent<Node>().getXp();
	}

	public float getGold() {
		return gold;
	}

	public float getXp() {
		return xp;
	}
}
