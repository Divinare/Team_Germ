using UnityEngine;
using System.Collections;

public class ActionHandler : MonoBehaviour {


	public void performAction(GameObject initiator, GameObject target, string actionType) {

		bool targetIsHostile;
		if (initiator.GetComponent<UnitStatus> ().IsEnemy () == target.GetComponent<UnitStatus> ().IsEnemy ()) {
			targetIsHostile = false;
		} else {
			targetIsHostile = true;
		}
		if (targetIsHostile) { // actions that can only be performed on hostile targets
			if (actionType.Equals ("ranged")) {
				Battlelog (initiator, target, "shoots a noxious slimeball at " + GetUnitName(target) + "!");
				this.GetComponent<RangedAttack> ().attack (initiator, target);
			}
			if (actionType.Equals ("melee")) {
				if (this.GetComponent<MeleeAttack>().initiateAttack (initiator, target)) {
					Battlelog (initiator, target, "sinks its gelatinous fangs deep into " + GetUnitName(target) + "'s RNA!");
				}
			}
			if (actionType.Equals ("rangedStun")) {
				Battlelog (initiator, target, "sends a sphere of stunning lightning towards " + GetUnitName(target) + "!");
				this.GetComponent<RangedAttack>().initiateStun (initiator, target);
			}
			if (actionType.Equals ("poison")) {
				Battlelog (initiator, target, "releases a cloud of toxic fumes and directs it at " + GetUnitName(target) + "!");
				this.GetComponent<Poison>().poisonAttack(initiator, target);
			}
		}
		else { // actions that can only be performed on friendly targets
			if (actionType.Equals ("heal")) {
				Battlelog (initiator, target, "reaches its healing flagella towards " + GetTargetName(initiator, target) + "'s wounds!");
				this.GetComponent<Heal>().healTarget (initiator, target);
			}
			if (actionType.Equals ("detox")) {
				Battlelog (initiator, target, "boldly sucks away all harmful poison that's affecting " + GetTargetName(initiator, target) + "'s cell wall!");
				this.GetComponent<Dispel>().dispelPoison(initiator, target);
			}
			if (actionType.Equals ("healingPotion") || actionType.Equals ("speedPotion") || actionType.Equals ("ragePotion")) {
				this.GetComponent<PotionHandler>().usePotion(target, actionType, initiator);
			}
		}
	}

	private string GetTargetName(GameObject initiator, GameObject target) {
		string unitName;
		if (target == initiator) {
			return "it";
		}
		else {
			return GetUnitName(initiator);
		}
	}

	private void Battlelog(GameObject initiator, GameObject target,  string txt) {
		GameObject.FindGameObjectWithTag ("Drawer").GetComponent<BattleMenuBar> ().addToBattleLog (GetUnitName (initiator) + " " + txt);
	}

	private string GetUnitName(GameObject who) {
		return who.GetComponent<UnitStatus> ().getUnitName ();
	}
}
