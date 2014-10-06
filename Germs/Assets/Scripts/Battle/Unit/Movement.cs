using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	public float movementSpeed = 1;
	public Vector3 targetPosition;
	private GameObject Matrix;

	private List<GameObject> route;


	void Start() {
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		checkIfReachedTargetPosition ();

		Debug.DrawLine(transform.position, targetPosition, Color.red);

			//constant movement
		if (targetPosition != transform.position) {
			//Debug.Log ("moving");
			//Debug.Log (targetPosition); // Nämä spämmäävät koko konsolilogin täyteen - älkää pls jättäkö näitä kommentoimatta silloin, kun pushaatte

			transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
		}

	}
	
	public void startMoving(List<GameObject> route) {
		if (route == null) {
			Debug.Log ("can't move, route was empty");
			return;
		}
		this.route = route;
		GameObject firstTargetSquare = this.route [0];
		this.route.RemoveAt (0);
		moveToSquare (firstTargetSquare);

	}

	private void checkIfReachedTargetPosition() {
		// No need to do anything if no having no route left
		if (route == null) {
			return;
		}

		GameObject nextSquare = route [0];
		if (nextSquare.transform.position == targetPosition) {
			targetPosition = nextSquare.transform.position;
			route.RemoveAt(0);
		}
	}

	private void moveToSquare(GameObject targetSquare) {
	//	if (targetSquare.GetComponent<SquareStatus> ().getStatus ().Equals("movable")) {
			Debug.Log ("Moving towards " + targetSquare);
			targetPosition = targetSquare.transform.position;
			targetPosition.z = transform.position.z;
			this.gameObject.GetComponent<UnitStatus>().getSquare().GetComponent<SquareStatus>().setStatus ("movable", null); // clear status of currently occupied square
			targetSquare.GetComponent<SquareStatus>().setStatus ("friendly", this.gameObject); // set this object as occupying the target square
			this.gameObject.GetComponent<UnitStatus>().setSquare (targetSquare);
	//	}
	}

}
