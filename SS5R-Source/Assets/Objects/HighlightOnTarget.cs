using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class HighlightOnTarget : MonoBehaviour, IOnHovered {

    public Material highlightMaterial;

    Coroutine coroutine;

    int hoverBuffer = 0;

    IEnumerator RunHighlight() {
        MeshRenderer mr = this.GetComponent<MeshRenderer>();
        List<Material> mats = new List<Material>(mr.materials);
        mats.Add(highlightMaterial);
        mr.materials = mats.ToArray();
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
            coroutine = StartCoroutine(RunHighlight());
    }
}
