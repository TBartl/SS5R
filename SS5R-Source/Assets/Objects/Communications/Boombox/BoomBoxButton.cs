using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoomBoxButtonType {
    back,
    forward,
	play,
    stop,
}

public class BoomBoxButton : MonoBehaviour, IOnPressed {

    public BoomBoxButtonType buttonType;

    public void OnPressed(Interactor by) {
        BoomBox boomBox = this.GetComponentInParent<BoomBox>();
        if (buttonType == BoomBoxButtonType.back) {
            boomBox.Back();
        } else if (buttonType == BoomBoxButtonType.forward) {
            boomBox.Forward();
        } else if (buttonType == BoomBoxButtonType.play) {
            boomBox.Play();
        } else if (buttonType == BoomBoxButtonType.stop) {
            boomBox.Stop();
        }
    }

}
