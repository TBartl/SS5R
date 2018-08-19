using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnHover : MonoBehaviour, IOnHovered {

    public Material highlightMaterial;

    Coroutine coroutine;

    int hoverBuffer = 0;

    IEnumerator RunHighlight(Interactor interactor) {
        MeshRenderer mr = this.GetComponent<MeshRenderer>();
        List<Material> mats = new List<Material>(mr.materials);
        mats.Add(highlightMaterial);
        mr.materials = mats.ToArray();

        Controller interactorController = interactor.GetComponent<Controller>();
        if (!interactorController) {
            Containable interactorContainable = interactor.GetComponent<Containable>();
            if (interactorContainable && interactorContainable.GetContainer()) {
                interactorController = interactorContainable.GetContainer().GetComponent<Controller>();
            }
        }
        if (interactorController) {
            interactorController.Get.TriggerHapticPulse(2000);
        }
        while (hoverBuffer >= 0) {
            yield return null;
        }
        mats.RemoveAt(mats.Count - 1);
        mr.materials = mats.ToArray();
        coroutine = null;
    }

    void FixedUpdate() {
        hoverBuffer -= 1;
    }

    public void OnHovered(Interactor interactor) {
        hoverBuffer = 2;
        if (coroutine == null)
            coroutine = StartCoroutine(RunHighlight(interactor));
    }
}
