using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSounds : MonoBehaviour, IOnHandStateChange {
    public List<AudioClip> rustleSounds;
    float falloffTime = .3f;

    public void OnChange(HandState newState) {
        if (newState == HandState.closed) {
            AudioClip rustleSound = rustleSounds[Random.Range(0, rustleSounds.Count)];
            AudioSource source = this.gameObject.AddComponent<AudioSource>();
            Destroy(source, rustleSound.length + 1);
            source.clip = rustleSound;
            source.spatialBlend = 1;
            source.Play();
            source.time = .03f;
			StartCoroutine(FallOff(source));
        }
    }
    IEnumerator FallOff(AudioSource source) {
        for (float t = 0; t < falloffTime; t += Time.deltaTime) {
            float p = t / falloffTime;
            if (source)
                source.volume = 1 - t;
            yield return null;
        }
        if (source)
            source.volume = 0;
    }
}
