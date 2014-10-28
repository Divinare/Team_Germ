using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private GameObject unit;
	private int maxHealth;
	public int curHealth;
	private float barLength;
	private float healthBarlength;
	private float bckgrndLength;
	private Texture2D healthTexture;
	private Texture2D backgroundTexture;
	private bool enemy;
	private bool valuesNeedToBeInitialized;

	void Start () {

		barLength = Screen.width / 22; // This variable makes it easy to adjust the Healthbar's size
		healthBarlength = barLength; 
		bckgrndLength = barLength + 2;
		valuesNeedToBeInitialized = true;
	}
	

	void Update () {

		if (valuesNeedToBeInitialized) {
			valuesNeedToBeInitialized = false;
			unit = gameObject;
			enemy = unit.transform.GetComponent<UnitStatus> ().IsEnemy();
			loadBarColors ();
		}
		maxHealth = unit.transform.GetComponent<UnitStatus> ().getMaxHp ();
		curHealth = unit.transform.GetComponent<UnitStatus> ().getHp ();
		AdjustcurHealth(curHealth) ; 	
	}


	public void AdjustcurHealth (int hp) {
		
		curHealth = hp;
		
		if(curHealth < 0)
			curHealth = 0;
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		
		healthBarlength = barLength * (curHealth / (float)maxHealth);
	} 

	private void loadBarColors() {

		// if code is ported to a mobile device, colors should probably be applied only once to a global texture, as this solution is pretty heavy
		Color32 backgroundColor = new Color (0.2F, 0.5F, 0.5F, 0.4F);
		backgroundTexture = new Texture2D(1, 1);
		backgroundTexture.SetPixel(0, 0, backgroundColor);
		backgroundTexture.Apply ();

		healthTexture = new Texture2D(1,1);

		if (enemy) {
			healthTexture.SetPixel(0, 0, new Color (1.0F, 0.3F, 0.3F, 0.9F));
		}
		else {
			healthTexture.SetPixel(0, 0, new Color (0.3F, 1.0F, 0.3F, 0.9F));
		}

		healthTexture.Apply ();
	}

	void OnGUI () {		

		int barHeight = 9;
		float distanceFromUnitsHead = 0.078f;

		// Healthbar background size
		Rect healthbarBackground = new Rect(0, 0, bckgrndLength, barHeight);

		// Healthbar current health size
		Rect currentHealth = new Rect(0, 0, healthBarlength, barHeight-2);


		// Where unit is located on the screen (offsets center it above the unit)
		Vector3 point = Camera.main.WorldToScreenPoint(new Vector3(
			transform.position.x,
			transform.position.y,
			transform.position.z));
		
		// Setting healthbar to correct location on Y axis
		healthbarBackground.y = Screen.height - point.y - Screen.height * distanceFromUnitsHead;
		currentHealth.y = healthbarBackground.y + 1;
		
		// Setting healthbar to correct location on X axis
		healthbarBackground.x = point.x - (0.5f * barLength);
		currentHealth.x = healthbarBackground.x + 1;

		// Drawing the bars
		GUI.DrawTexture(healthbarBackground, backgroundTexture);
		GUI.DrawTexture(currentHealth, healthTexture);
	}
}
