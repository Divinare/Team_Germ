using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	public float movementSpeed = 1;
	public Vector3 targetPosition;
	private GameObject Matrix;

	public List<GameObject> route;
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

			transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
		}

	}
	
	public void startMoving(List<GameObject> newRoute) {
		this.route = newRoute;
		this.routeIndex = this.route.Count-2;
		//debugRoutes (this.route);

		targetPosition = this.route [routeIndex].transform.position;
		targetPosition.z = -1;
		routeIndex--;


		/*
		//Debug.Log ("countti oli : " + count);
		if (newRoute == null) {
			Debug.Log ("can't move, route was empty");
			return;
		}
		Debug.Log (newRoute);
	//	Debug.Log ("pituuus?!?! " + newRoute.Count);
		//Debug.Log ("routenPPP: " + GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().route.Count);
		this.route = newRoute;
		GameObject firstTargetSquare = this.route [this.route.Count-2];
		Debug.Log ("first target square: ");
		Debug.Log (firstTargetSquare);
		this.route.RemoveAt (this.route.Count - 1);
		moveToSquare (firstTargetSquare);

*/

	}

	private void checkIfReachedTargetPosition() {
		// No need to do anything if no having no route left
		if (route == null && routeIndex == 0) {
			return;
		}
	//	debugRoutes (this.route);


		GameObject currentTargetSquare = this.route [routeIndex+1];
		Vector3 currentTargetSquarePosition = currentTargetSquare.transform.position;
		currentTargetSquarePosition.z = -1;
		//Debug.Log ("next x: " + nextSquare.transform.position.z + " target x: " + targetPosition.z);
		if (currentTargetSquarePosition == targetPosition) {


			Debug.Log ("edettiiiiin");
			targetPosition = this.route [routeIndex].transform.position;
			targetPosition.z = -1;
			Debug.Log ("äks: " + targetPosition.x);
			Debug.Log ("yy: " + targetPosition.y);
			//route.RemoveAt(this.route.Count-1);
			this.routeIndex--;
			//moveToSquare ();

		}
	}

	private void moveToSquare(GameObject targetSquare) {
	//	if (targetSquare.GetComponent<SquareStatus> ().getStatus ().Equals("movable")) {
			Debug.Log ("Moving towards " + targetSquare);
			Debug.Log (targetSquare);
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
