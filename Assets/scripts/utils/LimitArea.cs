using UnityEngine;
using System.Collections;

public class LimitArea : MonoBehaviour {

	/** Radius of the playable field */
	public const float radius = 3.25f;

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
