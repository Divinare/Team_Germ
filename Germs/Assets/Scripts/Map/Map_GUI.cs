using UnityEngine;
using System.Collections;

public class Map_GUI : MonoBehaviour {
	public float gold;
	public float xp;

	public Texture2D goldIcon;
	public Texture2D xpIcon;
	public GUIStyle bigNumbers;

	public AudioSource clickSound;

	//GameStateObject
	private Transform statusTracker;
 
	// Use this for initialization
	void Start () {
		statusTracker = GameObject.Find("StatusTracker").transform;
		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> (); 

		gold = statusTracker.gameObject.GetComponent<storeMapStatus>().getGold();
		xp = statusTracker.gameObject.GetComponent<storeMapStatus>().getXp();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//gold and xp
		GUI.Box (new Rect (Screen.width - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), xpIcon);
		GUI.Label(new Rect(Screen.width - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), xp.ToString(), bigNumbers);

		GUI.Box (new Rect (Screen.width - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), goldIcon);
		GUI.Label(new Rect(Screen.width - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), gold.ToString(), bigNumbers);

		//shop buttons

		if (GUI.Button (new Rect (0,0,Screen.height/6,Screen.height/12), "Shop")) {
			clickSound.Play ();	
			Debug.Log ("Shop");
		}
		if (GUI.Button (new Rect (Screen.width - Screen.height/6,0,Screen.height/6,Screen.height/12), "Training")) {
			clickSound.Play ();	
			Debug.Log ("Training");
		}
		if (GUI.Button (new Rect (0,Screen.height - Screen.height/12,Screen.height/4,Screen.height/12), "Choose bacs")) {
			clickSound.Play ();
			Debug.Log ("Choose bacteria");
		}

		//frame for chosen bacteria
		GUI.Box (new Rect (Screen.width/2 - Screen.width/4,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), "1");
		GUI.Box (new Rect (Screen.width/2 - Screen.width/6,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), "2");
		GUI.Box (new Rect (Screen.width/2 - Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), "3");
		GUI.Box (new Rect (Screen.width/2,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), "4");
		GUI.Box (new Rect (Screen.width/2 + Screen.width/12,Screen.height - Screen.height/10,Screen.width/12,Screen.height/10), "5");

	}
}
