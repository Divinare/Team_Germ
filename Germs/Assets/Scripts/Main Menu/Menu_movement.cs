using UnityEngine;
using System.Collections;

public class Menu_movement : MonoBehaviour {
	private Vector3 mousePos;
	private float movementSpeed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (mousePos != transform.position) {
			mousePos.z = transform.position.z;
			transform.position = Vector3.MoveTowards(transform.position, mousePos, movementSpeed * Time.deltaTime);
		} else {
			//DIBBADUUU
		}

	}
}
