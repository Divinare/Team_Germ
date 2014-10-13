using UnityEngine;
using System.Collections;

public class ActivityMenu : MonoBehaviour {
	
	public Texture2D meleeIcon;
	public Texture2D rangedIcon;
	public AudioSource clickSound;
	
	private Vector2 buttonSize;
	// Use this for initialization
	void Start () {
		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> (); 
		buttonSize = new Vector2 (80, 60);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {

		float amountOfItems = 3;

		// Create menu button
		MenuCreator.menuCreator.createMenu ();


		//bg box
		GUI.Box (new Rect (Screen.width-buttonSize.x * amountOfItems, Screen.height - buttonSize.y, buttonSize.x * (amountOfItems+1), buttonSize.y), "");

		//buttons
		if (GUI.Button (new Rect (Screen.width-buttonSize.x*4, Screen.height - buttonSize.y, buttonSize.x, buttonSize.y), new GUIContent (meleeIcon, "Fancy melee attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("melee");
			clickSound.Play ();		
		}
		if (GUI.Button (new Rect (Screen.width-buttonSize.x*3, Screen.height - buttonSize.y, buttonSize.x, buttonSize.y), new GUIContent (rangedIcon, "Fancy ranged attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("ranged");
			clickSound.Play ();		
		}
		if (GUI.Button (new Rect (Screen.width-buttonSize.x*2, Screen.height - buttonSize.y, buttonSize.x, buttonSize.y), new GUIContent ("3", "Fancy magic attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("heal");
			clickSound.Play ();		
		}
	

		//Tooltip position
		GUI.Label(new Rect(Screen.width - 100, Screen.height - buttonSize.y - 50, 100, 100), GUI.tooltip);
		
		
	}
}
