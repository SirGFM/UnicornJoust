using UnityEngine;
using System.Collections;

public class PushComponents : MonoBehaviour {

	/** Force to be applied */
	private float _force;
	/** Game object to be ignored */
	private GameObject _ignore;

	private IEnumerator timer(float time) {
		yield return new WaitForSeconds(time);
		GameObject.Destroy(this.gameObject);
	}

	/**
	 * Start a push force field
	 * 
	 * @param  [ in]force  Force to be applied
	 * @param  [ in]time   For how long it should last
	 * @param  [ in]ignore Game object to be ignored
	 */
	public void start(float force, float time, GameObject ignore) {
		this.StartCoroutine(this.timer (time));
		this._ignore = ignore;
		this._force = force;
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

		rb = other.gameObject.GetComponent<Rigidbody2D>();
		if (rb == null) {
			rb = other.gameObject.GetComponentInParent<Rigidbody2D>();
		}
		if (rb != null) {
			Vector3 norm = (other.transform.position - this.transform.position).normalized;
			rb.AddForce(norm * this._force * Time.fixedDeltaTime);
		}
	}
}
