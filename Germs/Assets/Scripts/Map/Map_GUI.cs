using UnityEngine;
using System.Collections;

public class Map_GUI : MonoBehaviour {
	public float gold;
	public float xp;

	//GameStateObject
	private Transform statusTracker;

	// Use this for initialization
	void Start () {
		statusTracker = GameObject.Find("StatusTracker").transform;

		gold = statusTracker.gameObject.GetComponent<storeMapStatus>().getGold();
		xp = statusTracker.gameObject.GetComponent<storeMapStatus>().getXp();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//gold and xp
		GUI.Box (new Rect (Screen.width - 200,Screen.height - 50,200,50), "Gold: "+gold+" XP: "+xp);

		//shop buttons
		if (GUI.Button (new Rect (0,0,100,50), "Shop")) {
			Debug.Log ("Shop");
		}
		if (GUI.Button (new Rect (Screen.width - 100,0,100,50), "Training")) {
			Debug.Log ("Training");
		}
	}
}
