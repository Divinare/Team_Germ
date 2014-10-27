using UnityEngine;
using System.Collections;

public class meleeEffect : MonoBehaviour {
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
		//this.gameObject.animation.Play("slash");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
