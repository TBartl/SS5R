using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour {
    public Vector2 waitRange = new Vector2(5, 15);
    public float rareClipChance = .3f;

    AudioSource baseSource;
    AudioSource addedSource;

    public AudioClip baseClip;
    public List<AudioClip> randomClips;
    public List<AudioClip> rareClips;

    void Awake() {
        AudioSource[] sources = this.GetComponents<AudioSource>();
        baseSource = sources[0];
        addedSource = sources[1];

        baseSource.clip = baseClip;
        StartCoroutine(RandomOnDelay());
    }
    IEnumerator RandomOnDelay() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
            List<AudioClip> selectionClips = randomClips;
            if (Random.value < rareClipChance) {
                selectionClips = rareClips;
            }
            AudioClip selectedClip = selectionClips[Random.Range(0, selectionClips.Count)];
            addedSource.clip = selectedClip;
            addedSource.Play();
            while (addedSource.isPlaying) yield return null;

        }

    }
}
