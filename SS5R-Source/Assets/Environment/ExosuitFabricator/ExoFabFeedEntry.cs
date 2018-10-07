using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoFabFeedEntry : Interactable {
    [SerializeField] Transform insertPoint;
    [SerializeField] AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    static float feedTime = .5f;
    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor); ;
    }

    public override void InteractWith(Interactor interactor) {
        StartCoroutine(Feed(interactor));
    }

    IEnumerator Feed(Interactor interactor) {
        Vector3 fromPos = interactor.transform.position;
        Quaternion fromRot = interactor.transform.rotation;
        Destroy(interactor.GetComponent<Rigidbody>());
        for (float t = 0; t < feedTime; t += Time.deltaTime) {
            float p = t / feedTime;
            p = curve.Evaluate(p);
            interactor.transform.position = Vector3.Lerp(fromPos, insertPoint.transform.position, p);
            interactor.transform.rotation = Quaternion.Slerp(fromRot, insertPoint.transform.rotation, p);
            yield return null;
        }
        Destroy(interactor.gameObject);
    }
}
