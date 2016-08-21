using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour {

	public Text title;
	public Text pressToStart;

	public float speed = 3.0f;
	public float hue = 55.0f;
	public float saturation = 0.69f;
	public float value = 1.0f;
	public float timeToStart = 4.0f;

	private Color updateColor() {
		/* Source: http://pastebin.com/683Gk9xZ */
		Color ret;
		/* which chunk of the rainbow are we in? */
		float sector;
		float sectorDecimal;
		/* components */
		float p, q, t;

		/* no saturation, we can return the value across the board (grayscale) */
		if (this.saturation == 0) {
			return new Color(this.value, this.value, this.value, 1.0f);
		}

		sector = this.hue / 60.0f;
		sectorDecimal = sector - Mathf.Floor(sector);

		p = this.value * (1 - this.saturation);
		q = this.value * (1 - this.saturation * sectorDecimal);
		t = this.value * (1 - this.saturation * (1 - sectorDecimal));

		ret = Color.black;

		switch ((int)Mathf.Floor(sector)) {
			case 0: {
				ret.r = this.value;
				ret.g = t;
				ret.b = p;
			} break;
			case 1: {
				ret.r = q;
				ret.g = this.value;
				ret.b = p;
			} break;
			case 2: {
				ret.r  = p;
				ret.g  = this.value;
				ret.b  = t;
			} break;
			case 3: {
				ret.r  = p;
				ret.g  = q;
				ret.b  = this.value;
			} break;
			case 4: {
				ret.r  = t;
				ret.g  = p;
				ret.b  = this.value;
			} break;
			default: {
				ret.r  = this.value;
				ret.g  = p;
				ret.b  = q;
			} break;
		}

		return ret;
	}

	void Update () {
		/* Update the title's color */
		this.hue += speed;
		if (this.hue > 360) {
			this.hue -= 360;
		}
		this.title.color = this.updateColor();
		this.pressToStart.color = this.updateColor();
		/* Start the game if requested (or on timeout) */
		this.timeToStart -= Time.deltaTime;
		if (Input.anyKeyDown || this.timeToStart <= 0.0f) {
			Application.LoadLevel("game");
		}
	}
}
