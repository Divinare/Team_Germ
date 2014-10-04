using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float movementSpeed = 1;
	public Vector3 targetPosition;
	GameObject selector;

	void Start() {
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, targetPosition, Color.red);

			//constant movement
		if (targetPosition != transform.position) {
			//Debug.Log ("moving");
			//Debug.Log (targetPosition); // Nämä spämmäävät koko konsolilogin täyteen - älkää pls jättäkö näitä kommentoimatta silloin, kun pushaatte

			transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
		}

	}
	
	public void startMoving(GameObject targetSquare) {
		Debug.Log ("Moving towards " + targetSquare);
		targetPosition = targetSquare.transform.position;
		targetPosition.z = transform.position.z;
	}

}
