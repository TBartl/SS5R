using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour {
	
	Rigidbody rb;
	Hand grabber;

	void Awake() {
		rb = this.GetComponent<Rigidbody>();
	}

	public bool IsGrabbable() {
		return (grabber == false);
	}

	public void Grab(Hand newGrabber) {
		grabber = newGrabber;
		rb.isKinematic = true;
		this.transform.parent = grabber.transform;
	}

	public void Release() {
		grabber = null;
		rb.isKinematic = false;
		this.transform.parent = null;
	}
}
