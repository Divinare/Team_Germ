using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	private float totalTimeBeforeDestroy;

	// Use this for initialization
	void Start () {
	
		totalTimeBeforeDestroy = 4f;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		totalTimeBeforeDestroy -= Time.deltaTime;
		if (totalTimeBeforeDestroy <= 0f) {
			Destroy (this.gameObject);
		}
	
	}
}
