using UnityEngine;
using System.Collections;

public class LimitArea : MonoBehaviour {

	/** Radius of the playable field */
	static float radius = 7.5f;

	private Rigidbody2D _rb;

	void Awake() {
		_rb = this.GetComponent<Rigidbody2D>();
	}

	void Update() {
		float r2 = LimitArea.radius * LimitArea.radius;
		if (this.transform.position.sqrMagnitude > r2) {
			/* Place the object slightly toward the the inner area, to avoid
			 * corner cases */
			this.transform.position = this.transform.position.normalized
					* -0.95f * radius;
		}
	}
}
