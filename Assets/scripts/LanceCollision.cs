using UnityEngine;
using System.Collections;

public class LanceCollision : BaseCollision {

	/** Reference to the parenting player */
	private Player self;
	
	void Awake() {
		this.self = this.GetComponentInParent<Player>();
	}
	
	void OnCollisionEnter2D(Collision2D otherCol) {
		BaseCollision other;

		/* Ignore collision against other lances */
		if (otherCol.collider.CompareTag ("lance")) {
			Rigidbody2D orb = otherCol.gameObject.GetComponent<Rigidbody2D>();
			if (orb != null) {
				this.self.pushback(orb.velocity);
			}
			return;
		}

		other = otherCol.collider.GetComponent<BaseCollision>();
		if (other != null) {
			this.self.hit(other);
			return;
		}
	}
}
