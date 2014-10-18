using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_TurnLogic : MonoBehaviour {

	private GameObject currentUnit;
	private UnitStatus thisStatus;
	private SquareStatus currentSquare;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void handleTurnForGerm(GameObject unit) {
		currentUnit = unit;
		thisStatus = currentUnit.GetComponent<UnitStatus> ();
		currentSquare = currentUnit.GetComponent<UnitStatus> ().getSquare ().GetComponent<SquareStatus> ();

		// choose action (ranged, melee or special attack)
		attemptRangedAttack ();


	}

	bool attemptRangedAttack() {
		List<GameObject> targets = findValidRangedTargets ();
		if (targets.Count == 0) {
			return false; // no valid targets found for ranged attack, try another behaviour
		}
		GameObject target = pickTargetForRanged (targets);
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().setTargetedUnit (target);
		GameObject.FindGameObjectWithTag ("ActionHandler").GetComponent<RangedAttack> ().attack (currentUnit, target);

		return true; // valid target found and ranged attack executed

	}

	List<GameObject> findValidRangedTargets() {
		GameObject[] allBacs = GameObject.FindGameObjectsWithTag ("Unit");
		List<GameObject> validTargets = new List<GameObject> ();
		foreach (GameObject target in allBacs) {
			UnitStatus targetStatus = target.GetComponent<UnitStatus>();
			SquareStatus targetSquare = target.GetComponent<UnitStatus>().getSquare ().GetComponent<SquareStatus>();
			if (targetStatus.IsEnemy () != thisStatus.IsEnemy ()) { // ignore that are on the same side
				int distanceX = Mathf.Abs (targetSquare.x - currentSquare.x);
				int distanceY = Mathf.Abs (targetSquare.y - currentSquare.y);
				if (distanceX > 1 && distanceY > 1) { // if target is not within melee range, add as valid target
					validTargets.Add (target);
				}
			}
		}
		return validTargets;
	}

	GameObject pickTargetForRanged(List<GameObject> targets) {
		GameObject selectedTarget = targets [0];
		int lowestHealth = selectedTarget.GetComponent<UnitStatus> ().currentHealth;
		foreach (GameObject target in targets) {
			if (target.GetComponent<UnitStatus>().currentHealth < lowestHealth);
			selectedTarget = target;
		}
		return selectedTarget;
	}
}
