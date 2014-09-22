using UnityEngine;
using System.Collections;

public class SquarePosition : MonoBehaviour {

	public int x;
	public int y;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("My location is at x: " + x + " y: " + y);
	}
}
