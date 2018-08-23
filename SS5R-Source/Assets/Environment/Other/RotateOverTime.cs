using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {

    public Vector3 amount;

    void Update() {
        this.transform.rotation *= Quaternion.Euler(amount * Time.deltaTime);
    }
}
