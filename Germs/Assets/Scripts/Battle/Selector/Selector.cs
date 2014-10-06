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

	// for developing
	private bool debug = false;
	
	void Start() {
		//this.route = null;
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

			handleMouseHover (hit.collider.gameObject);

			if (Input.GetMouseButtonUp (0)) {
				GameObject objectClicked = hit.collider.gameObject;

				GameObject activeUnit = turnHandler.getActiveUnit();

				Debug.Log (activeUnit);
				if (objectClicked.tag == "Unit") {

					unitAction(activeUnit, objectClicked);
				} 
				else if (objectClicked.tag == "MenuItem") {
					Debug.Log("menu item clicked!");
					activeUnit.GetComponent<UnitStatus> ().switchSelectedAction(objectClicked.name);
				} 
				else if (objectClicked.tag == "Square"){
					// Clicked a square, squares have no tags

					//Debug.Log ("Moving taken out because of some errors");
					if(this.route != null) {
						List<GameObject> tempRoute = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (hit.collider.gameObject);
						//Debug.Log ("aikaisempi countti: " + tempRoute.Count);
						activeUnit.GetComponent<Movement> ().startMoving(tempRoute);
					}
				}
			}
		}
	}

	private void unitAction(GameObject activeUnit, GameObject objectClicked) {
		Debug.Log ("Unit clicked!");
		
		string action = activeUnit.GetComponent<UnitStatus> ().selectedAction;
		if (action == "melee") {
			Debug.Log ("Melee attack selected");
			
			// to be implemented
			
		} else if (action == "ranged") {
			Debug.Log ("Ranged attack selected");
			activeUnit.GetComponent<RangedAttack> ().attack(objectClicked);
			
		} else if (action == "magic") {
			Debug.Log ("Magic attack selected");
			
			// to be implemented
			
		} else if (action == "heal") {
			Debug.Log ("Heal selected");
			
			// to be implemented
			
		}
		
		// etc...
		
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

		int x = (int)mouseHoveredSquare.transform.position.x;
		int y = (int)mouseHoveredSquare.transform.position.y;
		GameObject[,] squares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<Matrix> ().getSquares();
		GameObject targetSquare = squares [x, y];

		if(hoveredSquare.tag.Equals("Square")) {

		// getting route for a new square, will be null if not found!
		this.route = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (targetSquare);
		
		//Debug.Log ("laitettii route: " + this.route.Count);
		// draws a route if there is one
		GameObject.FindGameObjectWithTag ("Drawer").GetComponent<Drawer> ().drawRoute (this.route);

		// draws a circle hovered square
		GameObject.FindGameObjectWithTag ("Drawer").GetComponent<Drawer> ().drawSelectionSquare (hoveredSquare);
		}
	}

	public void db(string stringToDebug) {
		if (debug) {
			Debug.Log (stringToDebug);
		}
	}

	private void changeUnitsBoxColliders(bool b) {
		GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
		for (int i = 0; i < units.Length; i++) {
			if(b) {
				units[i].collider.enabled = true;
			} else {
				units[i].collider.enabled = false;
			}
		}
	}




}
