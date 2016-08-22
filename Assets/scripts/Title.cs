using UnityEngine;

public class Title : MonoBehaviour {

	public float timeToStart = 4.0f;

	void Update () {
		/* Start the game if requested (or on timeout) */
		this.timeToStart -= Time.deltaTime;
		if (Input.anyKeyDown || this.timeToStart <= 0.0f) {
			Application.LoadLevel("game");
		}
	}
}
