using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public static AudioController audioController;
	public AudioSource audioSource;
	public AudioClip[] audioClips;

	// Sounds
	public AudioSource clickSound;

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

		clickSound = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioSource> ();
		initSounds ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void initSounds() {

	}



	public void playClickSound() {
		clickSound.Play ();
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
