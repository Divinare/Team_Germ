using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemStats : MonoBehaviour {
	
	public static ItemStats itemStats;
	private GameStatus gameStatus;

	private static int itemAttributes = 3;
	private static int inventorySize = 3;
	private Dictionary<string, int[]> currentItemStats = new Dictionary<string, int[]>();
	private string[,] inventoryContent = new string[inventorySize, itemAttributes+1];

	private Dictionary<string, Texture2D> itemIcons = new Dictionary<string, Texture2D>();

	private Dictionary<string, string[]> itemDescriptions = new Dictionary<string, string[]>();

	public Texture2D empty;
	public Texture2D healingPotion;
	public Texture2D ragePotion;
	public Texture2D speedPotion;

	private float lvlUpCostFactor = 0.15f;
	private float lvlUpEffectFactor = 0.25f;
	private float lvlUpUpgradeCostFactor = 0.25f;

	void Start () {
		if (itemStats == null) {
			DontDestroyOnLoad (gameObject);
			itemStats = this;
		} else if (itemStats != this) {
			Destroy (gameObject);
		}
		gameStatus = GameStatus.gameStatus;

		initInventory ();

		// in the int array the order is: 0: itemUpgradeCost, 1 level,  2 cost, 3 effect (healing/damage buff amount)
		currentItemStats.Add("healingPotion", new int[] {50, 1, 25, 10});
		currentItemStats.Add("ragePotion", new int[] {50, 1, 25, 10});
		currentItemStats.Add("speedPotion", new int[] {50, 1, 25, 5});

		itemDescriptions.Add ("healingPotion", new string[] {"Healing Potion", "Potion", "Heals a bacterium"});
		itemDescriptions.Add ("ragePotion", new string[] {"Rage Potion", "Potion", "Increases damage, causes a bacterium to rage"});
		itemDescriptions.Add ("speedPotion", new string[] {"Speed Potion", "Potion", "Makes a bacterium to move faster"});

		itemIcons.Add ("empty", empty);
		itemIcons.Add ("healingPotion", healingPotion);
		itemIcons.Add ("ragePotion", ragePotion);
		itemIcons.Add ("speedPotion", speedPotion);
	}
	


	private void initInventory() {
		for(int i = 0; i < inventorySize; i++) {
			inventoryContent[i,0] = "empty";
		}
	}

	public bool buyItem(string itemName) {
		int cost = currentItemStats [itemName] [2];
		if (gameStatus.getGold() - cost < 0) {
			return false; // Not enough gold
		}

		bool full = true;
		for (int i = 0; i < inventoryContent.GetLength(0); i++) {

			if(inventoryContent[i,0].Equals("empty")) {
				full = false;
			}
		}
		if (full) {
			return false;
		}
		gameStatus.decreaseGold (currentItemStats [itemName][2]);

		KeyValuePair<string, int[]> entry = new KeyValuePair<string, int[]>(itemName, currentItemStats [itemName]);
		for (int i = 0; i < inventoryContent.Length; i++) {
			if(inventoryContent[i,0].Equals("empty")) {
				inventoryContent[i,0] = itemName;
				for(int j = 1; j <= currentItemStats[itemName].GetLength(0)-1; j++) {
					inventoryContent[i,j] = currentItemStats[itemName][j].ToString();
				}
				break;
			}
		}
		return true;
	}

	public int useItem(string itemName, int selectedItemIndex) {
		string itemType = itemDescriptions [itemName] [1];
		if (itemType.Equals ("Potion")) {
			int effect = int.Parse(inventoryContent[selectedItemIndex,3]);
			inventoryContent[selectedItemIndex,0] = "empty";
			return effect;
		}
		return 0;
	}

	public bool sellItem(int selectedInventoryIndex) {
		if (inventoryContent [selectedInventoryIndex, 0].Equals ("empty")) {
			return false;
		}
		gameStatus.addGold (getValueOfItem (selectedInventoryIndex));
		inventoryContent [selectedInventoryIndex, 0] = "empty";
		return true;
	}

	public int getValueOfItem(int selectedInventoryIndex) {
			int gold = int.Parse (inventoryContent [selectedInventoryIndex, 2]);
			int value = (int)(gold * 0.5);
			return value;
	}

	public bool levelUpItem(string itemName) {
		int upgradeCost = currentItemStats [itemName] [0];
		if (gameStatus.getGold () - upgradeCost < 0) {
			return false; // not enough gold
		}
		gameStatus.decreaseGold (currentItemStats [itemName][0]);
		currentItemStats [itemName] [0] += (int)(currentItemStats [itemName] [0] * lvlUpUpgradeCostFactor); // upgrade cost increase
		currentItemStats [itemName] [1]++; // level increase
		currentItemStats [itemName] [2] += (int)(currentItemStats [itemName] [2] * lvlUpCostFactor); // cost increase
		currentItemStats [itemName] [3] += (int)(currentItemStats [itemName] [3] * lvlUpEffectFactor); // effect inrease
		return true;
	}

	public int getItemUpgradeCost(string itemName) {
		return currentItemStats [itemName] [0];
	}

	public int getItemLevel(string itemName, int selectedInventoryIndex) {
		if (selectedInventoryIndex != -1) {
			return int.Parse(inventoryContent[selectedInventoryIndex, 1]);
		} else {
			return currentItemStats [itemName] [1];
		}
	}
	public int getItemCost(string itemName) {
		return currentItemStats[itemName][2];
	}
	public int getItemEffect(string key, int selectedInventoryIndex) {
		if (selectedInventoryIndex != -1) {
			return int.Parse(inventoryContent[selectedInventoryIndex, 3]);
		}
		return currentItemStats[key][3];
	}

	public Texture2D getItemIcon(string itemName) {
		return itemIcons [itemName];
	}

	public string[,] getInventoryContent() {
		return inventoryContent;
	}

	public int getInventorySize() {
		return inventorySize;
	}

	public string getItemName(string itemName) {
		return itemDescriptions [itemName] [0];
	}
	public string getItemType(string itemName) {
		return itemDescriptions [itemName] [1];
	}
	public string getItemDescription(string itemName) {
		return itemDescriptions[itemName][2];
	}

	public Dictionary<string, int[]> getCurrenItemStats() {
		return currentItemStats;
	}

	public float getLvlUpCostFactor() {
		return lvlUpCostFactor;
	}
	public float getLvlUpEffectFactor() {
		return lvlUpEffectFactor;
	}

}
