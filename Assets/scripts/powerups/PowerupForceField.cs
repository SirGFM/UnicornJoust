using UnityEngine;
using System.Collections;

public class PowerupForceField : Powerup {

	private const float force = 0.25f;
	private const float time = 10.0f;
	private const float radius = 6.0f;

	public override void use(GameObject caller) {
		GameObject go = new GameObject();
		PushComponents pc = go.AddComponent<PushComponents>();
		CircleCollider2D cc = go.AddComponent<CircleCollider2D>();
		go.name = "Powerup Force Field";
		go.transform.position = caller.transform.position;
		go.AddComponent<Rigidbody2D>().gravityScale = 0.0f;
		cc.radius = PowerupForceField.radius;
		cc.isTrigger = true;
		pc.start(PowerupForceField.force, PowerupForceField.time, caller);
	}
}
