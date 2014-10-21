using UnityEngine;
using System.Collections;

public class ModalRestart : ModalButton {
  protected override void OnPress(bool isPressed) {
    if(isPressed) {
      Logger.Log("RestartButton::OnPress()", Logger.Level.INFO);
      GameStateController.restart();
    }
    //FIXME usefulness?
    base.OnPress(isPressed);
  }
}
