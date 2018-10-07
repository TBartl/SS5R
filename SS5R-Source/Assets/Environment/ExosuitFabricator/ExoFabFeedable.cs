using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Containable))]
public class ExoFabFeedable : Interactor, IOnLetGo {

    [SerializeField] BuildResource resourceType;
    [SerializeField] int resourceAmount;

    public BuildResource ResourceType { get { return resourceType; } }
    public int ResourceAmount { get { return resourceAmount; } }

    public override bool CanInteractWith(Interactable interactable) {
        if (!base.CanInteractWith(interactable))
            return false;
        return interactable.GetComponent<ExoFabFeedEntry>();
    }

    public void OnLetGo(Container from) {
        if (this.hoverObject && this.CanInteractWith(hoverObject)) {
            this.hoverObject.GetComponent<ExoFabFeedEntry>().InteractWith(this);
        }
    }
}
