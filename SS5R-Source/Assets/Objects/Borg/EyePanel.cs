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
        Quaternion from = Quaternion.Euler(0, transform.parent.transform.localRotation.eulerAngles.y, 0);
        Quaternion to = Quaternion.Euler(0, open ? maxAngle : 0, 0);
        for (float t = 0; t < openTime; t += Time.deltaTime) {
            float p = curve.Evaluate(t / openTime);
            transform.parent.transform.localRotation = Quaternion.SlerpUnclamped(from, to, p);
            yield return null;
        }
        transform.parent.transform.localRotation = to;
    }
}
