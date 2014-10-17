using UnityEngine;
using System.Collections;

public class TrainerMenuBar : MonoBehaviour {
		
		
		public GameStatus gameStatus;
		public AudioSource clickSound;
		
		public Texture2D goldIcon;
		public Texture2D xpIcon;
		
		// Common stuff
		public Vector2 menuBarSize;
		public Vector2 menuBarPosition;
		public float menuBarDescriptionHeight;
		public Vector2 xpGoldButtonSize;
		public Vector2 shopMapTrainerButtonSize;
		public Vector2 menuButtonSize;
		public Vector2 menuPosition;
		
		
		
		void Start () {
			gameStatus = GameObject.Find ("GameStatus").GetComponent<GameStatus> ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		void OnGUI() {
			// Common GUI stuff
			menuBarSize = MenuBar.menuBar.menuBarSize;
			menuBarPosition = MenuBar.menuBar.menuBarPosition;
			menuBarDescriptionHeight = MenuBar.menuBar.menuBarDescriptionHeight;
			xpGoldButtonSize = MenuBar.menuBar.xpGoldButtonSize;
			menuButtonSize = MenuBar.menuBar.menuButtonSize;
			menuPosition = MenuBar.menuBar.menuPosition;
			shopMapTrainerButtonSize = MenuBar.menuBar.shopMapTrainerButtonSize;

			createTrainerMenu ();
		}

		public void createTrainerMenu() {
			MenuBar.menuBar.createReturnToMapButton (1);
			MenuBar.menuBar.createShopButton(2);
			MenuBar.menuBar.createXpAndGoldButtons ();
			MenuBar.menuBar.createMainMenuButton ();
		}
		
	}
