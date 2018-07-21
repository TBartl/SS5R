using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPosition : MonoBehaviour {

    public Transform head;

    public Vector3 baseOffset = new Vector3(0, -.46f, -.147f);

    public AnimationCurve pitchOffsetCurve;
    public AnimationCurve rollOffsetCurve;

    // Update is called once per frame
    void Update() {
        if (!head)
            return;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, head.transform.rotation.eulerAngles.y, 0), Time.deltaTime * 2);


        Vector3 pitchOffset = Vector3.zero;
        float xRot = head.transform.rotation.eulerAngles.x;
        if (xRot > 180)
            xRot -= 360;
        xRot = Mathf.Clamp(xRot, -90, 90);
        float pXRot = xRot / 90;
        pitchOffset -= this.transform.forward * pitchOffsetCurve.Evaluate(pXRot);
        pitchOffset += Vector3.up * pitchOffsetCurve.Evaluate(pXRot);

        float zRot = head.transform.rotation.eulerAngles.z;
        if (zRot > 180)
            zRot -= 360;
        float pZRot = zRot / 90;
        pitchOffset += Mathf.Sign(pZRot) * this.transform.right * rollOffsetCurve.Evaluate(Mathf.Abs(pZRot));
        pitchOffset += this.transform.up * rollOffsetCurve.Evaluate(Mathf.Abs(pZRot));

        this.transform.position = head.transform.position + this.transform.TransformVector(baseOffset) + pitchOffset;
    }
}
