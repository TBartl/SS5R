using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoomBox : MonoBehaviour {
    public int index = 0;
    public AudioClip[] clips;

    AudioSource source;
	Text text;

    public AudioClip overrideBwoink;

	void Awake() {
		source = this.GetComponent<AudioSource>();
		text = this.GetComponentInChildren<Text>();
		Play();
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
		text.text = source.clip.name + ".ogg";
    }

    public void Stop() {
		text.text = "";
        if (!overrideBwoink) {
            source.Stop();
        } else {

        }
    }


}
