using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	
	public enum enController {
		CONTROLLER_1 = 1,
		CONTROLLER_2 = 2
	}

	public float velocity = 0.5f;
	public float maxVelocity = 1.5f;
	public float spinVelocity = 90.0f;
	public float deadzone = 0.3f;
	public enController controller = enController.CONTROLLER_1;

	private Rigidbody2D rb;
	private string _moveAxis;
	private string _boostAxis;

	/** Update the axis controllers to the assigned one */
	public void updateAxis() {
		this._moveAxis = ((int)controller).ToString() + "-movement";
		this._boostAxis = ((int)controller).ToString() + "-boost";
	}

	/** Called as soon as the component is instantiated */
	void Awake() {
		this.rb = this.GetComponent<Rigidbody2D>();
		this.updateAxis();
	}

	/** Called on a fixed interval */
	void FixedUpdate() {
		/* Update angle */
		if (Input.GetAxisRaw (this._moveAxis) < -deadzone) {
			this.rb.angularVelocity = this.spinVelocity;
		}
		else if (Input.GetAxisRaw (this._moveAxis) > deadzone) {
			this.rb.angularVelocity = -this.spinVelocity;
		}
		else {
			this.rb.angularVelocity = 0;
		}
		/* Update forward movement */
		if (Input.GetAxisRaw(this._boostAxis) > deadzone) {
			this.rb.AddRelativeForce(new Vector2(this.velocity * Time.fixedDeltaTime, 0.0f), 0);
		}
		/* Cap speed */
		if (this.rb.velocity.sqrMagnitude > this.maxVelocity * this.maxVelocity) {
			this.rb.velocity = this.rb.velocity.normalized * this.maxVelocity;
		}
	}
}
