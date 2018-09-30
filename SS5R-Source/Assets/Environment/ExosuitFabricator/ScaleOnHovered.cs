using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnHovered : MonoBehaviour, IOnHovered {
    public Vector2 range = new Vector2(1.1f, 1.2f);
    public float oscillateSpeed = 1f;
    public float decreaseSpeed = 1f;

    public void OnHovered(Interactor interactor) {
        this.transform.localScale = Vector3.one * Mathf.Lerp(range.x, range.y, (Mathf.Sin(Time.time * oscillateSpeed) + 1) / 2);
    }

    void Update() {
        float decrease = Mathf.Min(decreaseSpeed * Time.deltaTime, this.transform.localScale.x - 1);
        this.transform.localScale -= Vector3.one * decrease;
    }

}
