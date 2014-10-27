using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStatus : MonoBehaviour {
	public static GameStatus gameStatus;

	//currency
	public int gold;
	public int xp;

	private int firstLvlGoldReward = 250;
	private int goldAddOn = 50;

	private int firstLvlXpReward = 250;
	private int xpAddOn = 50; // for example 250, 300, 350, 400.. when firstLvlReward is 250 and lvlRewardAddOn is 50

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

		gold = 0;
		xp = 0;
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
	public void addGoldFromNode(int nodeId) {
		addGold(getGoldReward(nodeId));
	}
	public void addXpFromNode(int nodeId) {
		addXp(getXpReward(nodeId));
	}

	public int getXpReward(int nodeId) {
		return firstLvlXpReward + nodeId * xpAddOn;
	}
	public int getGoldReward(int nodeId) {
		return firstLvlGoldReward + nodeId * goldAddOn;
	}       

	public int getGold() {
		return gold;
	}
	public void addGold(int amount) {
		gold += amount;
	}
	public bool decreaseGold(int amount) {
		if (gold - amount >= 0) {
			gold -= amount;
			return true;
		}
		return false;
	}
	public int getXp() {
		return xp;
	}

	public void setXp(int newXp) {
		xp = newXp;
	}

	public void addXp(int amount) {
		xp += amount;
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
