using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RouteFinder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public List<GameObject> getRoute(GameObject targetSquare) {
		List<GameObject> route = new List<GameObject> ();


		return route;
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
