using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacher : Interactor, IOnLetGo {
    AttachPoint attachedTo;
    float attachTime = .18f;
    AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public override bool CanInteractWith(Interactable interactable) {
        if (!base.CanInteractWith(interactable))
            return false;
        if (this.GetComponent<Containable>().GetContainer() == null)
            return false;
        return attachedTo == null && interactable.GetComponent<AttachPoint>();
    }

    public void OnLetGo(Container from) {
        if (!hoverObject)
            return;
        AttachPoint point = hoverObject.GetComponent<AttachPoint>();
        point.InteractWith(this);
    }

    public void SetAttached(AttachPoint point) {
        attachedTo = point;
        this.transform.parent = point.transform.parent;
        StartCoroutine(SmoothAttach());
        Destroy(this.GetComponent<Rigidbody>());
        Destroy(this.GetComponent<Grabbable>());
    }

    IEnumerator SmoothAttach() {
        Vector3 fromPos = this.transform.localPosition;
        Vector3 toPos = attachedTo.transform.localPosition;
        Quaternion fromRot = this.transform.localRotation;
        Quaternion toRot = attachedTo.transform.localRotation;
        for (float t = 0; t < attachTime; t += Time.deltaTime) {
            float p = t / attachTime;
            this.transform.localPosition = Vector3.Lerp(fromPos, toPos, curve.Evaluate(p));
            this.transform.localRotation = Quaternion.Slerp(fromRot, toRot, curve.Evaluate(p));
            yield return null;
        }
        this.transform.localPosition = toPos;
        this.transform.localRotation = toRot;
    }
}

