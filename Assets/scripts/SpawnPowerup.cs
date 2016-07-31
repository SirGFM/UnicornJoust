using UnityEngine;
using System.Collections;

public class SpawnPowerup : MonoBehaviour {

	/** Minimum time between updates in seconds */
	public float minTime = 5.0f;
	/** Maximum time between updates in seconds */
	public float maxTime = 8.0f;
	public GameObject[] powerups;

	/** Flag to control polling */
	private bool _isSpawning;

	/**
	 * Queue the spawning of a powerup
	 * 
	 * @param  [ in]delay How long until the powerup should be spawned
	 */
	private IEnumerator spawn(float delay) {
		int i;
		/* Wait the desired time and spawn a new powerup */
		yield return new WaitForSeconds(delay);
		i = Random.Range(0, powerups.Length);
		if (i < powerups.Length) {
			/* TODO Spawn it! */
		}
		/* Unqueue the powerup */
		this._isSpawning = false;
	}

	/** === UNITY EVENTS ======================================================== */

	/** Called after the object gets activated, but only once per script */
	void Start () {
		this._isSpawning = false;
	}

	/** Called on a variable interval (sync'ed with the draw rate) */
	void Update () {
		/* Poll until the next update should be queued */
		if (!this._isSpawning) {
			float delay = Random.Range(this.minTime, this.maxTime);
			this.StartCoroutine(this.spawn(delay));
			this._isSpawning = true;
		}
	}
}
