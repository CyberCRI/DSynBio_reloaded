﻿using UnityEngine;
using System.Collections;

public class EquipedDeviceCloseButton : MonoBehaviour {

  public EquipedDisplayedDevice device;
  private static DevicesDisplayer _devicesDisplayer;

  void Start()
  {
    if(null == _devicesDisplayer)
    {
      _devicesDisplayer = DevicesDisplayer.get();
    }
  }

	void OnPress()
  {
    if(null != device)
    {
      device.askRemoveDevice();
    }
    else
    {
            Logger.Log("EquipedDeviceCloseButton::OnPress null==device", Logger.Level.WARN);
    }
  }

    /*
  void Update()
  {
    if(null == _devicesDisplayer)
    {
      _devicesDisplayer = DevicesDisplayer.get();
    }
    else
    {
      gameObject.SetActive(_devicesDisplayer.IsEquipScreen());
    }
  }
  */
}
