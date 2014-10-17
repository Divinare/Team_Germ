using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testAnimationScript : MonoBehaviour {
	private Animator animator;
	//private int health;
	public int healthi;
	public int maxHealth;

	public bool dropHp;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		healthi = 100;
		maxHealth = 100;
		/*
		foreach (AnimationState clip in animation) {
			Debug.Log (clip.name);
			animations.Add(clip.name);
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
		if (healthi/maxHealth < 0.50) {
			animator.SetInteger ("health", 1);
		} 
		else if (healthi/maxHealth >= 0.5) {
			animator.SetInteger ("health", 3);
		}
	}

	void OnGUI() {
		if (GUI.Button (new Rect(25,25,25,25), "")) {
				healthi -= 25;
				animator.SetTrigger("takeDamage");
		}
		if (GUI.Button (new Rect(100,100,25,25), "")) {
			healthi += 25;
			animator.SetTrigger("takeDamage");
		}
	}
}
