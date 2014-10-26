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

	private float lvlUpCostFactor = 0.25f;
	private float lvlUpEffectFactor = 0.25f;

	void Start () {
		if (itemStats == null) {
			DontDestroyOnLoad (gameObject);
			itemStats = this;
		} else if (itemStats != this) {
			Destroy (gameObject);
		}
		gameStatus = GameStatus.gameStatus;

		initInventory ();

		// in the int array the order is: 0 level,  1 cost, 2 effect (healing/damage buff amount)
		currentItemStats.Add("healingPotion", new int[] {1, 25, 10});
		currentItemStats.Add("ragePotion", new int[] {1, 25, 10});
		currentItemStats.Add("speedPotion", new int[] {1, 25, 5});

		itemDescriptions.Add ("healingPotion", new string[] {"Healing Potion", "Potion", "heals a bacteria"});
		itemDescriptions.Add ("ragePotion", new string[] {"Rage Potion", "Potion", "Increases damage, causes a bacteria to rage"});
		itemDescriptions.Add ("speedPotion", new string[] {"Speed Potion", "Potion", "Makes a bacteria move faster"});

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
		bool full = true;
		for (int i = 0; i < inventoryContent.GetLength(0); i++) {

			if(inventoryContent[i,0].Equals("empty")) {
				full = false;
			}
		}
		if (full) {
			return false;
		}
		gameStatus.decreaseGold (currentItemStats [itemName][1]);

		KeyValuePair<string, int[]> entry = new KeyValuePair<string, int[]>(itemName, currentItemStats [itemName]);
		for (int i = 0; i < inventoryContent.Length; i++) {
			if(inventoryContent[i,0].Equals("empty")) {
				inventoryContent[i,0] = itemName;
				for(int j = 1; j <= currentItemStats[itemName].GetLength(0); j++) {
					inventoryContent[i,j] = currentItemStats[itemName][j-1].ToString();
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

	public void sellItem(int selectedInventoryIndex) {
		gameStatus.addGold (getValueOfItem (selectedInventoryIndex));
		inventoryContent [selectedInventoryIndex, 0] = "empty";
	}

	public int getValueOfItem(int selectedInventoryIndex) {
			int gold = int.Parse (inventoryContent [selectedInventoryIndex, 2]);
			int value = (int)(gold * 0.5);
			return value;
	}

	public void levelUpItem(string key) {
		currentItemStats [key] [0]++;
		currentItemStats [key] [1] += (int)(currentItemStats [key] [1] * lvlUpCostFactor); // cost increase
		currentItemStats [key] [2] += (int)(currentItemStats [key] [2] * lvlUpEffectFactor); // effect inrease
	}

	public int getItemLevel(string key, int selectedInventoryIndex) {
		if (selectedInventoryIndex != -1) {
			return int.Parse(inventoryContent[selectedInventoryIndex, 1]);
		} else {
			return currentItemStats [key] [0];
		}
	}
	public int getItemCost(string key) {
		return currentItemStats[key][1];
	}
	public int getItemEffect(string key, int selectedInventoryIndex) {
		if (selectedInventoryIndex != -1) {
			return int.Parse(inventoryContent[selectedInventoryIndex, 3]);
		}
		return currentItemStats[key][2];
	}

	public Texture2D getItemIcon(string key) {
		return itemIcons [key];
	}

	public string[,] getInventoryContent() {
		return inventoryContent;
	}

	public int getInventorySize() {
		return inventorySize;
	}

	public string getItemName(string key) {
		return itemDescriptions [key] [0];
	}
	public string getItemType(string key) {
		return itemDescriptions [key] [1];
	}

	public string getItemDescription(string key) {
		return itemDescriptions[key][2];
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
