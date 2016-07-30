using UnityEngine;
using System.Collections;

public class collisionHandler : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		Debug.Log(col.collider.gameObject.name);
	}
}
