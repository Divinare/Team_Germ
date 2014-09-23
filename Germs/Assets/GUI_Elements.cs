using UnityEngine;
using System.Collections;

public class GUI_Elements : MonoBehaviour {

	public Texture2D meleeIcon;
	public Texture2D rangedIcon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//bg box
		GUI.Box(new Rect(Screen.width - 400,Screen.height - 50,500,50),"");
			//buttons
			if (GUI.Button (new Rect(Screen.width - 400,Screen.height - 50,50,50), new GUIContent (meleeIcon, "Fancy melee attack"))); {
				}
			if (GUI.Button (new Rect(Screen.width - 350,Screen.height - 50,50,50), new GUIContent (rangedIcon, "Fancy ranged attack"))); {
				}
			if (GUI.Button (new Rect(Screen.width - 300,Screen.height - 50,50,50),"3")); {
			}
			if (GUI.Button (new Rect(Screen.width - 250,Screen.height - 50,50,50),"4")); {
			}
			if (GUI.Button (new Rect(Screen.width - 200,Screen.height - 50,50,50),"5")); {
			}

			//Tooltip position
			GUI.Label(new Rect(Screen.width - 100, Screen.height - 100, 100, 100), GUI.tooltip);
			
			
	}
}
