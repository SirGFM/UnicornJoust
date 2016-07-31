using UnityEngine;
using System.Collections;

public class SlowComponents : MonoBehaviour {

	/** For how long it should last */
	public float time = 10.0f;
	/** Minimum velocity (don't slow components further) */
	public float minVelocity = 0.125f;
	/** Multiply velocity by this factor */
	public float slowRate = 0.35f;
	
	/** Game object to be ignored */
	private GameObject _ignore;
	
	private IEnumerator timer(float time) {
		yield return new WaitForSeconds(time);
		GameObject.Destroy(this.gameObject);
	}
	
	/**
	 * Start a push force field
	 * 
	 * @param  [ in]ignore Game object to be ignored
	 */
	public void start(GameObject ignore) {
		this.StartCoroutine(this.timer(this.time));
		this._ignore = ignore;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		this.onTrigger(other);
	}
	
	void OnTriggerStay2D(Collider2D other) {
		this.onTrigger(other);
	}
	
	private void onTrigger(Collider2D other) {
		GameObject go;
		Rigidbody2D rb;
		
		go = other.gameObject;
		while (go.transform.parent != null) {
			go = go.transform.parent.gameObject;
		}
		if (go == this._ignore) {
			return;
		}
		
		rb = go.GetComponent<Rigidbody2D>();
		if (rb != null && rb.velocity.sqrMagnitude > this.minVelocity * this.minVelocity) {
			rb.velocity *= this.slowRate;
			/* Set the objec to the minimum velocity */
			if (rb.velocity.sqrMagnitude != 0.0f
			    	&& rb.velocity.sqrMagnitude < this.minVelocity * this.minVelocity) {
				rb.velocity = rb.velocity.normalized * this.slowRate;
			}
		}
	}
}
