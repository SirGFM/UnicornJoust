using UnityEngine;
using System.Collections;

public class PowerupExtraSpeed : Powerup {

	public override void use(GameObject caller) {
		Player pl = caller.GetComponent<Player>();
		if (pl != null) {
			pl.goFast();
		}
	}

}
