using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemStats : MonoBehaviour {
	
	public static ItemStats itemStats;
	
	private Dictionary<string, int[]> currentPotionStats = new Dictionary<string, int[]>();
	private Dictionary<string, int[]> inventoryContent = new Dictionary<string, int[]>();


	void Start () {
		if (itemStats == null) {
			DontDestroyOnLoad (gameObject);
			itemStats = this;
		} else if (itemStats != this) {
			Destroy (gameObject);
		}
		initItemStats ();
	

	}
	
	private void initItemStats() {
		// in the int array the order is: level,  cost, effect (healing/damage buff amount)
		currentPotionStats.Add("healPotion", new int[] {1, 25, 10});
		currentPotionStats.Add("ragePotion", new int[] {1, 25, 10});
		currentPotionStats.Add("speedPotion", new int[] {1, 25, 5});
	}



	public string getItemDescription(string itemName, string type, bool currentLevelStats) {
		string description = "";

		if (type.Equals ("potion")) {
			description = getPotionDescription(itemName, currentLevelStats);
		}




		//itemDescription.Add ("healPotion", "heals a bacteria");
	//	itemDescription.Add ("ragePotion", "Increases damage, causes a bacteria to rage");
	//	itemDescription.Add ("speedPotion", "Makes a bacteria move faster");
		return description;
	}

	private string getPotionDescription(string itemName, bool currentLevelStats) {
		string description = "";
		int[] potionStats = currentPotionStats[itemName];
		string n = itemName;
		if(description.Equals("healPotion")) {
		string t = "Potion"; // type
		description += "Level " + getItemLevel(t,n);
		}

		return description;
	}

	public void lvlUpHealPotions() {
		currentPotionStats ["healPotion"] [0]++;
		currentPotionStats ["healPotion"] [1] += 5; // cost increase
		currentPotionStats ["healPotion"] [2] += 3 * currentPotionStats ["healPotion"] [0]; // heal increase

	}
	public void lvlUpRagePotions() {
		currentPotionStats ["ragePotion"] [0]++;
		currentPotionStats ["ragePotion"] [1] += 5; // cost
		currentPotionStats ["ragePotion"] [2] += 2 * currentPotionStats ["ragePotion"] [0]; // dmg
	}

	public Dictionary<string, int[]> getCurrentPotionStats() {
		return currentPotionStats;
	}

	public void addToInventory(string itemName, int[] content) {
		inventoryContent.Add (itemName, content);
	}


	public int getItemLevel(string itemType, string key) {
	if(itemType.Equals("healPotion")) {
		return currentPotionStats[key][0];
	}
		return 0;
	}
	public int getItemCost(string itemType, string key) {
		if(itemType.Equals("healPotion")) {
			return currentPotionStats[key][1];
		}
		return 0;
	}
	public int getItemEffect(string itemType, string key) {
		if(itemType.Equals("healPotion")) {
			return currentPotionStats[key][2];
		}
		return 0;
	}
}
