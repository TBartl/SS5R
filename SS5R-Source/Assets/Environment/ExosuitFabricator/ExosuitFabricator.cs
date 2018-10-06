using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExosuitFabricator : MonoBehaviour {
    AudioSource noiseAudioSource;

    public RectTransform buildQueueContainer;
    public MeshFilter displayModel;

    public Transform spawnPosition;
    public GameObject buildButtonPrefab;

    List<ExoFabBuildButton> buildQueue = new List<ExoFabBuildButton>();

    void Awake() {
        noiseAudioSource = this.GetComponent<AudioSource>();
    }

    public void TryBuild(ExoFabBuildInformation buildInformation) {
        GameObject buildGO = Instantiate(buildButtonPrefab, buildQueueContainer); ;

        RectTransform buildRT = buildGO.GetComponent<RectTransform>();
        buildRT.localPosition = Vector3.zero;
        buildRT.localRotation = Quaternion.identity;

        ExoFabBuildButton build = buildGO.GetComponent<ExoFabBuildButton>();
        buildQueue.Add(build);
        build.BuildInformation = buildInformation;
        UpdateQueueIndexes();

        if (buildQueue.Count == 1) {
            StartCoroutine(Build());
        }
    }

    public void TryDelete(int index) {
        if (index == 0) return;
        Delete(index);

    }

    void UpdateQueueIndexes() {
        for (int i = 0; i < buildQueue.Count; i++) {
            buildQueue[i].Index = i;
        }
    }

    IEnumerator Build() {
        noiseAudioSource.Play();
        ExoFabBuildInformation buildInformation = buildQueue[0].BuildInformation;
        SetupDisplayModel(buildInformation.displayModel);

        for (float t = 0; t < buildInformation.buildTime; t += Time.deltaTime) {
            buildQueue[0].Progress = t / buildInformation.buildTime;
            yield return null;
        }
        Instantiate(buildInformation.prefab, spawnPosition.transform.position, Quaternion.identity);
        Delete(0);
        noiseAudioSource.Stop();
        displayModel.mesh = null;
        if (buildQueue.Count > 0) {
            StartCoroutine(Build());
        }
    }

    void SetupDisplayModel(Mesh mesh) {
        MeshRenderer displayRenderer = displayModel.GetComponent<MeshRenderer>();
        displayModel.mesh = mesh;
        Bounds bounds = displayRenderer.bounds;
        if (bounds.size.y > bounds.size.z) {
            displayModel.transform.rotation = Quaternion.Euler(-90, 0, 0);
            bounds = displayRenderer.bounds;
        }
    }

    void Delete(int index) {
        Destroy(buildQueue[0].gameObject);
        buildQueue.RemoveAt(0);
        UpdateQueueIndexes();
    }
}
