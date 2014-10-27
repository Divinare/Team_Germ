using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_TargetFinder : MonoBehaviour {

	private GameObject currentUnit;
	private UnitStatus thisStatus;
	private SquareStatus currentSquareStatus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void updateUnits(GameObject currentUnit, SquareStatus currentSquareStatus, UnitStatus thisStatus) {
		this.currentUnit = currentUnit;
		this.currentSquareStatus = currentSquareStatus;
		this.thisStatus = thisStatus;
	}
	
	public List<GameObject> findFriendliesWithDebuffs() {
		GameObject[] allBacs = GameObject.FindGameObjectsWithTag ("Unit");
		List<GameObject> debuffedFriendlies = new List<GameObject> ();
		foreach (GameObject target in allBacs) {
			UnitStatus targetStatus = target.GetComponent<UnitStatus>();
			if (targetStatus.IsEnemy () == thisStatus.IsEnemy ()) {
				if (targetStatus.IsUnitPoisoned() || targetStatus.IsUnitStunned ()) {
					debuffedFriendlies.Add (target);
				}
			}
		}
		return debuffedFriendlies;
	}
	
	public List<GameObject> findAllEnemies() {
		GameObject[] allBacs = GameObject.FindGameObjectsWithTag ("Unit");
		List<GameObject> enemies = new List<GameObject> ();
		foreach (GameObject target in allBacs) {
			UnitStatus targetStatus = target.GetComponent<UnitStatus>();
			if(targetStatus.IsEnemy () != thisStatus.IsEnemy ()) {
				enemies.Add (target);
			}
		}
		return enemies;
	}
	
	
	public GameObject pickRandomTarget(List<GameObject> targets) {
		return targets[Random.Range (0, targets.Count - 1)];
	}
	
	public List<GameObject> findFriendlies() {
		GameObject[] allBacs = GameObject.FindGameObjectsWithTag ("Unit");
		List<GameObject> friendlies = new List<GameObject> ();
		foreach (GameObject target in allBacs) {
			UnitStatus targetStatus = target.GetComponent<UnitStatus>();
			if(targetStatus.IsEnemy () == thisStatus.IsEnemy ()) {
				friendlies.Add (target);
			}
		}
		return friendlies;
	}
	
	public List<GameObject> findValidMeleeTargets () {
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
	
	public bool targetIstAtMeleeRange(GameObject unit, GameObject target) {
		SquareStatus targetSquare = target.GetComponent<UnitStatus>().getSquare ().GetComponent<SquareStatus>();
		int distanceX = Mathf.Abs (targetSquare.x - currentSquareStatus.x);
		int distanceY = Mathf.Abs (targetSquare.y - currentSquareStatus.y);
		return (distanceX <= 1 && distanceY <= 1);
	}
	
	public double checkDistance(GameObject from, GameObject to) {
		SquareStatus fromSquare = from.GetComponent<SquareStatus> ();
		SquareStatus toSquare = to.GetComponent<SquareStatus> ();
		int distanceX = Mathf.Abs (fromSquare.x - toSquare.x);
		int distanceY = Mathf.Abs (fromSquare.y - toSquare.y);
		double distance = Mathf.Max (distanceX, distanceY) - Mathf.Min(distanceX, distanceY) + Mathf.Min (distanceX, distanceY) * 1.5;
		return distance;
	}
	
	public List<GameObject> findValidRangedTargets() { // finds all opposing side targets that are not in melee rnage
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
	
	public List<GameObject> findValidStunTargets() { // finds all opposing targets that are not in melee range and are not stunned
		List<GameObject> validTargets = findValidRangedTargets();
		List<GameObject> alreadyStunned = new List<GameObject>();
		for (int i = 0; i < validTargets.Count; i++) {
			if (validTargets[i].GetComponent<UnitStatus>().IsUnitStunned ()) {
				alreadyStunned.Add (validTargets[i]);
			}
		}
		foreach (GameObject o in alreadyStunned) {
			validTargets.Remove (o);
		}
		return validTargets;
	}
	
	public GameObject findClosestTarget(List<GameObject> targets) {
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
	
	public GameObject findLowestHealthTarget(List<GameObject> targets) { 
		GameObject selectedTarget = targets [0];
		int lowestHealth = selectedTarget.GetComponent<UnitStatus> ().getHp ();
		foreach (GameObject target in targets) {
			if (target.GetComponent<UnitStatus>().getHp () < lowestHealth) {
				selectedTarget = target;
			}
		}
		return selectedTarget;
	}
	
	
	
	public GameObject findHighestDamageTarget(List<GameObject> targets) { 
		GameObject selectedTarget = targets [0];
		int highestDamage = selectedTarget.GetComponent<UnitStatus> ().getDmg ();
		foreach (GameObject target in targets) {
			if (target.GetComponent<UnitStatus>().getHp () > highestDamage) {
				selectedTarget = target;
			}
		}
		return selectedTarget;
	}
}
