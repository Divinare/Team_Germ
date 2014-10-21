using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_TurnLogic : MonoBehaviour {

	private GameObject currentUnit;
	private UnitStatus thisStatus;
	private SquareStatus currentSquareStatus;
	private bool stunned;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void handleTurnForGerm(GameObject unit) {
		stunned = unit.transform.GetComponent<UnitStatus>().IsUnitStunned();
		if (!stunned) {
			currentUnit = unit;
			thisStatus = currentUnit.GetComponent<UnitStatus> ();
			currentSquareStatus = currentUnit.GetComponent<UnitStatus> ().getSquare ().GetComponent<SquareStatus> ();
			if (Random.Range (1,10) > 5) {
				if (attemptRangedAttack ()) {
					return; // ranged attack successful, end turn
				}
			}

			if (attemptMeleeattack ()) {
				return; // melee attack successful, end turn
			}
			

			if (findTargetAndMoveTowardsIt ()) {
				return; // was able to find a target and move towards it, end turn
			}
		}
		// no possible behaviours found, skipping turn
		thisStatus.Deselect ();

		// choose action (ranged, melee or special attack)
		



	}

	bool findTargetAndMoveTowardsIt() {
		GameObject[] allBacs = GameObject.FindGameObjectsWithTag ("Unit");
		List<GameObject> targetBacs = new List<GameObject> ();

		foreach (GameObject bac in allBacs) {
			UnitStatus targetStatus = bac.GetComponent<UnitStatus>();
			if (targetStatus.IsEnemy () != thisStatus.IsEnemy ()) {
				targetBacs.Add (bac);
			}
		}

		List<GameObject> movableSquares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<MovableSquaresFinder> ().getMovableSquares ();
		if (movableSquares.Count == 0) {
			return false; // no movable squares, can't move
		}
		GameObject target = findClosestTarget (targetBacs);

		GameObject closestSquareToTarget = movableSquares [0];
		double lowestDistance = 999;
		foreach (GameObject square in movableSquares) {
			GameObject targetSquare = target.GetComponent<UnitStatus>().getSquare();
			double distance = checkDistance (square, targetSquare);
			if (distance < lowestDistance) {
				lowestDistance = distance;
				closestSquareToTarget = square;
			}
		}
		List<GameObject> routeToTargetSquare = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (closestSquareToTarget, false);
		currentUnit.GetComponent<Movement> ().startMoving (routeToTargetSquare);
		return true;
	}

	bool attemptMeleeattack() {
		List<GameObject> targets = findValidMeleeTargets ();
		if (targets.Count == 0) {
			return false;
		}
		GameObject target = pickTargetForMelee (targets);
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().setTargetedUnit (target);
		GameObject.FindGameObjectWithTag ("ActionHandler").GetComponent<MeleeAttack> ().initiateAttack (currentUnit, target);
		return true;
	}

	bool attemptRangedAttack() {
		List<GameObject> targets = findValidRangedTargets ();
		if (targets.Count == 0) {
			return false; // no valid targets found for ranged attack
		}
		GameObject target = pickTargetForRanged (targets);
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().setTargetedUnit (target);
		GameObject.FindGameObjectWithTag ("ActionHandler").GetComponent<RangedAttack> ().attack (currentUnit, target);

		return true; // valid target found and ranged attack executed

	}

	List<GameObject> findValidMeleeTargets () {
		GameObject[] allBacs = GameObject.FindGameObjectsWithTag ("Unit");
		List<GameObject> validTargets = new List<GameObject> ();
		foreach (GameObject target in allBacs) {
			UnitStatus targetStatus = target.GetComponent<UnitStatus>();
			GameObject targetSquare = target.GetComponent<UnitStatus>().getSquare();
			if (targetStatus.IsEnemy () != thisStatus.IsEnemy ()) {
				if (targetIstAtMeleeRange (currentUnit, target)) { // check if target is already at melee range, if yes then it's a valid attack target
					validTargets.Add (target);
				}
				else { // if target is not at melee range, check if there is a valid route to it and if yes, add it as a valid target
					List<GameObject> route = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (targetSquare, true);
					if (route != null) {
						validTargets.Add (target);
					}
				}
			}
		}
		return validTargets;
	}

	bool targetIstAtMeleeRange(GameObject unit, GameObject target) {
		SquareStatus targetSquare = target.GetComponent<UnitStatus>().getSquare ().GetComponent<SquareStatus>();
		int distanceX = Mathf.Abs (targetSquare.x - currentSquareStatus.x);
		int distanceY = Mathf.Abs (targetSquare.y - currentSquareStatus.y);
		return (distanceX <= 1 && distanceY <= 1);
	}

	double checkDistance(GameObject from, GameObject to) {
		SquareStatus fromSquare = from.GetComponent<SquareStatus> ();
		SquareStatus toSquare = to.GetComponent<SquareStatus> ();
		int distanceX = Mathf.Abs (fromSquare.x - toSquare.x);
		int distanceY = Mathf.Abs (fromSquare.y - toSquare.y);
		double distance = Mathf.Max (distanceX, distanceY) - Mathf.Min(distanceX, distanceY) + Mathf.Min (distanceX, distanceY) * 1.5;
		return distance;
	}

	List<GameObject> findValidRangedTargets() { // finds all opposing side targets that are not in melee rnage
		GameObject[] allBacs = GameObject.FindGameObjectsWithTag ("Unit");
		List<GameObject> validTargets = new List<GameObject> ();
		foreach (GameObject target in allBacs) {
			UnitStatus targetStatus = target.GetComponent<UnitStatus>();
			if (targetStatus.IsEnemy () != thisStatus.IsEnemy ()) { 
				if (!targetIstAtMeleeRange (currentUnit, target)) { 
					validTargets.Add (target);
				}
			}
		}
		return validTargets;
	}

	GameObject findClosestTarget(List<GameObject> targets) {
		GameObject selectedTarget = targets [0];
		double closestDistance = 999;
		foreach (GameObject target in targets) {
			GameObject targetSquare = target.GetComponent<UnitStatus>().getSquare ();
			double distance = checkDistance (thisStatus.getSquare (), targetSquare);
			if (distance < closestDistance) {
				closestDistance = distance;
				selectedTarget = target;
			}
		}
		return selectedTarget;
	}

	GameObject pickTargetForRanged(List<GameObject> targets) { // currently picks lowest health target
		GameObject selectedTarget = targets [0];
		int lowestHealth = selectedTarget.GetComponent<UnitStatus> ().currentHealth;
		foreach (GameObject target in targets) {
			if (target.GetComponent<UnitStatus>().currentHealth < lowestHealth);
			selectedTarget = target;
		}
		return selectedTarget;
	}

	GameObject pickTargetForMelee(List<GameObject> targets) { // currently picks lowest health target
		GameObject selectedTarget = targets [0];
		int lowestHealth = selectedTarget.GetComponent<UnitStatus> ().currentHealth;
		foreach (GameObject target in targets) {
			if (target.GetComponent<UnitStatus>().currentHealth < lowestHealth);
			selectedTarget = target;
		}
		return selectedTarget;
	}
}
