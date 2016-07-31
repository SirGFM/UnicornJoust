using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	/**
	 * Enumerate all possible controllers
	 * 
	 * A controller represents the index of the input to be queried. It has
	 * no relationship to the player type.
	 */
	public enum enController {
		CONTROLLER_1 = 1,
		CONTROLLER_2 = 2
	}

	public int initialHealth = 2;
	/** Invulnerability duration in seconds */
	public float invulnerability = 2.0f;
	/** Acceleration in some weird unit */
	public float acceleration = 0.5f;
	/** Max velocity in units/s */
	public float maxVelocity = 3f;
	/** Spin rate in degree/s (theoretically) */
	public float spinVelocity = 270.0f;
	/** Input deadzone (0.0f == perfectly-centered, 1.0f == maximum-reach) */
	public float deadzone = 0.3f;
	/** For how long should the player move faster (on powerup) */
	public float timeToGoFast = 3.5f;
	/** How much faster can the object go */
	public float fastRate = 2.0f;
	public enController controller = enController.CONTROLLER_1;

	private Rigidbody2D rb;
	private Powerup _pup;
	private string _moveAxis;
	private string _boostAxis;
	private string _itemButton;
	private bool _itemNotPressed;
	private int _curHealth;
	public int curHealth {
		get {
			return this._curHealth;
		}
	}
	private float _curInvulnerability;
	private bool _doGoFast = false;

	/** === PUBLIC FUNCTION ===================================================== */

	/** Update the axis controllers to the assigned one */
	public void updateAxis() {
		this._moveAxis = ((int)controller).ToString() + "-movement";
		this._boostAxis = ((int)controller).ToString() + "-boost";
		this._itemButton = ((int)controller).ToString() + "-item";
	}

	/**
	 * Signal that this object is to be pushed back (from the next frame onward)
	 * 
	 * @param  [ in]velocity Velocity by which the player will be pushed back (in units/s)
	 */
	public void pushback(Vector2 velocity) {
		this.StartCoroutine(this._pushback(velocity));
	}
	private IEnumerator _pushback(Vector2 velocity) {
		float time = 0.0f;
		yield return null;
		while (time < 0.5f) {
			this.rb.AddForce(velocity * Time.fixedDeltaTime);
			time += Time.fixedDeltaTime;
			yield return null;
		}
	}

	/**
	 * Try to get a powerup
	 */
	public bool hit(PowerupType col) {
		this._pup = col.getPowerup();
		return false;
	}

	/**
	 * Try to hit this player
	 * 
	 * This function may also be used to heal it
	 * 
	 * @param  [ in]dmg Amount of damage to be dealt
	 * @return          Whether the player was actually hit
	 */
	public bool hit(BaseCollision col) {
		/* Check if collision did actually happen */
		if (this._curInvulnerability > 0.0f) {
			return false;
		}
		if (col.damage == 0) {
			return false;	
		}
		/* Apply damage/heal */
		this._curHealth -= col.damage;
		/* Only set invulnerability on damage */
		if (col.damage > 0) {
			this._curInvulnerability += this.invulnerability;
		}
		/* If we were hit by a lance, pushback */
		if (col.CompareTag("lance")) {
			Rigidbody2D other = col.GetComponentInParent<Rigidbody2D>();
			if (other != null) {
				this.pushback(other.velocity);
			}
		}
		return true;
	}

	/** Check if the player is still alive */
	public bool isAlive() {
		return this._curHealth > 0;
	}

	public void goFast() {
		this.StartCoroutine(this._gottaGoFast());
	}
	private IEnumerator _gottaGoFast() {
		this._doGoFast = true;
		yield return new WaitForSeconds(this.timeToGoFast);
		this._doGoFast = false;
	}

	/** === UNITY EVENTS ======================================================== */

	/** Called as soon as the component is instantiated */
	void Awake() {
		this.rb = this.GetComponent<Rigidbody2D>();
		this._curHealth = this.initialHealth;
		this._curInvulnerability = 0.0f;
		this._itemNotPressed = false;
		this.updateAxis();
	}

	/** Called on a fixed interval (once per physical update) */
	void FixedUpdate() {
		float maxv;
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
			Vector2 force = Vector2.zero;
			float ang = Mathf.Deg2Rad * this.transform.eulerAngles.z;
			/* Manually rotate the applied force, to separate it from the sprite */
			force.x = Mathf.Cos(ang);
			force.y = Mathf.Sin(ang);
			force *= this.acceleration * Time.fixedDeltaTime;
			this.rb.AddForce(force);
		}
		/* Cap speed */
		maxv = this.maxVelocity;
		if (this._doGoFast) {
			maxv *= this.fastRate;
		}
		if (this.rb.velocity.sqrMagnitude > maxv * maxv ) {
			this.rb.velocity = this.rb.velocity.normalized * maxv;
		}
		/* Update the invulnerability */
		if (this._curInvulnerability > 0.0f) {
			this._curInvulnerability -= Time.fixedDeltaTime;
			if (this._curInvulnerability < 0.0f) {
				this._curInvulnerability = 0.0f;
			}
		}
	}

	/** Called on a variable interval (sync'ed with the draw rate) */
	void Update() {
		/* Try to use the current item */
		if (Input.GetAxisRaw(this._itemButton) > this.deadzone) {
			if (this._itemNotPressed) {
				if (this._pup != null) {
					this._pup.use(this.gameObject);
					this._pup = null;
				}
				else {
					/* TODO Signal error! */
					Debug.LogWarning("No items!");
				}
			}
			this._itemNotPressed = false;
		}
		else {
			this._itemNotPressed = true;
		}
		/* Try to keep the joust toward the screen's top */
		if (this.transform.eulerAngles.z < 270.0f && 
		    	this.transform.eulerAngles.z > 90.0f) {
			Vector3 s = this.transform.localScale;
			s.y = -1.0f;
			this.transform.localScale = s;
		}
		else {
			Vector3 s = this.transform.localScale;
			s.y = 1.0f;
			this.transform.localScale = s;
		}
	}
}
