﻿using UnityEngine;
using System.Collections;

public class RelativeWASDButton : MonoBehaviour {
    
  private void OnPress(bool isPressed)
  {
    if(isPressed) {
      Logger.Log("RelativeWASDButton::OnPress()");
      ControlsMainMenuItemArray.get ().switchControlTypeToRelativeWASD();
    }
  }
}
