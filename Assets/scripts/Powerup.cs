﻿using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	/** Enumerate the powerup types */
	public enum Type {
		POWERUP_NONE = 0,
		POWERUP_FORCE_FIELD,
		POWERUP_EXTRA_SPEED,
		POWERUP_DELAY_ZONE,
		POWERUP_TRON,
	}

	/** This powerup's type */
	public Type type = Type.POWERUP_NONE;
	/** How long until the powerup disappears */
	public float destroyTime = 15.0f;
	/** How long until the powerup may be gotten */
	public float invulnerableTime = 2.5f;

	/** Whether the powerup is vulnerable and may be gotten */
	public bool isVulnerable { get { return this._isVulnerable; } }
	private bool _isVulnerable;

	/** Check if no one got the powerup and destroy it */
	private IEnumerator disappearTimer() {
		yield return new WaitForSeconds(this.invulnerableTime);
		this._isVulnerable = true;
		yield return new WaitForSeconds(this.destroyTime - this.invulnerableTime);
		/* If this returns, then no one got the powerup (so the object hasn't been destroy) */
		/* TODO Play animation */
		GameObject.Destroy(this.gameObject);
	}

	/** Called after the object gets activated, but only once per script */
	void Start() {
		this._isVulnerable = false;
		/* Start coroutine to destroy the object if it isn't gotten within a time frame */
		this.StartCoroutine(this.disappearTimer());
	}

	void OnEnterCollision2D(Collider2D col) {
		/* Since the player will handle the collision, simply destroy the powerup */
		if (col.CompareTag("player")) {
			GameObject.Destroy(this.gameObject);
		}
	}
}
