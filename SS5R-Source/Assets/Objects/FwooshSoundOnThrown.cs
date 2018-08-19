using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FwooshSoundOnThrown : MonoBehaviour, IOnLetGo {
    public AudioClip fwoosh;
    VelocityTracker velTracker;

    void Awake() {
        velTracker = this.GetComponentInParent<VelocityTracker>();
    }

    public void OnLetGo(Container from) {
        AudioSource source = this.gameObject.AddComponent<AudioSource>();
        Destroy(source, fwoosh.length + 1);
        source.clip = fwoosh;
        source.spatialBlend = 1;
        if (velTracker)
            source.volume = velTracker.GetVelocity().magnitude * .03f;
        source.Play();
    }

}
