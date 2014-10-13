using UnityEngine;
using System.Collections;

public class CursorIconHandler : MonoBehaviour {

	public Texture2D[] cursorIcons; // currently contains: 0 = melee attack, 1 = ranged attack, 2 = magic wand, 3 = heal, 4 = error
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
		Cursor.SetCursor (cursorIcons[1], new Vector2(15,12), CursorMode.Auto);
		currentCursor = "ranged";
	}

	public void drawHealCursor() {
		Cursor.SetCursor (cursorIcons[3], new Vector2(16,16), CursorMode.Auto);	
		currentCursor = "heal";
	}

	public void drawErrorCursor() {
		Cursor.SetCursor (cursorIcons[4], new Vector2(16,16), CursorMode.Auto);
		currentCursor = "error";
	}

	public void drawDefaultCursor() {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);	
		currentCursor = "default";
	}



	public void chooseCursorForSquare(GameObject square) {
		// square is empty, draw default cursor if it's not already drawn
		if (square.GetComponent <SquareStatus> ().getStatus ().Equals ("movable") && !currentCursor.Equals ("default")) { 
			drawDefaultCursor ();
			return;
		}
		GameObject activeUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler> ().getActiveUnit ();
		string activeUnitSquareStatus = activeUnit.GetComponent<UnitStatus> ().getSquare ().GetComponent<SquareStatus> ().getStatus ();
		string targetSquareStatus = square.GetComponent<SquareStatus>().getStatus();

		if (!activeUnitSquareStatus.Equals (targetSquareStatus)) {
			if (activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("melee") && !currentCursor.Equals ("melee")) {
				drawMeleeAttackCursor ();
			}
			if (activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("ranged") && !currentCursor.Equals ("ranged")) {
				drawRangedAttackCursor ();
			}
			if (activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("heal") && !currentCursor.Equals ("error")) {
				drawErrorCursor ();
			}
		}
		if (activeUnitSquareStatus.Equals (targetSquareStatus)) {
			if (activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("heal") && !currentCursor.Equals ("heal")) {
				drawHealCursor();
			}
			if (activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("melee") && !currentCursor.Equals ("error")) {
				drawErrorCursor();
			}
			if (activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("ranged") && !currentCursor.Equals ("error")) {
				drawErrorCursor();
			}
		}
	}

}
