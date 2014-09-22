using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float movementSpeed;
	public Vector3 targetPosition;
	private Vector3 prevPosition;


	private Quaternion rot;
	public GameObject moveToBox;
	public GameObject destructor;

	void Start() {
		targetPosition = transform.position;
		prevPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, targetPosition, Color.red);
		
		//cancel bound to button c
		if (Input.GetKeyDown(KeyCode.C)) {
			//create unit to destroy X
			Instantiate(destructor, targetPosition, transform.rotation);
			
			//return to previous position
			targetPosition = prevPosition;
		}
			//constant movement
		if (targetPosition != transform.position) {
			
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
			
			
		}

	}
	
	void OnMouseDown() {
		//store current position as previous
		prevPosition = transform.position;
	}
	
	void OnMouseUp() {
		/*distance = Vector2.Distance(transform.position, target);
		Debug.Log (distance);
		*/

			//set target to current mouse point
			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			targetPosition.z = transform.position.z;
			
			//direction X
			Instantiate(moveToBox, targetPosition, transform.rotation);

	}
}
