using UnityEngine;
using System.Collections;

public class UnicornBody : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//void OnCollisionEnter(Collision c){
	void OnTriggerEnter(Collider col){
		//Collider col = c.collider;
		if(col.gameObject.name == "Spear"){
			Debug.Log("AAAAAAAAHHHHHHH");
		}
	}
}
