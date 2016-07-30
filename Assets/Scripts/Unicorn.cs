using UnityEngine;
using System.Collections;

public class Unicorn : MonoBehaviour {


	public float propVel = 10;
	public float rotVel = 1;

	public float MAXPROPVEL = 15;
	public float MAXROTVEL = 5;

	public bool canMove = true;

	GameObject body;
	GameObject spear;

	Rigidbody rigidBody;

	bool colSpearFlag;

	void Awake() {
		body = transform.Find("Body").gameObject;	
		spear = transform.Find("Spear").gameObject;
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		//Keys actions
		if(canMove){
			if(Input.GetKey(KeyCode.UpArrow)){
				rigidBody.AddForce(transform.right * propVel, ForceMode.Force);
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				rigidBody.AddTorque(-Vector3.forward * rotVel, ForceMode.Force);
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				rigidBody.AddTorque(Vector3.forward * rotVel, ForceMode.Force);
			}
		}

		//Max propVel
		if(rigidBody.velocity.magnitude > MAXPROPVEL){
			rigidBody.velocity = rigidBody.velocity.normalized * MAXPROPVEL;
		}
		//Max rotVel
		if(rigidBody.angularVelocity.magnitude > MAXROTVEL){
			rigidBody.angularVelocity = rigidBody.angularVelocity.normalized * MAXROTVEL;
		}
	}

	void OnCollisionEnter(Collision c){
		Collider col = c.collider;
		if(col.gameObject.name == "Spear"){
			Debug.Log("AAAAAAAAHHHHHHH");
			
			//Check 
			colSpearFlag = false;
			foreach (ContactPoint contact in c.contacts) {							
				if(spear.GetComponent<Collider>().bounds.Contains(contact.point))
					colSpearFlag = true;								
			}			
			if(colSpearFlag){
				Debug.Log(gameObject.name + " BOOM!!");	
			}else{
				Debug.Log(gameObject.name + " Morri!");	
			}		
		}
	}

}
