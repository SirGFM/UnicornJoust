using UnityEngine;
using System.Collections;

public class BaseCollision : MonoBehaviour {
	public int damage = 0;

	/** Reference to the parenting player */
	protected Player self;

	void Awake() {
		this.self = this.GetComponentInParent<Player>();
	}
}
