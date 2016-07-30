using UnityEngine;
using System.Collections;

public class LimitArea : MonoBehaviour {

	/** Radius of the playable field */
	static float radius = 8.5f;

	private Rigidbody2D _rb;

	void Awake() {
		_rb = this.GetComponent<Rigidbody2D>();
	}

	void Update() {
		float r2 = LimitArea.radius * LimitArea.radius;
		if (this.transform.position.sqrMagnitude > r2) {
			Vector2 norm = this.transform.position.normalized * Time.deltaTime;
			this.transform.position *= -1.0f;
			/* Apply a slight force toward the center of the playfield,
			 * to push it outside the border */
			this._rb.AddForce(norm);
		}
	}
}
