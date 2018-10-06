using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExosuitFabricator : MonoBehaviour {
    public RectTransform buildQueueContainer;

    public Transform spawnPosition;
    public GameObject buildButtonPrefab;

    List<ExoFabBuildButton> buildQueue = new List<ExoFabBuildButton>();


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

    void UpdateQueueIndexes() {
        for (int i = 0; i < buildQueue.Count; i++) {
            buildQueue[i].SetPosition(i);
        }
    }

    IEnumerator Build() {
        ExoFabBuildInformation buildInformation = buildQueue[0].BuildInformation;
        for (float t = 0; t < buildInformation.buildTime; t += Time.deltaTime) {
            buildQueue[0].Progress = t / buildInformation.buildTime;
            yield return null;
        }
        Instantiate(buildInformation.prefab, spawnPosition.transform.position, Quaternion.identity);
        Destroy(buildQueue[0].gameObject);
        buildQueue.RemoveAt(0);
        if (buildQueue.Count > 0) {
            UpdateQueueIndexes();
            StartCoroutine(Build());
        }
    }
}
