using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class storeMapStatus : MonoBehaviour {

	private Dictionary<Transform, bool> gameState = new Dictionary<Transform, bool>();
	private bool storeState;
	private Transform map;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad( gameObject );
		map = transform.Find("Map");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void storeGameStatus(List<Transform> allNodes) {
		foreach (Transform node in allNodes) {
			gameState[node] = node.GetComponent<Node>().completed;
		}

		Debug.Log (gameState.Count);
	}

	void retrieveGameStatus() {
		Debug.Log ("Retrieve status");
		map.SendMessage("setGameStatus", gameState);
	}
}
