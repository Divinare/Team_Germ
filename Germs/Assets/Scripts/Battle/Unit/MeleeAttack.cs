using UnityEngine;
using System.Collections;

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

		// if unit has reached target and attack is primed, perform attack
		if (goingToAttack) {
			Vector3 targetPos = targetSquare.transform.position;
			targetPos.z = -1;
			if (this.gameObject.transform.position == targetPos) {
				goingToAttack = false;
				target.GetComponent<UnitStatus>().TakeDamage (this.gameObject.GetComponent<UnitStatus>().damage);
				target = null;
				targetSquare = null;
			}
		}
	
	}
}
