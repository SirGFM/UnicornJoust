using UnityEngine;
using System.Collections;

public class PushComponents : MonoBehaviour {

	/** For how long it should last */
	public float time = 10.0f;
	/** Force to be applied */
	public float force = 0.125f;

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
		if (rb != null) {
			Vector3 norm = (other.transform.position - this.transform.position).normalized;
			rb.AddForce(norm * this.force * Time.fixedDeltaTime);
		}
	}
}
