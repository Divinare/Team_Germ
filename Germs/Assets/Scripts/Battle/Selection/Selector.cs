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
	public Transform selectedSquareIcon;

	private TurnHandler turnHandler;
	// for developing
	private bool debug = false;

	void Start() {
		this.turnHandler = GameObject.FindGameObjectWithTag ("TurnHandler").transform.GetComponent<TurnHandler> ();
	}

	// Update is called once per frame
	void Update () {

		if (!turnHandler.isBattleOver()) {
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
			drawSelectionSquare (hit.collider.gameObject);

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
	private void drawSelectionSquare(GameObject squareToPopUp) {

		float x = squareToPopUp.transform.position.x;
		float y = squareToPopUp.transform.position.y;
		float z = squareToPopUp.transform.position.z;

		poppedSquare = squareToPopUp;
		if (poppedSquare != null) {
			// Delete old square selection icon
			GameObject toDelete = GameObject.FindGameObjectWithTag ("SquareGfx");
			if (toDelete != null) {
				Destroy(toDelete);
			}
		}

		// Create selected square icon at selected square
		Instantiate (selectedSquareIcon, new Vector3(x,y,z -1f), Quaternion.identity);

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
