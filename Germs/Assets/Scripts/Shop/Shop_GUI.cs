using UnityEngine;
using System.Collections;

public class Shop_GUI : MonoBehaviour {

	//selection
	RaycastHit hit;
	RaycastHit storedHit;
	private float raycastLength = 200;

	public float gold;
	public float xp;

	public Texture2D goldIcon;
	public Texture2D xpIcon;

	public GUIStyle bigNumbers;

	//GameStateObject
	private Transform gameStatus;
	private Transform battleTracker;

	//sound
	public AudioSource clickSound;

	// Use this for initialization
	void Start () {
		gold = gameStatus.gameObject.GetComponent<GameStatus>().getGold();
		xp = gameStatus.gameObject.GetComponent<GameStatus>().getXp();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
			//gold and xp
			GUI.Box (new Rect (Screen.width - Screen.width / 6, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), xpIcon);
			GUI.Label (new Rect (Screen.width - Screen.width / 6, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), xp.ToString (), bigNumbers);
	
			GUI.Box (new Rect (Screen.width - Screen.width / 12, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), goldIcon);
			GUI.Label (new Rect (Screen.width - Screen.width / 12, Screen.height - Screen.height / 10, Screen.width / 12, Screen.height / 10), gold.ToString (), bigNumbers);

	}
}
