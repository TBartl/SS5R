using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnAttach : MonoBehaviour, IOnAttach {
	public AudioClip clip;
    public void OnAttach() {
        AudioSource source = this.gameObject.AddComponent<AudioSource>();
        Destroy(source, clip.length + 1);
        source.clip = clip;
        source.spatialBlend = 1;
        source.Play();
    }
}
