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
				this.GetComponent<RangedAttack> ().attack (initiator, target);
			}
			if (actionType.Equals ("melee")) {
				this.GetComponent<MeleeAttack>().initiateAttack (initiator, target);
			}
			if (actionType.Equals ("rangedStun")) {
				this.GetComponent<RangedAttack>().initiateStun (initiator, target);
			}
			if (actionType.Equals ("poison")) {
				this.GetComponent<Poison>().poisonAttack(initiator, target);
			}
		}
		else { // actions that can only be performed on friendly targets
			if (actionType.Equals ("heal")) {
				this.GetComponent<Heal>().healTarget (initiator, target);
			}
			if (actionType.Equals ("detox")) {
				this.GetComponent<Dispel>().dispelPoison(initiator, target);
			}
			if (actionType.Equals ("healingPotion") || actionType.Equals ("speedPotion") || actionType.Equals ("ragePotion")) {
				this.GetComponent<PotionHandler>().usePotion(target, actionType, initiator);
			}
		}
	}
}
