﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour {
	
	private GameObject activeUnit; // The unit whose turn is active
	private int unitListIndex;
	private List<GameObject> unitList;
	private bool battleIsOver;
	private GameStatus gameStatus;

	
	// Puts all units in to a list, sorts them by speed and gives the turn to the fastest one
	void Start () {
		gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();

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
						foreach (GameObject unit in unitList) {
							if (unit != null) {
								if (unit.GetComponent<UnitStatus>().IsUnitPoisoned()) {
									unit.GetComponent<UnitStatus>().countDownPoison();
								}
							}
						}
						trimUnitList ();
						checkIfBattleOver ();
						if (!battleIsOver) {
							initNewRound();
						}
						
					}
					// Round hasn't ended
					else {
						unitListIndex++;
						// Checks wheter the next unit is still alive so it can be given the next turn
						if (unitListIndex <= unitList.Count - 1 && unitList [unitListIndex] != null) {
							activeUnit = unitList [unitListIndex];
							activeUnit.transform.GetComponent<UnitStatus>().Select();
							if (!activeUnit.GetComponent<UnitStatus>().IsUnitStunned ()) {
								DrawSelectionCircleForUnit(activeUnit);
							}
						}
					}
				}
			}
		}
	}
	
	// Comparison thingy for Sort() - compares the units' speed
	private int SpeedCompare(GameObject x, GameObject y) {
		int result = -x.transform.GetComponent<UnitStatus>().getSpeed().CompareTo(y.transform.GetComponent<UnitStatus>().getSpeed());		
		return result;
	}
	
	// Removes the dead creatures (nulls) from the unitList
	private void trimUnitList() {
		List<GameObject> newList = new List<GameObject>();
		for (int i = 0; i < unitList.Count; i++) {
			if (unitList[i] != null) {
				newList.Add (unitList[i]);
			}
		}
		unitList = newList;
		
	}
	
	// Initializes variables for a new game round
	private void initNewRound() {
		unitListIndex = 0;
		activeUnit = unitList[unitListIndex];
		DrawSelectionCircleForUnit(activeUnit);
		activeUnit.transform.GetComponent<UnitStatus>().Select();
	}
	
	// Assigns true to bool battleIsOver, if either side is defeated
	public void checkIfBattleOver() {
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
			GameObject.FindGameObjectWithTag ("CursorHandler").GetComponent<CursorIconHandler>().drawDefaultCursor ();
			gameStatus.levelFailed();
			this.battleIsOver = true;
			GameObject.FindGameObjectWithTag("TurnHandler").transform.GetComponent<BattleEndWindow>().drawGameEndWindow("enemy");
		}
		else if (enemyIsDead) {
			GameObject.FindGameObjectWithTag ("CursorHandler").GetComponent<CursorIconHandler>().drawDefaultCursor ();
			gameStatus.levelCompleted();
			this.battleIsOver = true;
			GameObject.FindGameObjectWithTag("TurnHandler").transform.GetComponent<BattleEndWindow>().drawGameEndWindow("player");
		}
	}

	private void DrawSelectionCircleForUnit(GameObject unit) {

		string circleName = GetCircleName (unit);
		unit.transform.FindChild(circleName).gameObject.active = true;
		unit.transform.FindChild(circleName).GetComponent<BlinkingCircle>().StartBlink();		
	}

	private void RemoveSelectionCircleFromUnit(GameObject unit) {

		string circleName = GetCircleName (unit);
		unit.transform.FindChild(circleName).gameObject.active = false;
	}

	private string GetCircleName(GameObject unit) {

		if (unit.GetComponent<UnitStatus>().IsEnemy()) {
			return "enemyCircle";
		}
		return "selectionCircle";
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