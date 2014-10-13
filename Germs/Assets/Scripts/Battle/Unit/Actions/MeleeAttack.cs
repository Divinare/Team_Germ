using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour {

	public bool goingToAttack;
	public GameObject target;
	public GameObject targetSquare; // not the square holding the melee target, but the one the Germ needs to stand on in order to be able to melee
	// Use this for initialization
	void Start () {
		goingToAttack = false;
	}
	
	// Update is called once per frame
	void Update () {

		// checks every frame to see if unit has reached a square adjacent to the target and whether an attack is primed, if yes then perform attack
		if (goingToAttack) {
			Vector3 targetPos = targetSquare.transform.position;
			targetPos.z = -1;
			if (this.gameObject.transform.position == targetPos) {
				goingToAttack = false;
				target.GetComponent<UnitStatus>().TakeDamage (this.gameObject.GetComponent<UnitStatus>().damage);
				target = null;
				targetSquare = null; // turn ends here due to code in Movement.cs
			}
		}
	}

	public void initiateAttack(GameObject activeUnit, GameObject targetGerm) {

		List<GameObject> route = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (targetGerm.GetComponent<UnitStatus>().getSquare ());
		if (route == null) {
			return; // no route to enemy found, abort attack
		}

		if (route.Count > 1) { // check if the target is in an adjacent square, if not, move to the square next to the target
			route.RemoveAt (route.Count - 1); 
			targetSquare = route[route.Count - 1];
			activeUnit.GetComponent<Movement> ().startMoving(route);
		}
		else { // if target is already at an adjacent square, set current square as the square from which to initiate melee attack
			targetSquare = activeUnit.GetComponent<UnitStatus>().getSquare ();
			activeUnit.GetComponent<UnitStatus>().Deselect ();
		}
		target = targetGerm;
		goingToAttack = true;
	}
}
