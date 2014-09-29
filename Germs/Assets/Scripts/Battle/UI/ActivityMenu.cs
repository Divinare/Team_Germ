using UnityEngine;
using System.Collections;

public class ActivityMenu : MonoBehaviour {
	
	public Texture2D meleeIcon;
	public Texture2D rangedIcon;
	public AudioSource clickSound;
	
	
	// Use this for initialization
	void Start () {
		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> (); 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		
		//bg box
		GUI.Box (new Rect (Screen.width - 400, Screen.height - 50, 500, 50), "");
		//buttons
		if (GUI.Button (new Rect (Screen.width - 400, Screen.height - 50, 50, 50), new GUIContent (meleeIcon, "Fancy melee attack"))) {
			clickSound.Play ();		
		}
		
		if (GUI.Button (new Rect (Screen.width - 350, Screen.height - 50, 50, 50), new GUIContent (rangedIcon, "Fancy ranged attack"))) {
			clickSound.Play ();
		}
		
		if (GUI.Button (new Rect (Screen.width - 300, Screen.height - 50, 50, 50), "3")) {
			clickSound.Play();
		}
		
		
		if (GUI.Button (new Rect (Screen.width - 250, Screen.height - 50, 50, 50), "4")) {
			clickSound.Play ();
		}
		
		if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 50, 50, 50), "5")) {
			clickSound.Play ();
		}
		
		
		//Tooltip position
		GUI.Label(new Rect(Screen.width - 100, Screen.height - 100, 100, 100), GUI.tooltip);
		
		
	}
}
