using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacher : Interactor, IOnLetGo {
    AttachPoint attachedTo;

    public override bool CanInteractWith(Interactable interactable) {
        return base.CanInteractWith(interactable) && attachedTo == null && interactable.GetComponent<AttachPoint>();
    }

    public void OnLetGo(Container from) {
        if (!hoverObject)
            return;
        AttachPoint point = hoverObject.GetComponent<AttachPoint>();
        point.InteractWith(this);
    }

    public void SetAttached(AttachPoint point) {
        attachedTo = point;
        this.transform.parent = point.transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        this.transform.parent = point.transform.parent;
        Destroy(this.GetComponent<Rigidbody>());
        Destroy(this.GetComponent<Grabbable>());
    }

}

