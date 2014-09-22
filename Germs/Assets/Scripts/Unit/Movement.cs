using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float movementSpeed = 1;
	public Vector3 targetPosition;

	void Start() {
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, targetPosition, Color.red);

			//constant movement
		if (targetPosition != transform.position) {
			Debug.Log ("moving");
			Debug.Log (targetPosition);

			transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
		}

	}
	
	public void startMoving(GameObject targetSquare) {
		Debug.Log ("Moving towards " + targetSquare);
		targetPosition = targetSquare.transform.position;
		targetPosition.z = transform.position.z;
	}

}
