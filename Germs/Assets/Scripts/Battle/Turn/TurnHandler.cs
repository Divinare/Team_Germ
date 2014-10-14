using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour {
	
	private GameObject activeUnit; // The unit whose turn is active
	private int unitListIndex;
	private List<GameObject> unitList;
	private bool battleIsOver;

	
	// Puts all units in to a list, sorts them by speed and gives the turn to the fastest one
	void Start () {
		battleIsOver = false;
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		unitList = new List<GameObject>(units);
		unitList.Sort(SpeedCompare);
		initNewRound ();		
	}
	
	
	void Update () {
		
		if (!battleIsOver) {
			
			// Checks whether the selected unit's turn has ended
			if (!activeUnit.transform.GetComponent<UnitStatus>().IsSelected()) {

				RemoveSelectionCircleFromUnit(activeUnit);

				checkIfBattleOver ();
				if (!battleIsOver) {
					
					// Checks whether round has ended
					if (unitListIndex == unitList.Count - 1) {
						trimUnitList ();						
						initNewRound();
					}
					// Round hasn't ended
					else {
						unitListIndex++;
						// Checks wheter the next unit is still alive so it can be given the next turn
						if (unitListIndex <= unitList.Count - 1 && unitList [unitListIndex] != null) {
							activeUnit = unitList [unitListIndex];
							activeUnit.transform.GetComponent<UnitStatus>().Select();
							DrawSelectionCircleForUnit(activeUnit);
						}
					}
				}
			}
		}
	}
	
	// Comparison thingy for Sort() - compares the units' speed
	private int SpeedCompare(GameObject x, GameObject y) {
		int result = -x.transform.GetComponent<UnitStatus>().speed.CompareTo(y.transform.GetComponent<UnitStatus>().speed);		
		return result;
	}
	
	// Removes the dead creatures (nulls) from the unitList
	private void trimUnitList() {
		for (int i = 0; i < unitList.Count; i++) {
			if (unitList[i] == null) {
				unitList.RemoveAt(i);
			}
		}
	}
	
	// Initializes variables for a new game round
	private void initNewRound() {
		unitListIndex = 0;
		activeUnit = unitList[unitListIndex];
		DrawSelectionCircleForUnit(activeUnit);
		activeUnit.transform.GetComponent<UnitStatus>().Select();
	}
	
	// Assigns true to bool battleIsOver, if either side is defeated
	private void checkIfBattleOver() {
		bool playerIsDead = true, enemyIsDead = true;
		for (int i = 0; i < unitList.Count; i++) {
			// If the enemy side has unit(s) alive
			if (unitList[i] != null && unitList[i].transform.GetComponent<UnitStatus>().enemy) {
				enemyIsDead = false;
			}
			// If the player side has unit(s) alive
			else if (unitList[i] != null && !unitList[i].transform.GetComponent<UnitStatus>().enemy) {
				playerIsDead = false;
			}
		}
		if (playerIsDead) {
			Debug.Log("BATTLE IS OVER, the enemy side has triumphed!");
			this.battleIsOver = true;
			GameObject.FindGameObjectWithTag("TurnHandler").transform.GetComponent<BattleEndWindow>().drawGameEndWindow("enemy");
		}
		else if (enemyIsDead) {
			Debug.Log("BATTLE IS OVER, the player is very victorious! Much win! So wow!");
			this.battleIsOver = true;
			GameObject.FindGameObjectWithTag("TurnHandler").transform.GetComponent<BattleEndWindow>().drawGameEndWindow("player");
		}
	}

	private void DrawSelectionCircleForUnit(GameObject unit) {
		if (unit.GetComponent<UnitStatus>().IsEnemy()) {
			unit.transform.FindChild("enemyCircle").gameObject.active = true;
			unit.transform.FindChild("enemyCircle").GetComponent<BlinkingCircle>().enabled = true;
		}
		else {
			unit.transform.FindChild("selectionCircle").gameObject.active = true;
			unit.transform.FindChild("selectionCircle").GetComponent<BlinkingCircle>().enabled = true;
		}
	}

	private void RemoveSelectionCircleFromUnit(GameObject unit) {
		if (unit.GetComponent<UnitStatus>().IsEnemy()) {
			unit.transform.FindChild("enemyCircle").gameObject.active = false;
			unit.transform.FindChild("enemyCircle").GetComponent<BlinkingCircle>().enabled = false;

		}
		else {	
			unit.transform.FindChild("selectionCircle").gameObject.active = false;
			unit.transform.FindChild("selectionCircle").GetComponent<BlinkingCircle>().enabled = false;
		}
	}


	
	// The unit whose turn it is
	public GameObject getActiveUnit() {
		return this.activeUnit;
	}
	
	// True if either side has been defeated
	public bool isBattleOver() {
		return battleIsOver;
	}
	
	
}