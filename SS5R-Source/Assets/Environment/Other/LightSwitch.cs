using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IOnPressed {
    public bool on = true;
    float transitionTime = .14f;
    float transitionPercent = 1;

    Coroutine transition;
    public Material offMaterial;
    public Material onMaterial;

    public List<Light> lights;

    void Awake() {
        UpdateLights();
    }

    public void OnPressed(Interactor by) {
        if (transition != null) StopCoroutine(transition);
        on = !on;
        UpdateLights();
        transition = StartCoroutine(Flip());
    }

    IEnumerator Flip() {
        Quaternion fromRot = this.transform.localRotation;
        Quaternion toRot = Quaternion.identity;
        if (!on)
            toRot = Quaternion.Euler(-32, 0, 0);
        for (float t = (1 - transitionPercent) * transitionTime; t < transitionTime; t += Time.deltaTime) {
            transitionPercent = t / transitionTime;
            this.transform.localRotation = Quaternion.Slerp(fromRot, toRot, transitionPercent);
            yield return null;
        }
        this.transform.localRotation = toRot;
        transitionPercent = 1;
    }

    void UpdateLights() {
        foreach (Light light in lights) {
            light.enabled = on;
            light.GetComponentInChildren<MeshRenderer>().material = on ? onMaterial : offMaterial;
        }
    }
}
