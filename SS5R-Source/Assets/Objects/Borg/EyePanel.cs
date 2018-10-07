using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePanel : MonoBehaviour {

    [SerializeField] AnimationCurve curve;
    [SerializeField] float maxAngle = 90;


    bool open = false;
    Coroutine co;

    static float openTime = .3f;

    public bool Open {
        get { return open; }
        set {
            if (value == open) return;
            open = value;
            if (co != null) StopCoroutine(co);
            co = StartCoroutine(Move());
            if (open) this.GetComponent<SoundOnPressed>().OnPressed(null);
        }
    }
    IEnumerator Move() {
        float from = (transform.parent.transform.localRotation.eulerAngles.y + 360) % 360;
        float to = open ? maxAngle : 0;
        for (float t = 0; t < openTime; t += Time.deltaTime) {
            float p = curve.Evaluate(t / openTime);
            float angle = Mathf.LerpUnclamped(from, to, p);
            transform.parent.transform.localRotation = Quaternion.Euler(0, angle, 0);
            yield return null;
        }
        transform.parent.transform.localRotation = Quaternion.Euler(0, to, 0);
    }
}
