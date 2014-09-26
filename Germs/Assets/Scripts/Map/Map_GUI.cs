using UnityEngine;
using System.Collections;

public class Map_GUI : MonoBehaviour {
	public float gold;
	public float xp;

	public Texture2D goldIcon;
	public Texture2D xpIcon;

	public GUIStyle bigNumbers;

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
		GUI.Box (new Rect (Screen.width - Screen.width/6,Screen.height - Screen.height/8,Screen.width/12,Screen.height/8), xpIcon);
		GUI.Label(new Rect(Screen.width - Screen.width/6,Screen.height - Screen.height/8,Screen.width/12,Screen.height/8), xp.ToString(), bigNumbers);

		GUI.Box (new Rect (Screen.width - Screen.width/12,Screen.height - Screen.height/8,Screen.width/12,Screen.height/8), goldIcon);
		GUI.Label(new Rect(Screen.width - Screen.width/12,Screen.height - Screen.height/8,Screen.width/12,Screen.height/8), gold.ToString(), bigNumbers);

		//shop buttons
		if (GUI.Button (new Rect (0,0,Screen.height/6,Screen.height/12), "Shop")) {
			Debug.Log ("Shop");
		}
		if (GUI.Button (new Rect (Screen.width - Screen.height/6,0,Screen.height/6,Screen.height/12), "Training")) {
			Debug.Log ("Training");
		}
		if (GUI.Button (new Rect (0,Screen.height - Screen.height/12,Screen.height/4,Screen.height/12), "Choose Your Bacs")) {
		}
	}
}
