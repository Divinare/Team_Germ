using UnityEngine;
using System.Collections;

public class CursorIconHandler : MonoBehaviour {

	public Texture2D[] cursorIcons; // currently contains: 0 = melee attack, 1 = ranged attack, 2 = magic wand, 3 = heal, 4 = error, 5 = stun, 6= poison, 7 = detox
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

	public void drawRangedStunCursor() {
		Cursor.SetCursor (cursorIcons[5], new Vector2(16,16), CursorMode.Auto);
		currentCursor = "rangedStun";
	}

	public void drawPoisonCursor() {
		Cursor.SetCursor (cursorIcons[6], new Vector2(16,16), CursorMode.Auto);
		currentCursor = "poison";
	}
	
	public void drawDetoxCursor() {
		Cursor.SetCursor (cursorIcons[7], new Vector2(16,16), CursorMode.Auto);
		currentCursor = "detox";
	}

	public void drawDefaultCursor() {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);	
		currentCursor = "default";
	}



	public void chooseCursorForSquare(GameObject square) {
		// square is empty, draw default cursor if it's not already drawn
		if (square.GetComponent <SquareStatus> ().getStatus ().Equals ("movable")) { 
			drawDefaultCursor ();
			return;
		}
		GameObject activeUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler> ().getActiveUnit ();
		string activeUnitSquareStatus = activeUnit.GetComponent<UnitStatus> ().getSquare ().GetComponent<SquareStatus> ().getStatus ();
		string targetSquareStatus = square.GetComponent<SquareStatus>().getStatus();
		string currentAction = activeUnit.GetComponent<UnitStatus> ().selectedAction;

		if (!activeUnitSquareStatus.Equals (targetSquareStatus)) {
			if (currentAction.Equals ("melee") && !currentCursor.Equals ("melee")) {
				drawMeleeAttackCursor ();
				return;
			}
			if (currentAction.Equals ("ranged") && !currentCursor.Equals ("ranged")) {
				drawRangedAttackCursor ();
				return;
			}
			if (currentAction.Equals ("heal") && !currentCursor.Equals ("error")) {
				drawErrorCursor ();
				return;
			}
			if (currentAction.Equals ("poison") && !currentCursor.Equals ("poison")) {
				drawPoisonCursor ();
				return;
			}
			if (currentAction.Equals ("detox") && !currentCursor.Equals ("error")) {
				drawErrorCursor();
				return;
			}
			if (currentAction.Equals ("rangedStun") && !currentCursor.Equals ("rangedStun")) {
				drawRangedStunCursor ();
				return;
			}
		}
		if (activeUnitSquareStatus.Equals (targetSquareStatus)) {
			if (currentAction.Equals ("detox") && !currentCursor.Equals ("detox")) {
				drawDetoxCursor();
				return;
			}
			if (currentAction.Equals ("heal") && !currentCursor.Equals ("heal")) {
				drawHealCursor();
				return;
			}
			if (currentAction.Equals ("melee") && !currentCursor.Equals ("error")) {
				drawErrorCursor();
				return;
			}
			if (currentAction.Equals ("ranged") && !currentCursor.Equals ("error")) {
				drawErrorCursor();
				return;
			}
			if (currentAction.Equals ("poison") && !currentCursor.Equals ("error")) {
				drawErrorCursor();
				return;
			}
			if (currentAction.Equals ("rangedStun") && !currentCursor.Equals ("error")) {
				drawErrorCursor();
				return;
			}
		}
	}

}
