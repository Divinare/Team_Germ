using UnityEngine;
using System.Collections;

public class SquareStatus : MonoBehaviour {

	private int squareStatus;

	// Use this for initialization
	void Start () {
		squareStatus = 0;
	}

	public void setStatus(int status) {
		squareStatus = status;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
