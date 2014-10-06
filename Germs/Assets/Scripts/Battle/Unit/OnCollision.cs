using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("hai, this is bullet");	
	}
	


	void OnCollisionEnter(Collision collision) {
		Debug.Log ("omg, i'm hitting something1");
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("omg, i'm hitting something2");
		Destroy(gameObject);
	}


	}
