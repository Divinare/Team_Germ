using UnityEngine;
using System.Collections;

public class onClickDestroy : MonoBehaviour {


	void OnMouseDown() {
		Destroy(this.gameObject);
	}
}
