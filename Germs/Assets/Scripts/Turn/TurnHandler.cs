using UnityEngine;
using System.Collections;

public class TurnHandler : MonoBehaviour {


	// oltava lista kaikista otukoista ja niiden nopeuksista
	// käy otukset läpi niiden nopeuden perusteella


	// Use this for initialization
	void Start () {
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		Debug.Log("LOG: "+ units);
		Debug.Log("LOG[index]: "+ units[0]);	
		Debug.Log("MOOOI");
	}
	
	// Update is called once per frame
	void Update () {

	
	}


	public void insertionSort (ref int[] A){
		for (int i = 0; i < A.Length; i++)
		{
			int value = A[i], j = i-1;
			while (j >= 0 && A[j] > value)
			{
				A[j + 1] = A[j];
				j--;
			}
			A[j + 1] = value;
		}
	}


}
