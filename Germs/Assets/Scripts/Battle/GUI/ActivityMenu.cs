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

		float amountOfItems = 3;
		float buttonWidth = 80;
		float buttonHeight = 80;

		//bg box
		GUI.Box (new Rect (Screen.width-buttonWidth * amountOfItems, Screen.height - buttonHeight, buttonWidth * amountOfItems, buttonHeight), "");

		//buttons
		if (GUI.Button (new Rect (Screen.width-buttonWidth*3, Screen.height - buttonHeight, buttonWidth, buttonHeight), new GUIContent (meleeIcon, "Fancy melee attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("melee");
			clickSound.Play ();		
		}
		if (GUI.Button (new Rect (Screen.width-buttonWidth*2, Screen.height - buttonHeight, buttonWidth, buttonHeight), new GUIContent (rangedIcon, "Fancy ranged attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("ranged");
			clickSound.Play ();		
		}
		if (GUI.Button (new Rect (Screen.width-buttonWidth, Screen.height - buttonHeight, buttonWidth, buttonHeight), new GUIContent ("3", "Fancy magic attack"))) {
			GameObject currentUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
			currentUnit.GetComponent<UnitStatus>().switchSelectedAction ("heal");
			clickSound.Play ();		
		}
	

		//Tooltip position
		GUI.Label(new Rect(Screen.width - 100, Screen.height - buttonHeight - 50, 100, 100), GUI.tooltip);
		
		
	}
}
