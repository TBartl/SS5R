using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used if you wear your headset on your forehead while developing
public class DebugTiltHead : MonoBehaviour {

    bool tilt = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            tilt = !tilt;
        }
    }

    void OnPreRender() {
        if (tilt) {
            Debug.Log(tilt);
            this.transform.rotation *= Quaternion.Euler(45, 0, 0);
        }
    }
}
