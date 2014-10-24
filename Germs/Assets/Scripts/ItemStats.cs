using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemStats : MonoBehaviour {
	
	public static ItemStats itemStats;

	private static int itemAttributes = 3;
	private static int inventorySize = 3;
	private Dictionary<string, int[]> currentItemStats = new Dictionary<string, int[]>();
	private string[,] inventoryContent = new string[inventorySize, itemAttributes+1];

	public Dictionary<string, Texture2D> itemIcons = new Dictionary<string, Texture2D>();
	
	public Texture2D empty;
	public Texture2D healPotion;
	public Texture2D ragePotion;
	public Texture2D speedPotion;

	void Start () {
		if (itemStats == null) {
			DontDestroyOnLoad (gameObject);
			itemStats = this;
		} else if (itemStats != this) {
			Destroy (gameObject);
		}
		initInventory ();
		addItemIcons ();
		initItemStats ();
	}
	
	private void initItemStats() {
		// in the int array the order is: level,  cost, effect (healing/damage buff amount)
		currentItemStats.Add("healPotion", new int[] {1, 25, 10});
		currentItemStats.Add("ragePotion", new int[] {1, 25, 10});
		currentItemStats.Add("speedPotion", new int[] {1, 25, 5});
	}

	private void initInventory() {
		for(int i = 0; i < inventorySize; i++) {
			inventoryContent[i,0] = "empty";
		}
	}

	//itemDescription.Add ("healPotion", "heals a bacteria");
	//	itemDescription.Add ("ragePotion", "Increases damage, causes a bacteria to rage");
	//	itemDescription.Add ("speedPotion", "Makes a bacteria move faster");


	private string getItemDescription(string itemName) {
		string description = "";
		int[] potionStats = currentItemStats[itemName];
		string n = itemName;
		if(n.Equals("healPotion")) {
		description += "Level " + getItemLevel(n);
		
		}

		return description;
	}
	

	public Dictionary<string, int[]> getCurrentPotionStats() {
		return currentItemStats;
	}

	public bool addToInventory(string itemName) {
		bool full = true;
		for (int i = 0; i < inventoryContent.GetLength(0); i++) {

			if(inventoryContent[i,0].Equals("empty")) {
				full = false;
			}
		}
		if (full) {
			return false;
		}

		bool added = false;
		KeyValuePair<string, int[]> entry = new KeyValuePair<string, int[]>(itemName, currentItemStats [itemName]);
		for (int i = 0; i < inventoryContent.Length; i++) {
			if(inventoryContent[i,0].Equals("empty")) {
				inventoryContent[i,0] = itemName;
				for(int j = 1; j < currentItemStats[itemName].GetLength(0); j++) {
					inventoryContent[i,j] = currentItemStats[itemName][j].ToString();
				}
				break;
			}
		}
		return true;
	}

	public void sellItem(int selectedInventoryIndex) {
		inventoryContent [selectedInventoryIndex-1, 0] = "empty";
	}

	public void levelUpItem(string key) {
		currentItemStats [key] [0]++;
		currentItemStats [key] [1] += (int)(currentItemStats [key] [1] * 0.25); // cost increase
		currentItemStats [key] [2] += (int)(currentItemStats [key] [2] * 0.25); // effect inrease
	}

	public int getItemLevel(string key) {
		return currentItemStats[key][0];
	}
	public int getItemCost(string key) {
		return currentItemStats[key][1];
	}
	public int getItemEffect(string key) {
		return currentItemStats[key][2];
	}

	private void addItemIcons() {
		itemIcons.Add ("empty", empty);
		itemIcons.Add ("healPotion", healPotion);
		itemIcons.Add ("ragePotion", ragePotion);
		itemIcons.Add ("speedPotion", speedPotion);


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


}
