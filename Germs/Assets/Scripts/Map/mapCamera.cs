using UnityEngine;
using System.Collections;

public class mapCamera : MonoBehaviour {
	public GameObject bg;
	public Vector3 minBounds;
	public Vector3 maxBounds;
	public Vector3 mousePos;
	public Vector3 camPos;

	public float cameraSpeed = 0.5F;

	// Use this for initialization
	void Start () {
		//bg unravel
		var minX = bg.renderer.bounds.min.x;
		var minY = bg.renderer.bounds.min.y;
		var maxX = bg.renderer.bounds.max.x;
		var maxY = bg.renderer.bounds.max.y;

		minBounds = new Vector3(minX+this.camera.orthographicSize*camera.aspect, minY+this.camera.orthographicSize, 0);
		maxBounds = new Vector3(maxX-this.camera.orthographicSize*camera.aspect, maxY-this.camera.orthographicSize, 0);

	}
	
	// Update is called once per frame
	void Update () {
		mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
		camPos = new Vector3(Mathf.Clamp(mousePos.x, minBounds.x, maxBounds.x), Mathf.Clamp(mousePos.y, minBounds.y, maxBounds.y), -10);

		moveCamera();
	}

	void moveCamera() {
		transform.position = Vector3.Lerp(transform.position, camPos, cameraSpeed * Time.deltaTime);
	}
}
