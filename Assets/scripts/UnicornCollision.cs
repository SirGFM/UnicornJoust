﻿using UnityEngine;
using System.Collections;

public class UnicornCollision : BaseCollision {

	void OnCollisionEnter2D(Collision2D otherCol) {
		BaseCollision other = otherCol.collider.GetComponent<BaseCollision>();
		if (other != null) {
			this.self.hit(other);
			if (!this.self.isAlive()) {
				//Play death animation 1 (explosion)
				self.die();
			}
			return;
		}
	}
}
