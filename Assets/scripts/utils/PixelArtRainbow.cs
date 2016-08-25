using UnityEngine;
using System.Collections;

public class PixelArtRainbow : MonoBehaviour {

	/** How long until the next step into the "animation" */
	public float timeToNextColor = 0.06f;

	/** The sprite's material (a RainbowShader) */
	private Material _mat;
	/** Current step into the animation */
	private int _step;
	/** Number of step in the animation */
	private const int _maxColor = 7;

	private IEnumerator updateColor() {
		while (true) {
			yield return new WaitForSeconds(this.timeToNextColor);
			this._step++;
			this._step %= PixelArtRainbow._maxColor;
			this._mat.SetInt("_Color", this._step);
		}
	}

	void Start () {
		this._mat = this.gameObject.GetComponent<SpriteRenderer>().material;
		_step = 0;
		this.StartCoroutine(this.updateColor());
	}
}
