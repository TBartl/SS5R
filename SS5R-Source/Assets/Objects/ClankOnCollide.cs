using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClankOnCollide : MonoBehaviour, IOnCollision {
    public AudioClip clank;
    float threshold = 2;
    float max = 10;
    public void OnCollided(Collision col) {
        float speed = col.relativeVelocity.magnitude;

        AudioSource source = this.gameObject.AddComponent<AudioSource>();
        Destroy(source, clank.length + 1);
        source.clip = clank;
        source.spatialBlend = 1;
        source.volume = (speed - threshold) / (max - threshold);
        source.Play();
        source.time = .3f;
    }
}
