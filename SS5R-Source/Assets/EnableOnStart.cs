using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnStart : MonoBehaviour {

	public bool dontIfEditor = true;

    public Behaviour component;

    // Use this for initialization
    void Start() {
        if (!dontIfEditor || !Application.isEditor) {
            component.enabled = true;
        }
    }
}
