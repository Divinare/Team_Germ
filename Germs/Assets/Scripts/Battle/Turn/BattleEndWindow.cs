using UnityEngine;
using System.Collections;

public class BattleEndWindow : MonoBehaviour {

	public Rect windowRect;
	private bool battleIsOver;
	private string winner;
	public Texture2D victoryIcon;
	public Texture2D defeatIcon;
	public GUISkin battleOverWindowBackground;

	// Use this for initialization
	void Start () {
		battleIsOver = false;
		windowRect = centerRectangle(new Rect( (Screen.width / 2), (Screen.height / 2), (Screen.width / 2.5f), (Screen.height / 1.5f) ));
	}


	void OnGUI() {

		if (battleIsOver) {

			GUI.skin = battleOverWindowBackground;
			GUI.Label( new Rect(0, 0, 100, 100), "Your bacteria: (to be implemented)");

			if (winner.Equals("player")) {
				windowRect = GUI.Window(0, windowRect, DoMyWindow, victoryIcon);
				// add victory pic to window
			}
			else {
				windowRect = GUI.Window(0, windowRect, DoMyWindow, defeatIcon);
				// add defeat pic to window
			}
		}
		GUI.skin = null;
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
			Application.LoadLevel ("Map");

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
