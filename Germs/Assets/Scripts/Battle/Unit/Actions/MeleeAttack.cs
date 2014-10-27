using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour {
	
	public bool goingToAttack;
	public GameObject meleeEffect;
	private GameObject attacker;
	private GameObject target;
	private GameObject targetSquare; // not the square holding the melee target, but the one the Germ needs to stand on in order to be able to melee
	// Use this for initialization
	void Start () {
		goingToAttack = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void finalizeAttack() {
		goingToAttack = false;
		if (target != null) {
			//meleeEffect
			float x = target.transform.position.x;
			float y = target.transform.position.y;
			float z = target.transform.position.z;
			z = -3f;
			Destroy(Instantiate(meleeEffect, new Vector3(x,y,z), Quaternion.Euler(new Vector3(270,90,0))), 15f);

			target.GetComponent<UnitStatus>().TakeDamage (attacker.GetComponent<UnitStatus>().getDmg ());
		}
		targetSquare = null; 
		attacker.GetComponent<UnitStatus>().Deselect();
		attacker = null;		
	}
	
	public GameObject getAttacker () {
		return attacker;
	}
	
	public void initiateAttack(GameObject activeUnit, GameObject targetGerm) {
		
		attacker = activeUnit;
		List<GameObject> route = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (targetGerm.GetComponent<UnitStatus>().getSquare (), true);
		if (route == null) {
			target = null;
			targetSquare = null;
			attacker = null;
			return; // no route to enemy found, abort attack
		}
		
		goingToAttack = true;
		target = targetGerm;
		if (route.Count > 1) { // check if the target is in an adjacent square, if not, move to the square next to the target
			route.RemoveAt (route.Count - 1); 
			targetSquare = route[route.Count - 1];
			activeUnit.GetComponent<Movement> ().startMoving(route); // finalizeAttack will be called from Movement.cs when movement ends
		}
		else { // if target is already at an adjacent square, set current square as the square from which to initiate melee attack
			targetSquare = activeUnit.GetComponent<UnitStatus>().getSquare ();
			finalizeAttack ();
		}
		
		
	}
}