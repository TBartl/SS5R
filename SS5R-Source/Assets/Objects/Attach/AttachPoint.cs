using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : Interactable {
    public GameObject targetPrefab;

    Attacher attached;

    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor) && attached == null && interactor.name.Split(' ')[0] == targetPrefab.name;
    }

    public override void InteractWith(Interactor interactor) {
        Attacher attacher = interactor.GetComponent<Attacher>();
        attached = attacher;
        interactor.GetComponent<Attacher>().SetAttached(this);

        foreach (IOnAttach onAttach in this.GetComponentsInChildren<IOnAttach>()) {
            onAttach.OnAttach();
        }
    }
}
