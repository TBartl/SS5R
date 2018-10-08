using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wireable : Interactable {
    [SerializeField] MeshRenderer wires;

    Material replaceMaterial;
    bool wired = false;

    void Awake() {
        replaceMaterial = wires.material;
        wires.materials = new Material[] { };
    }

    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor) && wired == false && interactor.GetComponent<Wirer>() != null;
    }

    public override void InteractWith(Interactor interactor) {
        wired = true;
        wires.materials = new Material[] { replaceMaterial };
        this.GetComponent<AudioSource>().Play();
    }
}
