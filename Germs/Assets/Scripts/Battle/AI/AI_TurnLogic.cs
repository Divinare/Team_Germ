using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_TurnLogic : MonoBehaviour {
	
	private GameObject currentUnit;
	private UnitStatus thisStatus;
	private SquareStatus currentSquareStatus;
	private AI_TargetFinder targetFinder;
	private int framesSinceInitialization;
	// Use this for initialization
	void Start () {
		targetFinder = GameObject.FindGameObjectWithTag("AIController").GetComponent<AI_TargetFinder>();
		framesSinceInitialization = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void handleTurnForGerm(GameObject unit) {
	
		// This fixes a bug where sometimes unitActions was not initialized by another script before AI attempts to fetch unitActions. The solution is to wait a few frames to ensure everything is initialized.
		// The fix looks a bit ugly, but since there is no proper system to control the order in which game objects are loaded into the scene, have to use this for now.
		if (framesSinceInitialization < 2) {
			GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector>().resetHostileTurn ();
			framesSinceInitialization++;
			return;
		}
		
		currentUnit = unit;
		thisStatus = currentUnit.GetComponent<UnitStatus> ();
		Dictionary<string, bool> unitActions = currentUnit.GetComponent<UnitStatus> ().GetUnitActions ();
		currentSquareStatus = currentUnit.GetComponent<UnitStatus> ().getSquare ().GetComponent<SquareStatus> ();
		targetFinder.updateUnits(currentUnit, currentSquareStatus, thisStatus);
		
		
		if (thisStatus.actionCooldown == 0) {
			if (unitActions["detox"]) {
				if (attemptDetox()) {
					return;
				}
			}
		
			if (unitActions["heal"]) {
				if (attemptHeal()) {
					return;
				}
			}
		
			if (unitActions["rangedStun"]) {
				if (attemptRangedStun ()) {
					return; // ramged stun successful, end turn
				}
			}
		
			if (unitActions["poison"]) {
				if (attemptPoison()) {
					return; // poison successful, end turn
				}
			}
		}
		
		if (unitActions["ranged"]) {
			if (attemptRangedAttack ()) {
				return; // ranged attack successful, end turn
			}
		}
		
		if (unitActions["melee"]) {
			if (attemptMeleeattack ()) {
				return; // melee attack successful, end turn
			}
		}
		
		if (findTargetAndMoveTowardsIt ()) {
			return; // was able to find a target and move towards it, end turn
		}
		// no possible behaviours found, skipping turn
		thisStatus.Deselect ();
		
		
		
		
	}
	
	bool attemptMeleeattack() {
		List<GameObject> targets = targetFinder.findValidMeleeTargets ();
		if (targets.Count == 0) {
			return false;
		}
		GameObject target = targetFinder.findLowestHealthTarget (targets);
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().setTargetedUnit (target);
		GameObject.FindGameObjectWithTag("ActionHandler").GetComponent<ActionHandler>().performAction (currentUnit, target, "melee");
		return true;
	}
	
	bool attemptRangedAttack() {
		List<GameObject> targets = targetFinder.findValidRangedTargets ();
		if (targets.Count == 0) {
			return false; // no valid targets found for ranged attack
		}
		GameObject target = targetFinder.findLowestHealthTarget (targets);
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().setTargetedUnit (target);
		GameObject.FindGameObjectWithTag("ActionHandler").GetComponent<ActionHandler>().performAction (currentUnit, target, "ranged");
		
		return true; // valid target found and ranged attack executed
		
	}
	
	bool attemptPoison() {
		List<GameObject> targets = targetFinder.findAllEnemies();
		if (targets.Count == 0) {
			return false;
		}
		targets = targetFinder.removeDebuffedFromList("poison", targets);
		if (targets.Count == 0) {
			return false;
		}
		GameObject target = targetFinder.findHighestDamageTarget (targets);
		GameObject.FindGameObjectWithTag("ActionHandler").GetComponent<ActionHandler>().performAction (currentUnit, target, "poison");
		return true;
	}
	
	bool attemptDetox() {
		List<GameObject> debuffedUnits = targetFinder.findFriendliesWithDebuffs();
		if (debuffedUnits.Count == 0) {
			return false;
		}
		GameObject target = targetFinder.pickRandomTarget(debuffedUnits);
		GameObject.FindGameObjectWithTag("ActionHandler").GetComponent<ActionHandler>().performAction (currentUnit, target, "detox");
		return true;
		
	}
	
	bool attemptHeal() {
		List<GameObject> friendlies = targetFinder.findFriendlies();
		if (friendlies.Count == 0) {
			return false;
		}
		GameObject lowestHealthFriendly = targetFinder.findLowestHealthTarget (friendlies);
		if (lowestHealthFriendly.GetComponent<UnitStatus>().getHp () == lowestHealthFriendly.GetComponent<UnitStatus>().getMaxHp ()) {
			return false; // no damaged friendlies found, could not heal
		}		
		GameObject.FindGameObjectWithTag("ActionHandler").GetComponent<ActionHandler>().performAction (currentUnit, lowestHealthFriendly, "heal");
		return true;
		
	}
	
	bool attemptRangedStun() {
		List<GameObject> targets = targetFinder.findValidStunTargets ();
		if (targets.Count == 0) {
			return false; // no valid targets found for ranged stun
		}
		GameObject target = targetFinder.findHighestDamageTarget (targets);
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().setTargetedUnit (target);
		GameObject.FindGameObjectWithTag("ActionHandler").GetComponent<ActionHandler>().performAction (currentUnit, target, "rangedStun");
		return true; // valid target found and ranged stun executed
		
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
		GameObject target = targetFinder.findClosestTarget (targetBacs);
		
		GameObject closestSquareToTarget = movableSquares [0];
		double lowestDistance = 999;
		foreach (GameObject square in movableSquares) {
			GameObject targetSquare = target.GetComponent<UnitStatus>().getSquare();
			double distance = targetFinder.checkDistance (square, targetSquare);
			if (distance < lowestDistance) {
				lowestDistance = distance;
				closestSquareToTarget = square;
			}
		}
		List<GameObject> routeToTargetSquare = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (closestSquareToTarget, false);
		currentUnit.GetComponent<Movement> ().startMoving (routeToTargetSquare);
		return true;
	}
	
	
	
	
	
}
