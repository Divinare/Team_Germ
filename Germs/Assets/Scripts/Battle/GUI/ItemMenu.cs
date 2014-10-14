using UnityEngine;
using System.Collections;

public class ItemMenu : MonoBehaviour {
	
	public AudioSource clickSound;

	// just for testing
	public Texture2D meleeIcon;
	public Texture2D rangedIcon;
	
	void Start () {
		clickSound = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioSource> (); 
	}
	
	void OnGUI() {
		
		float amountOfItems = 5;
		float buttonWidth = 40;
		float buttonHeight = 40;
		float scaleToMiddle = Screen.width * 0.4f;

		GUI.contentColor = Color.yellow;

		//bg box
		GUI.Box (new Rect (Screen.width-buttonWidth * amountOfItems - scaleToMiddle, Screen.height - buttonHeight, buttonWidth * amountOfItems, buttonHeight), "");
		
		//buttons
		if (GUI.Button (new Rect (Screen.width-buttonWidth * 5  - scaleToMiddle, Screen.height - buttonHeight, buttonWidth, buttonHeight), new GUIContent ("1", "Fancy item 1"))) {
			clickSound.Play ();		
		}
		if (GUI.Button (new Rect (Screen.width-buttonWidth * 4  - scaleToMiddle, Screen.height - buttonHeight, buttonWidth, buttonHeight), new GUIContent ("2", "Fancy item 2"))) {
			clickSound.Play ();		
		}
		if (GUI.Button (new Rect (Screen.width-buttonWidth * 3  - scaleToMiddle, Screen.height - buttonHeight, buttonWidth, buttonHeight), new GUIContent ("3", "Fancy item 3"))) {
			clickSound.Play ();		
		}
		if (GUI.Button (new Rect (Screen.width-buttonWidth * 2  - scaleToMiddle, Screen.height - buttonHeight, buttonWidth, buttonHeight), new GUIContent ("4", "Fancy item 3"))) {
			clickSound.Play ();		
		}
		if (GUI.Button (new Rect (Screen.width-buttonWidth  - scaleToMiddle, Screen.height - buttonHeight, buttonWidth, buttonHeight), new GUIContent ("5", "Fancy item 3"))) {
			clickSound.Play ();		
		}
		//Tooltip position
		GUI.Label(new Rect(Screen.width-(buttonWidth*amountOfItems)  - scaleToMiddle, Screen.height - buttonHeight-20, 100, 100), GUI.tooltip);
		
		
	}

}