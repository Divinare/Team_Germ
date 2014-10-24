using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStatus : MonoBehaviour {
	public static GameStatus gameStatus;

	//currency
	public float gold;
	public float xp;

	//storage for if a node is completed (true/false
	public List<bool> mapStateBools = new List<bool>();

	//storing what node was entered when a level is entered
	public string storedNode;
	
	//level status/count
	public bool levelComplete;
	public bool levelInterrupt;
	public int levelsCompleted = 0;
	public int levelsFailed = 0;

	// Use this for initialization
	void Start () {
		if (gameStatus == null) {
			DontDestroyOnLoad (gameObject);
			gameStatus = this;
		} else if (gameStatus != this) {
			Destroy (gameObject);
		}

		gold = 500; // for testing
		xp = 500;
	}

	//stores map status
	public void storeGameStatus(List<Transform> allNodes) {
		mapStateBools.Clear();
		foreach (Transform node in allNodes) {
			mapStateBools.Add(node.gameObject.GetComponent<Node>().completed);
		}
	}

	//get map status
	public List<bool> retrieveMapStateBools() {
		return mapStateBools;
	}

	//store gold
	public void setGold(Transform node) {
		gold += node.gameObject.GetComponent<Node>().getGold();
		xp += node.gameObject.GetComponent<Node>().getXp();
	}

	public float getGold() {
		return gold;
	}

	public float getXp() {
		return xp;
	}

	public void setXp(float newXp) {
		xp = newXp;
	}

	public void decreaseXp(float amount) {
		xp -= amount;
	}

	//the level that was entered was...
	public void levelCompleted() {
		levelsCompleted += 1;
		levelComplete = true;
	}
	
	public void levelInterrupted() {
		levelInterrupt = true;
	}
	
	public void levelFailed() {
		//failure count
		levelsFailed += 1;
	}
	
	//called on Start in map
	public bool onReturnToMap() {
		if (levelComplete) {
			return true;
		} else if (levelInterrupt) {
			return false;
		} else {
			return false;
		}
	}
	
	//store entered node
	public void storeNode(Transform node) {
		clearLevelBools();
		storedNode = node.transform.name;
	}

	//clear bools
	public void clearLevelBools() {
		levelComplete = false;
		levelInterrupt = false;
	}
	
	//get entered node
	public string getNode() {
		return storedNode;
	}

	//clear node
	public void clearNode() {
		storedNode = null;
	}

	public int getCompletedLevels() {
		return levelsCompleted;
	}
}
