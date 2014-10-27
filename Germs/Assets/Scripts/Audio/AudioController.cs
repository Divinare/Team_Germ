using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public static AudioController audioController;
	public AudioSource audioSource;
	public AudioSource[] sounds; // 0 clicksound
	public AudioSource[] music;
	// Sounds
	//public AudioSource clickSound;
	//public AudioSource battleMusic;

	// Unit specific battle sounds
	private Dictionary<string, AudioSource> meleeSounds = new Dictionary<string, AudioSource>();
	private Dictionary<string, AudioSource> rangedSounds = new Dictionary<string, AudioSource>();
	private Dictionary<string, AudioSource> movingSounds = new Dictionary<string, AudioSource>();
	// etc..

	void Start () {
		audioSource = GetComponent<AudioSource> ();

		if (audioController == null) {
			DontDestroyOnLoad (gameObject);
			audioController = this;
		} else if (audioController != this) {
			Destroy (gameObject);
		}

		//clickSound = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playBattleMusic() {
		music [0].Play ();
	}

	public void stopBattleMusic() {
		music [0].Stop ();
	}

	public void playMapMusic() {
		music [1].Play ();
	}
	public void stopMapMusic() {
		music [1].Stop ();
	}

	public void playClickSound() {
		sounds[0].Play ();
	}

	public void playMeleeSound(string unitName) {
		meleeSounds [unitName].Play ();
	}

	public void playRangedSound(string unitName) {
		rangedSounds [unitName].Play ();
	}

	public void playMovingSound(string unitName) {
		movingSounds [unitName].Play ();
	}

	// etc

}
