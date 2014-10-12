using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private GameObject unit;
	private int maxHealth;
	public int curHealth;
	private float barLength;
	private float healthBarlength;
	private float bckgrndLength;
	private Texture2D redTexture;
	private Texture2D greenTexture;

	void Start () {
		unit = gameObject;
		maxHealth = unit.transform.GetComponent<UnitStatus> ().maxHealth;
		curHealth = unit.transform.GetComponent<UnitStatus> ().currentHealth;

		barLength = Screen.width / 22; // This variable makes it easy to adjust the Healthbar's size
		healthBarlength = barLength; 
		bckgrndLength = barLength;

		loadBarColors ();	
	}
	

	void Update () {

		curHealth = unit.transform.GetComponent<UnitStatus>().currentHealth;
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

		redTexture = new Texture2D(1,1);
		redTexture.SetPixel(0,0,Color.red);
		redTexture.Apply ();
		
		greenTexture = new Texture2D(1,1);
		greenTexture.SetPixel(0,0,Color.green);
		greenTexture.Apply ();
	}

	void OnGUI () {		

		// Healthbar background size
		Rect healthbarBackground = new Rect(0, 0, bckgrndLength, 6);

		// Healthbar current health size
		Rect currentHealth = new Rect(0, 0, healthBarlength, 6);


		// Where unit is located on the screen (offsets center it above the unit)
		Vector3 point = Camera.main.WorldToScreenPoint(new Vector3(
			transform.position.x,
			transform.position.y,
			transform.position.z));
		
		// Setting healthbar to correct location on Y axis
		healthbarBackground.y = Screen.height - point.y - Screen.height * 0.065f;
		currentHealth.y = healthbarBackground.y;
		
		// Setting healthbar to correct location on X axis
		healthbarBackground.x = point.x - (0.5f * barLength);
		currentHealth.x = healthbarBackground.x;

		// Drawing the bars
		GUI.DrawTexture(healthbarBackground, redTexture);
		GUI.DrawTexture(currentHealth, greenTexture);
	}
}
