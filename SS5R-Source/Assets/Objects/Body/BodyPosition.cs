using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPosition : MonoBehaviour {

    public Transform head;

    public float zOffSet;

    public AnimationCurve pitchCurve;

    // Update is called once per frame
    void Update() {
        if (!head)
            return;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, head.transform.rotation.eulerAngles.y, 0), Time.deltaTime * 2);
        float xRot = head.transform.rotation.eulerAngles.x;
        Vector3 pitchOffset = Vector3.zero;
        if (xRot > 0 && xRot <= 120) {
            float pRot = xRot / 90;
            Debug.Log(pRot);
            pitchOffset -= this.transform.forward * pitchCurve.Evaluate(pRot);
            pitchOffset += Vector3.up * pitchCurve.Evaluate(pRot);
        }
        this.transform.position = head.transform.position + Vector3.down * zOffSet + pitchOffset;
    }
}
