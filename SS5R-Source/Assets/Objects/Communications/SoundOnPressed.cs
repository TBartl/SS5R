using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnPressed : MonoBehaviour, IOnPressed {
    public AudioClip clip;

    public void OnPressed(Interactor by) {
        AudioSource source = this.gameObject.AddComponent<AudioSource>();
        Destroy(source, clip.length + 1);
        source.clip = clip;
        source.spatialBlend = 1;
        source.Play();
    }

}
