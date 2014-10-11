using UnityEngine;
using System.Collections;

public class CursorIconHandler : MonoBehaviour {

	public Texture2D[] cursorIcons; // currently contains: 0 = melee attack
	private string currentCursor; 
	// Use this for initialization
	void Start () {
		currentCursor = "default";

	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void drawMeleeAttackCursor() {
		Cursor.SetCursor (cursorIcons[0], new Vector2(15,12), CursorMode.Auto);
		currentCursor = "melee";
	}

	public void drawRangedAttackCursor() {

	}

	public void drawDefaultCursor() {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);	
		currentCursor = "melee";
	}

	public void chooseCursorForSquare(GameObject square) {
		// square is empty, draw default cursor if it's not already drawn
		if (square.GetComponent <SquareStatus> ().getStatus ().Equals ("movable") && !currentCursor.Equals ("default")) { 
			drawDefaultCursor ();
			return;
		}
		GameObject activeUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler> ().getActiveUnit ();
		if (square.GetComponent<SquareStatus> ().getStatus ().Equals ("enemy")) {
			if (activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("melee")) {
				drawMeleeAttackCursor ();
			}
		}
	}

}
