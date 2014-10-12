using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour {

	RaycastHit hit;
	private float raycastLength = 1000;
	string tags = "Selector, Unit, MenuItem, Matrix";
	private GameObject mouseHoveredSquare;

	private int unitMaxSize = 5;
	private TurnHandler turnHandler;
	public List<GameObject> route;
	private GameObject targetedUnit;
	private bool inputLocked;

	// for developing
	private bool debug = false;
	
	void Start() {
		//this.route = null;
		inputLocked = false;
		this.turnHandler = GameObject.FindGameObjectWithTag ("TurnHandler").transform.GetComponent<TurnHandler> ();
	}

	// Update is called once per frame
	void Update () {

		if (!turnHandler.isBattleOver()) {
			//changeUnitsBoxColliders(true);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (ray, out hit, raycastLength);
			//	if (Physics.Raycast (ray, out hit, raycastLength)) {
			//changeUnitsBoxColliders(false);

			//Debug.DrawRay (ray.origin, ray.direction * raycastLength);

			// empty space
			if (hit.collider == null) {
				return;
			}

			if (hit.collider.gameObject.tag.Equals ("Square")) {
				handleMouseHover (hit.collider.gameObject);
			}
			else if (hit.collider.gameObject.tag.Equals ("Unit")) { // if hovering over unit, perform mouseover actions on the square the unit is on
				handleMouseHover (hit.collider.gameObject.GetComponent<UnitStatus>().getSquare());
			}


			if (Input.GetMouseButtonUp (0)) {
				GameObject objectClicked = hit.collider.gameObject;

				GameObject activeUnit = turnHandler.getActiveUnit();

				Debug.Log (activeUnit);
				if (objectClicked.tag == "Unit" && !inputLocked) {

					unitAction(activeUnit, objectClicked);
				} 
				else if (objectClicked.tag == "MenuItem") {
					Debug.Log("menu item clicked!");
					// activeUnit.GetComponent<UnitStatus> ().switchSelectedAction(objectClicked.name); // handled via GUI/ActivityMenu.cs
				} 
				else if (objectClicked.tag == "Square" && !inputLocked){
					// Clicked a square, check if square contains a germ and use unitaction on the target square's germ if so
					if (objectClicked.GetComponent<SquareStatus>().getObjectOnSquare () != null) {
						unitAction (activeUnit, objectClicked.GetComponent<SquareStatus>().getObjectOnSquare());
					}
					else if(this.route != null) {
						List<GameObject> tempRoute = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (hit.collider.gameObject);
						//Debug.Log ("aikaisempi countti: " + tempRoute.Count);
						activeUnit.GetComponent<Movement> ().startMoving(tempRoute);
					}
				}
			}
		}
	}

	public void lockInput() { // lock and unlock input are used by other scripts which need to lock actions while something is being performed, ie. attacks, movement etc.
		inputLocked = true;
	}

	public void unlockInput() {
		inputLocked = false;
	}

	public void resetHoveredSquare() { // used to force redraw of path and mouse cursor icon, f.ex. when changing turn
		this.mouseHoveredSquare = null;
	}


	private void unitAction(GameObject activeUnit, GameObject objectClicked) {
		Debug.Log ("Unit clicked!");
		this.targetedUnit = objectClicked; // now projectiles know what they are trying to hit
		
		string action = activeUnit.GetComponent<UnitStatus> ().selectedAction;
		if (action == "melee" && objectClicked.GetComponent<UnitStatus>().IsEnemy()) {
			Debug.Log ("Melee attack selected");
			activeUnit.GetComponent<MeleeAttack>().initiateAttack (activeUnit, objectClicked);

		} else if (action == "ranged" && objectClicked.GetComponent<UnitStatus>().IsEnemy()) {
			Debug.Log ("Ranged attack selected");
			activeUnit.GetComponent<RangedAttack> ().attack(objectClicked);
			
		} else if (action == "magic") {
			Debug.Log ("Magic attack selected");

			
			// to be implemented
			
		} else if (action == "heal") {
			Debug.Log ("Heal selected");
			activeUnit.GetComponent<Heal>().healTarget (targetedUnit);
			
			// to be implemented
			
		}
		
		// etc...
		
	}

	public GameObject GetTargetedUnit() {
		if (targetedUnit != null) {
			return targetedUnit;
		}
		return null;
	}

	public void SetTargetedUnitToNull() {
		targetedUnit = null;
	}

	// Draw a circle to a square so that player can see where he can move
	private void handleMouseHover(GameObject hoveredSquare) {
		if (hoveredSquare == null) {
			return;
		}
		
		// Only do some action if a new square has been encountered or encountering a square for the first time
		if (this.mouseHoveredSquare == hoveredSquare) {
			return;
		}
		mouseHoveredSquare = hoveredSquare;

		if(hoveredSquare.tag.Equals("Square")) {
		GameObject.FindGameObjectWithTag ("CursorHandler").GetComponent<CursorIconHandler>().chooseCursorForSquare(hoveredSquare);
		GameObject.FindGameObjectWithTag ("Drawer").GetComponent <Drawer>().handleDrawingForSquare(hoveredSquare); // draws route and square selection icon
		
		}
	}

	public void db(string stringToDebug) {
		if (debug) {
			Debug.Log (stringToDebug);
		}
	}


	// is this still needed...?
	private void changeUnitsBoxColliders(bool b) {
		GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
		for (int i = 0; i < units.Length; i++) {
			units[i].collider.enabled = b;
		}
	}




}
