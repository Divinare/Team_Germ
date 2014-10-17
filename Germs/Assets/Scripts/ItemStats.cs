using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemStats : MonoBehaviour {
	
	public static ItemStats itemStats;

	private int[] potionLevels = new int[]{1,1};
	private Dictionary<string, int[]> currentPotionStats = new Dictionary<string, int[]>();

	void Start () {
		if (itemStats == null) {
			DontDestroyOnLoad (gameObject);
			itemStats = this;
		} else if (itemStats != this) {
			Destroy (gameObject);
		}
		// potion name, cost, healing/damage buff amount
		currentPotionStats.Add("healPotion", new int[] {25, 10});
		currentPotionStats.Add("ragePotion", new int[] {25, 10});
		currentPotionStats.Add("speedPotion", new int[] {25, 5});
	}

	void Update () {
	
	}

	public void lvlUpHealPotions() {
		potionLevels[0]++;
		currentPotionStats ["healPotion"] [1] *= potionLevels[0];
	}
	public void lvlUpRagePotions() {
		potionLevels[1]++;
		currentPotionStats ["ragePotion"] [1] *= potionLevels[1];
	}

	public Dictionary<string, int[]> getCurrentPotionStats() {
		return currentPotionStats;
	}


}
