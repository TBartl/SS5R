using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposalBinLever : OpenHandInteractable {

    Coroutine co;

    float leverMoveTime = .2f;
    float downDist = .16f;

    public AudioClip flush;
    public BoxCollider deleteCollider;

    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor) && (co == null);
    }

    public override void InteractWith(Interactor interactor) {
        if (co != null) {
            return;
        }
        co = StartCoroutine(Dispose());
    }

    IEnumerator Dispose() {
        PlaySound();

        Bounds bounds = deleteCollider.bounds;
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity);
        foreach (Collider col in colliders) {
            if (col.GetComponent<Grabbable>()) {
                Destroy(col.gameObject);
            }
        }

        for (float t = 0; t < leverMoveTime; t += Time.deltaTime) {
            float p = t / leverMoveTime;
            this.transform.localPosition = Vector3.down * Mathf.Lerp(0, downDist, p);
            yield return null;
        }
        this.transform.localPosition = Vector3.down * downDist;

        yield return new WaitForSeconds(flush.length - 2 * leverMoveTime);

        for (float t = 0; t < leverMoveTime; t += Time.deltaTime) {
            float p = t / leverMoveTime;
            this.transform.localPosition = Vector3.down * Mathf.Lerp(downDist, 0, p);
            yield return null;
        }
        this.transform.localPosition = Vector3.zero;

        co = null;
    }
    void PlaySound() {
        AudioSource source = this.gameObject.AddComponent<AudioSource>();
        Destroy(source, flush.length + 1);
        source.clip = flush;
        source.spatialBlend = 1;
        source.Play();
    }
}
