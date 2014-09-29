using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	
	public float distance;
	private float sensitivityDistance = -7.5f;
	private float damping = 2.5f;
	private float min = -15;
	private float max = -2;
	private Vector3 zdistance;
	
	void  Start ()
	{
		distance = -10f;
		distance = transform.localPosition.z;
	}
	void  Update ()
	{
		distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityDistance;
		distance = Mathf.Clamp(distance, min, max);
		//Camera.main.orthographicSize = distance;

	}

}
