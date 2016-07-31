using UnityEngine;
using System.Collections;

public class PowerupForceField : Powerup {

	/** Prefab to be spawned */
	static public PushComponents prefab;

	public override void use(GameObject caller) {
		PushComponents pc = GameObject.Instantiate<PushComponents>(prefab);
		pc.transform.position = caller.transform.position;
		pc.start(caller);
	}
}
