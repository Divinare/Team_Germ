using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Matrix : MonoBehaviour {

	public int matrixLength = 10;
	public int matrixWidth = 15;
	//public int squareDepth = 5;
	//public Dictionary<string, GameObject> squares = new Dictionary<string, GameObject>();

	void Start () {
		GameObject matrixParent = GameObject.FindGameObjectWithTag("Matrix");
		// Create length * width matrix of cubes
		for (int i = 0; i < matrixWidth; i++) {
			for (int j = 0; j < matrixLength; j++) {
			GameObject square = GameObject.CreatePrimitive (PrimitiveType.Cube);
			square.transform.position = new Vector3 (i, j, 0);
		//	square.name = "Square" + i + j;
		//	squares[square.name] = square;
			square.transform.parent = matrixParent.transform;
			
			//	cube.transform.localScale = Vector3 (1.25, 1.5, 1);
			}
		}
	}


	
	// Update is called once per frame
	void Update () {
		//cube.transform.position.x == 2;
	}
	
}
