using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnHovered : MonoBehaviour, IOnHovered {

    float recentlyHovered = 0f;

    public AudioClip clip;

    public void OnHovered(Interactor interactor) {
        bool wasRecentlyHovered = recentlyHovered > 0;
        recentlyHovered = .2f;
        if (wasRecentlyHovered)
            return;
        AudioSource source = this.gameObject.AddComponent<AudioSource>();
        Destroy(source, clip.length + 1);
        source.clip = clip;
        source.spatialBlend = 1;
        source.Play();
    }

    void Update() {
        recentlyHovered -= Time.deltaTime;
    }
}
