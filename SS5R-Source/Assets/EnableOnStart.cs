using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnStart : MonoBehaviour {

	public Behaviour component;

	// Use this for initialization
	void Start () {
		component.enabled = true;
	}
}
