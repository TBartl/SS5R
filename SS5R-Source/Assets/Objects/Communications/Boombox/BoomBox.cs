using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour {
    int index = 0;
    public AudioClip[] clips;

    AudioSource source;

    public AudioClip overrideBwoink;

	void Awake() {
		source = this.GetComponent<AudioSource>();
	}

    public void Forward() {
        index = (index + 1) % clips.Length;
        source.clip = clips[index];
		Play();
    }

    public void Back() {
        index = (index - 1 + clips.Length) % clips.Length;
        source.clip = clips[index];
		Play();
    }

    public void Play() {
        source.Play();
    }

    public void Stop() {
        if (!overrideBwoink) {
            source.Stop();
        } else {

        }
    }


}
