using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	private float movementSpeed = 1;
	private Vector3 targetPosition;
	private GameObject Matrix;

	private List<GameObject> route;
	private int routeIndex;

	void Start() {
		targetPosition = transform.position;
		this.route = null;
		this.routeIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//checkIfReachedTargetPosition ();

		Debug.DrawLine(transform.position, targetPosition, Color.red);

			//constant movement
		if (targetPosition != transform.position) {
				//Debug.Log ("moving");
				//Debug.Log (targetPosition); // Nämä spämmäävät koko konsolilogin täyteen - älkää pls jättäkö näitä kommentoimatta silloin, kun pushaatte

				transform.position = Vector3.MoveTowards (transform.position, targetPosition, movementSpeed * Time.deltaTime);
		} else {
			if (this.route != null) {
				if (this.route.Count > 0) {
					if(route[0] == null) {
						this.route = null;
					} else {
						moveToSquare (route [0]);
						this.route.RemoveAt(0);
					}
				}

			}
		}

	}
	
	public void startMoving(List<GameObject> newRoute) {
		this.route = newRoute;

		moveToSquare (route [0]);
		this.route.RemoveAt(0);
	

	}

	private void moveToSquare(GameObject targetSquare) {
			Debug.Log ("Moving towards " + targetSquare);
			targetPosition = targetSquare.transform.position;
			targetPosition.z = -1;
			this.gameObject.GetComponent<UnitStatus>().getSquare().GetComponent<SquareStatus>().setStatus ("movable", null); // clear status of currently occupied square
			targetSquare.GetComponent<SquareStatus>().setStatus ("friendly", this.gameObject); // set this object as occupying the target square
			this.gameObject.GetComponent<UnitStatus>().setSquare (targetSquare);
	//	}
	}

	private void debugRoutes(List<GameObject> newRoute) {
		Debug.Log ("logataan kaikki routet");
		for(int i = 0; i < newRoute.Count-1; i++) {
			Debug.Log ("x: " + newRoute[i].transform.position.x + " y: " + newRoute[i].transform.position.y);
		}
	}

}
