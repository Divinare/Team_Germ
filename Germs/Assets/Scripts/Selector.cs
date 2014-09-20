using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	RaycastHit hit;
	private float raycastLength = 1000;

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, raycastLength)) {
			Debug.Log (hit.collider.name);


			Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Make a floor function to the coordinates
			Debug.Log ("10.6" + Mathf.Floor (10.6f));
			Debug.Log ("10.3" + Mathf.Floor (10.3f));
			float x = Mathf.Floor (pz.x);
			float y = Mathf.Floor (pz.y);
			Debug.Log ("X: " + x + " Y: " + y);

		
		}
		Debug.DrawRay (ray.origin, ray.direction * raycastLength);
	}
}
