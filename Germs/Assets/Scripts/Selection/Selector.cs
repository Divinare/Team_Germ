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
		//	Debug.Log ("X: " + x + " Y: " + y + " row: " + hit.collider.name);

		
		}
		Debug.DrawRay (ray.origin, ray.direction * raycastLength);

		if (Input.GetMouseButtonUp (0)) {

			//this.GetComponent<Action> ().setTargetSquare(hit.collider.gameObject);
			Debug.Log("mouse pressed!");

		}

	}

	public GameObject findActiveUnit() {
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		for (int i = 0; i < units.Length; i++) {
			if(units[i].GetComponent<UnitStatus> ().selected) {
				return units[i];
			}
		}
		return null;
	}




}
