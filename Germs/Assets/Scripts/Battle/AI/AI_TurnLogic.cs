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

		if (attemptMeleeattack ()) {
			return; // melee attack successful, end turn
		}

		if (findTargetAndMoveTowardsIt ()) {
			return; // was able to find a target and move towards it, end turn
		}

		// no possible behaviours found, skipping turn
		thisStatus.Deselect ();
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().resetHostileTurn (); 

		// choose action (ranged, melee or special attack)
		if (attemptRangedAttack ()) {
			return; // ranged attack successful, end turn
		}



	}

	bool findTargetAndMoveTowardsIt() {
		GameObject[] allBacs = GameObject.FindGameObjectsWithTag ("Unit");
		List<GameObject> enemyBacs = new List<GameObject> ();

		foreach (GameObject bac in allBacs) {
			UnitStatus targetStatus = bac.GetComponent<UnitStatus>();
			if (targetStatus.IsEnemy () != thisStatus.IsEnemy ()) {
				enemyBacs.Add (bac);
			}
		}

		List<GameObject> movableSquares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<MovableSquaresFinder> ().getMovableSquares ();
		if (movableSquares.Count == 0) {
			return false; // no movable squares, can't move
		}
		GameObject target = findClosestTarget (enemyBacs);

		GameObject closestSquareToTarget = movableSquares [0];
		int lowestDistance = 999;
		foreach (GameObject square in movableSquares) {
			SquareStatus targetSquare = target.GetComponent<UnitStatus>().getSquare ().GetComponent<SquareStatus>();
			SquareStatus iSquare = square.GetComponent<SquareStatus>();
			int distance = Mathf.Abs (targetSquare.x - iSquare.x) + Mathf.Abs (targetSquare.y - iSquare.y);
			if (distance < lowestDistance) {
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
				List<GameObject> route = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (targetSquare, true);
				if (route != null) {
					validTargets.Add (target);
				}
			}
		}
		return validTargets;
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

	GameObject findClosestTarget(List<GameObject> targets) {
		GameObject selectedTarget = targets [0];
		int closestDistance = 999;
		foreach (GameObject target in targets) {
			SquareStatus targetSquare = target.GetComponent<UnitStatus>().getSquare ().GetComponent<SquareStatus>();
			int distance = Mathf.Abs (targetSquare.x - currentSquare.x) + Mathf.Abs (targetSquare.y - currentSquare.y);
			if (distance < closestDistance) {
				closestDistance = distance;
				selectedTarget = target;
			}
		}
		return selectedTarget;
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

	GameObject pickTargetForMelee(List<GameObject> targets) {
		GameObject selectedTarget = targets [0];
		int lowestHealth = selectedTarget.GetComponent<UnitStatus> ().currentHealth;
		foreach (GameObject target in targets) {
			if (target.GetComponent<UnitStatus>().currentHealth < lowestHealth);
			selectedTarget = target;
		}
		return selectedTarget;
	}
}
