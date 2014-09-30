using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	RaycastHit hit;
	private float raycastLength = 1000;
	string tags = "Selector, Unit, MenuItem, Matrix";
	private GameObject poppedSquare = null;
	private float poppedSquareX = 0;
	private float poppedSquareY = 0;
	private int unitMaxSize = 5;

	// for developing
	private bool debug = false;

	// Update is called once per frame
	void Update () {

		if (!GameObject.FindGameObjectWithTag("TurnHandler").transform.GetComponent<TurnHandler>().isBattleOver()) {
			changeUnitsBoxColliders(true);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (ray, out hit, raycastLength);
			//	if (Physics.Raycast (ray, out hit, raycastLength)) {
			changeUnitsBoxColliders(false);

			//Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Make a floor function to the coordinates
				//	float x = Mathf.Floor (pz.x);
				//	float y = Mathf.Floor (pz.y);
			//db ("x: " + x + " y: " + y);

			//Debug.DrawRay (ray.origin, ray.direction * raycastLength);

			// empty space
			if (hit.collider == null) {
			return;
			}
			popUpMovableSquare (hit.collider.gameObject);

			if (Input.GetMouseButtonUp (0)) {
				GameObject objectClicked = hit.collider.gameObject;

				GameObject activeUnit = findActiveUnit();
				Debug.Log (activeUnit);
				if (objectClicked.tag == "Unit") {

					unitAction(activeUnit, objectClicked);
				} 
				else if (objectClicked.tag == "MenuItem") {
					Debug.Log("menu item clicked!");
					activeUnit.GetComponent<UnitStatus> ().switchSelectedAction(objectClicked.name);
				} 
				else {
					// Clicked a square, squares have no tags
					activeUnit.GetComponent<Movement> ().startMoving(objectClicked);
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

	// pop up a square so that player can see where he can move
	private void popUpMovableSquare(GameObject squareToPopUp) {
		if (squareToPopUp == poppedSquare) {
			return;
		}
		// check if clicked something else than square, squares have no tag
		for (int i = 0; i < this.tags.Length; i++) {
				if (this.tags [i].Equals (squareToPopUp.tag)) {
						return;
				}
		}

		if (enoughSpace (squareToPopUp)) {

			float x = squareToPopUp.transform.position.x;
			float y = squareToPopUp.transform.position.y;
			squareToPopUp.transform.position = new Vector3 (x, y, -1f);

			// move the last popped square back to its original position
			if (poppedSquare != null) {
					poppedSquare.transform.position = new Vector3 (poppedSquareX, poppedSquareY, 0f);
			}
			poppedSquare = squareToPopUp;
			poppedSquareX = x;
			poppedSquareY = y;
		}

	}

	private bool enoughSpace(GameObject squareToPopUp) {
		GameObject activeUnit = findActiveUnit();
		int[,] unitMap = this.GetComponent<MovableSquareFinder> ().getUnitMap();
		int x = (int)squareToPopUp.transform.position.x;
		int y = (int)squareToPopUp.transform.position.y;

		// if target square is not empty
		if (unitMap [y, x] == 1 || unitMap [y, x] == 2) {
			return false;
		}

		return true;
	}

	public void db(string stringToDebug) {
		if (debug) {
			Debug.Log (stringToDebug);
		}
	}
	


	public GameObject findActiveUnit() {
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		for (int i = 0; i < units.Length; i++) {
			if(units[i].GetComponent<UnitStatus> ().selected) {
				return units[i];
			}
		}
		// No active units found
		Debug.Log ("Active unit not found");
		return null;
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
