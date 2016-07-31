using UnityEngine;
using System.Collections;

public class PowerupDelayZone : Powerup {

	/** Prefab to be spawned */
	static public SlowComponents prefab;

	public override void use(GameObject caller) {
		SlowComponents pc = GameObject.Instantiate<SlowComponents>(prefab);
		pc.transform.position = caller.transform.position;
		pc.start(caller);
	}

}
