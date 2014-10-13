using UnityEngine;
using System.Collections;

public class MenuCreator : MonoBehaviour {

	private Vector2 menuPosition;
	private Vector2 menuButtonSize;
	public AudioSource clickSound;
	public static MenuCreator menuCreator;
	
	// Use this for initialization
	void Start () {
		if (menuCreator == null) {
			DontDestroyOnLoad (gameObject);
			menuCreator = this;
		} else if (menuCreator != this) {
			Destroy (gameObject);
		}

		menuButtonSize = new Vector2 (80, 60);
		menuPosition = new Vector2(Screen.width-menuButtonSize.x, Screen.height-menuButtonSize.y);
		clickSound = GameObject.FindGameObjectWithTag ("AudioDummy").GetComponent<AudioSource> ();
	}

	public void createMenu() {
		if (GUI.Button (new Rect (menuPosition.x, menuPosition.y, menuButtonSize.x, menuButtonSize.y), "open!")) {
			clickSound.Play ();
		}

	}

}
