using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	RaycastHit hit;
	private float raycastLength = 1000;
	string tags = "Unit, MenuItem";
	private GameObject poppedSquare = null;
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Physics.Raycast (ray, out hit, raycastLength);
	//	if (Physics.Raycast (ray, out hit, raycastLength)) {

			//Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Make a floor function to the coordinates
	//		float x = Mathf.Floor (pz.x);
	//		float y = Mathf.Floor (pz.y);

	//	}
		Debug.DrawRay (ray.origin, ray.direction * raycastLength);

		if (hit.collider == null) {
			// Debug.Log ("empty space");
			return;
		}

		popUpSquare (hit.collider.gameObject);

		if (Input.GetMouseButtonUp (0)) {
			GameObject objectClicked = hit.collider.gameObject;

			GameObject activeUnit = findActiveUnit();
			Debug.Log (activeUnit);
			if (objectClicked.tag == "Unit") {
				unitAction(activeUnit, objectClicked);
			} else if (objectClicked.tag == "MenuItem") {
				Debug.Log("menu item clicked!");
				activeUnit.GetComponent<UnitStatus> ().switchSelectedAction(objectClicked.name);
			} else {
				// Clicked a square, squares have no tags
				activeUnit.GetComponent<Movement> ().startMoving(objectClicked);
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
	private void popUpSquare(GameObject go) {
		if (go == poppedSquare) {
			return;
		}
		// check if clicked something else than square, squares have no tag
		for (int i = 0; i < this.tags.Length; i++) {
			if(this.tags[i].Equals(go.tag)) {
				return;
			}
		}

		if (!enoughSpace()) {
			return;
		}

		float x = go.transform.position.x;
		float y = go.transform.position.y;
		go.transform.position = new Vector3 (x, y, -1.5f);

		// move the last popped square back to its original position
		if (poppedSquare != null) {
			poppedSquare.transform.position = new Vector3 (x, y, 0f);
		}
		poppedSquare = go;

	}

	private bool enoughSpace() {
		GameObject activeUnit = findActiveUnit();


		//widthLeftRight = activeUnit.GetComponent<UnitStatus> ().width;
	//	height





		return true;
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
