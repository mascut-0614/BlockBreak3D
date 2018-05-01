using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlocks : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		BallController.attack = 0;
		if (other.gameObject.CompareTag ("Red")) {
			Destroy (gameObject);
		} else {
			Rigidbody rig = other.gameObject.GetComponent<Rigidbody> ();
			rig.velocity = rig.velocity * 1.1f;
		}
	}
}
