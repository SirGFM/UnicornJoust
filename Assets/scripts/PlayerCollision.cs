using UnityEngine;
using System.Collections;

public class PlayerCollision : BaseCollision {

	void OnCollisionEnter2D(Collision2D otherCol) {
		PowerupType other;

		other = otherCol.collider.GetComponent<PowerupType>();
		if (other != null) {
			this.self.hit(other);
			return;
		}
	}
}
