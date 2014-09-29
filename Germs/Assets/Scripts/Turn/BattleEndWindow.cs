using UnityEngine;
using System.Collections;

public class BattleEndWindow : MonoBehaviour {

	public Rect windowRect;
	private bool battleIsOver;
	private string winner;
	private Texture2D victory;
	private Texture2D defeat;

	// Use this for initialization
	void Start () {
		battleIsOver = false;
		windowRect = centerRectangle(new Rect( (Screen.width / 2), (Screen.height / 2), (Screen.width / 2), (Screen.height / 2) ));
	}


	void OnGUI() {

		if (battleIsOver) {
			if (winner.Equals("player")) {
				windowRect = GUI.Window(0, windowRect, DoMyWindow, ("Battle Over"));
				// add victory pic to window
			}
			else {
				windowRect = GUI.Window(0, windowRect, DoMyWindow, ("Battle Over"));
				// add defeat pic to window
			}
		}
	}

	// Called by TurnHandler - let's this class know that the battle has ended and a window needs to be drawn
	public void drawGameEndWindow(string winner) {
		battleIsOver = true;
		this.winner = winner;
	}

	private void DoMyWindow(int windowID) {

		// Centering the button
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();		
		GUILayout.FlexibleSpace();

		if (GUILayout.Button ("Okay", GUILayout.Width(100), GUILayout.Height(50)))
			print("return to map view (yet to be implemented)");

		// More centering...
		GUILayout.EndVertical ();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
	}

	// Centers a rectangle	
	private Rect centerRectangle(Rect  r) {
		r.x = ( Screen.width - r.width ) / 2;
		r.y = ( Screen.height - r.height ) / 2;
		
		return r;
	}

}
