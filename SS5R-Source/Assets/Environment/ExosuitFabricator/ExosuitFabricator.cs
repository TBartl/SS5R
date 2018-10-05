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
        Instantiate(buildInformation.prefab, spawnPosition.transform.position, Quaternion.identity);

        GameObject buildGO = Instantiate(buildButtonPrefab, buildQueueContainer); ;

        RectTransform buildRT = buildGO.GetComponent<RectTransform>();
        buildRT.localPosition = Vector3.zero;
        buildRT.localRotation = Quaternion.identity;

        ExoFabBuildButton build = buildGO.GetComponent<ExoFabBuildButton>();
        buildQueue.Add(build);
        build.BuildInformation = buildInformation;
        UpdateQueueIndexes();
    }

    void UpdateQueueIndexes() {
        for (int i = 0; i < buildQueue.Count; i++) {
            buildQueue[i].SetPosition(i);
        }
    }
}
