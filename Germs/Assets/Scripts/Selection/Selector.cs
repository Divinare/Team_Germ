using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	RaycastHit hit;
	private float raycastLength = 1000;
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, raycastLength)) {

			Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Make a floor function to the coordinates
			float x = Mathf.Floor (pz.x);
			float y = Mathf.Floor (pz.y);

		}
		Debug.DrawRay (ray.origin, ray.direction * raycastLength);

		if (Input.GetMouseButtonUp (0)) {
			if (hit.collider == null) {
				Debug.Log ("clicked empty space");
				return;
			}
			GameObject objectClicked = hit.collider.gameObject;
			//this.GetComponent<Action> ().setTargetSquare(hit.collider.gameObject);
			Debug.Log (hit.collider.gameObject);
			Debug.Log("mouse pressed!");

			GameObject activeUnit = findActiveUnit();
			Debug.Log (activeUnit);
			if (objectClicked.tag == "Unit") {
				unitAction(activeUnit);
			} else if (objectClicked.tag == "Square") {
				activeUnit.GetComponent<Movement> ().startMoving(objectClicked);
			}

		}
	}

	private void unitAction(GameObject activeUnit) {
		Debug.Log ("Unit clicked!");
		
		string action = activeUnit.GetComponent<UnitStatus> ().selectedAction;
		if (action == "melee") {
			Debug.Log ("Melee attack selected");
			
			// to be implemented
			
		} else if (action == "ranged") {
			Debug.Log ("Ranged attack selected");
			
			// to be implemented
			
		} else if (action == "magic") {
			Debug.Log ("Magic attack selected");
			
			// to be implemented
			
		} else if (action == "heal") {
			Debug.Log ("Heal selected");
			
			// to be implemented
			
		}
		
		// etc...
		
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
