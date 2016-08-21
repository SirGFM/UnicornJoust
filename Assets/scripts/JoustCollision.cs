using UnityEngine;
using System.Collections;

public class JoustCollision : BaseCollision {

	/** Detach this player from its parent */
	private void detach() {
		Rigidbody2D rb;

		this.transform.SetParent(null, true);
		rb = this.gameObject.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0.0f;
		rb.angularVelocity = Random.Range(1, 10) * 30.0f;
		rb.velocity = this.self.GetComponent<Rigidbody2D>().velocity;
		/* Disable collision with this collider */
		this.gameObject.GetComponent<Collider2D>().enabled = false;
		/* Enable the knight to loop onto the playfield */
		this.gameObject.AddComponent<LimitArea>();
	}
	
	void OnCollisionEnter2D(Collision2D otherCol) {
		BaseCollision other = otherCol.collider.GetComponent<BaseCollision>();
		if (other != null) {
			this.self.hit(other);
			if (!this.self.isAlive()) {
				this.self.disableJount();
				this.detach();
			}
			
			return;
		}
	}
}
