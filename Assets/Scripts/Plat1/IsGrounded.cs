using UnityEngine;
using System.Collections;

public class IsGrounded : MonoBehaviour {
	public bool grounded;

	void Start() { 
		grounded = false;
	}

	void OnTriggerEnter2D (Collider2D c) {
		grounded = true;
		
	}

	void OnTriggerStay2D (Collider2D c) {
		grounded = true;

	}

	void OnTriggerExit2D (Collider2D c) {
		grounded = false;
	}
}

//24*13