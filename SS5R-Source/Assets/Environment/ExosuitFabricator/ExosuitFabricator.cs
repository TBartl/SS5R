using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExosuitFabricator : MonoBehaviour {
    public RectTransform buildQueueContainer;

    public Transform spawnPosition;
    public GameObject buildButtonPrefab;

    List<ExoFabBuildButton> buildQueue = new List<ExoFabBuildButton>();

    bool buildPercentage;

    public void TryBuild(ExoFabBuildInformation buildInformation) {
        Instantiate(buildInformation.prefab, spawnPosition.transform.position, Quaternion.identity);

        GameObject buildGO = Instantiate(buildButtonPrefab, buildQueueContainer);

        Image buildImage = buildGO.GetComponent<Image>();
        buildImage.SetNativeSize();
        // buildImage.sprite = buildInformation.;

        RectTransform buildRT = buildGO.GetComponent<RectTransform>();
        buildRT.localPosition = Vector3.zero;
        buildRT.localRotation = Quaternion.identity;

        ExoFabBuildButton build = buildGO.GetComponent<ExoFabBuildButton>();
        buildQueue.Add(build);

        UpdatePositions();

    }

    void UpdatePositions() {
        for (int i = 0; i < buildQueue.Count; i++) {
            RectTransform rt = buildQueue[i].GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, -(rt.sizeDelta.y + 10) * i, 0);
        }
    }
}
