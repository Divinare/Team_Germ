using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {
	public static UnitStats unitStats;

	// Use this for initialization
	void Start () {
		if (unitStats == null) {
			DontDestroyOnLoad (gameObject);
			unitStats = this;
		} else if (unitStats != this) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
