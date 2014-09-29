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
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (ray, out hit, raycastLength);
			//	if (Physics.Raycast (ray, out hit, raycastLength)) {

			//Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Make a floor function to the coordinates
			//		float x = Mathf.Floor (pz.x);
			//		float y = Mathf.Floor (pz.y);

			//	}
			//Debug.DrawRay (ray.origin, ray.direction * raycastLength);
			if (hit.collider == null) {
				// Debug.Log ("empty space");
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
	//	int[,] unitSpace = getUnitSpace (x, y);
		//Debug.Log ("x " + x);
		//Debug.Log ("y " + y);

		// if target square is not empty
		if (unitMap [y, x] == 1) {
			return false;
		}

		int up = activeUnit.GetComponent<UnitStatus> ().heightUp;
		int down = activeUnit.GetComponent<UnitStatus> ().heightDown;
		int left = activeUnit.GetComponent<UnitStatus> ().widthLeft;
		int right = activeUnit.GetComponent<UnitStatus> ().widthRight;
		db ("up" + up);
		db ("left" + left);

		/* check aroundings of target square */

		for (int l = 1; l < left+1; l++) {
			if(!spaceAtSquare(x-l, y, unitMap)) {
				db ("left false");
				return false;
			}
		}
		
		for (int r = 1; r < right+1; r++) {
			if(!spaceAtSquare(x+r, y, unitMap)) {
				db ("right false");
				return false;
			}
		}

		// up left & up right
		for (int u = 1; u < up+1; u++) {

			if(!spaceAtSquare (x, y-u, unitMap)) {
				db ("up false");
				return false;
			}
			for (int l = 1; l < left+1; l++) {
				if(!spaceAtSquare (x+l, y-u, unitMap)) {
					db ("up left false");
					return false;
				}
			}
			for (int r = 1; r < right+1; r++) {
				if(!spaceAtSquare (x-r, y-u, unitMap)) {
					db ("up right false");
					return false;
				}

			}
		}

		// down left & down right
		for (int d = 1; d < down+1; d++) {
			
			if(!spaceAtSquare (x, y+d, unitMap)) {
				db ("down false");
				return false;
			}
			for (int l = 1; l < left+1; l++) {
				if(!spaceAtSquare (x+l, y+d, unitMap)) {
					db ("down left false");
					return false;
				}
			}
			for (int r = 1; r < right+1; r++) {
				if(!spaceAtSquare (x-r, y+d, unitMap)) {
					db ("down right false");
					return false;
				}
				
			}
		}



		return true;
	}

	private bool spaceAtSquare(int x, int y, int[,] unitMap) {
		int matrixWidth = this.GetComponent<MovableSquareFinder> ().matrixWidth;
		int matrixHeight = this.GetComponent<MovableSquareFinder> ().matrixHeight;

		if (x <= 0 || x >= matrixWidth) {
			db ("x pieni/suuri " + x);
			return false;
		}
		if (y <= 0 || y >= matrixHeight) {
			db ("y pieni/suuri " + y );
			return false;
		}
		Debug.Log ("x " + x + " y " + y);
		if(unitMap[y,x] == 1) {
			db ("no space");
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




}
