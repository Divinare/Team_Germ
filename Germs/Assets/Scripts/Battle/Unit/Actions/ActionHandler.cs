using UnityEngine;
using System.Collections;

public class ActionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void performAction(GameObject initiator, GameObject target, string actionType) {
		bool targetIsHostile;
		if (initiator.GetComponent<UnitStatus> ().IsEnemy () == target.GetComponent<UnitStatus> ().IsEnemy ()) {
			targetIsHostile = false;
		} else {
			targetIsHostile = true;
		}
		if (targetIsHostile) { // actions that can only be performed on hostile targets
			if (actionType.Equals ("ranged")) {
				this.GetComponent<RangedAttack> ().attack (initiator, target);
			}
			if (actionType.Equals ("melee")) {
				this.GetComponent<MeleeAttack>().initiateAttack (initiator, target);

			}
		}
		else { // actions that can only be performed on friendly targets
			if (actionType.Equals ("heal")) {
				this.GetComponent<Heal>().healTarget (initiator, target);
			}
		}
	}



}
