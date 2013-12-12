using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class ListedDevice : DisplayedDevice {
  private static string craftZoneManagerObjectName = "AssemblyZonePanel";
  private static CraftZoneManager craftZoneManager;

  private static string _hoveredSuffix = "Hovered";
  private static string _pressedSuffix = "Pressed";
  private static string _defaultSpriteName = "bullet";
  public UILabel _nameLabel;



 public static ListedDevice Create(
   Transform parentTransform
   , Vector3 localPosition
   , Device device
   , DevicesDisplayer devicesDisplayer
   )
 {
    ListedDevice result = (ListedDevice)DisplayedDevice.Create(
          parentTransform
          , localPosition
          , _defaultSpriteName
          , device
          , devicesDisplayer
          , DevicesDisplayer.DeviceType.Listed
      );

    result.setNameLabel(GameplayNames.generateRealNameFromBricks(device));

    return result;
 }

  public void setNameLabel(string name) {
    _nameLabel.text = name;
  }

  protected override void OnPress(bool isPressed) {
    Logger.Log("ListedDevice::OnPress("+isPressed+")", Logger.Level.INFO);
    setPressed(isPressed);
    if(isPressed) {
      //ask craft zone to display biobricks associated to this device
      if(craftZoneManager == null)
      {
        craftZoneManager = CraftZoneManager.get();
      }
      craftZoneManager.setDevice(_device);
    }
  }

  protected void OnHover(bool isHovered) {
    Logger.Log("ListedDevice::OnHover("+isHovered+")", Logger.Level.TRACE);
    setHovered(isHovered);
  }


  private void setHovered(bool isHovered) {
    Logger.Log("ListedDevice::setHovered("+isHovered+")", Logger.Level.TRACE);
    if(isHovered) {
      setSprite(_currentSpriteName + _hoveredSuffix);
    } else {
      setSprite(_currentSpriteName);
    }
  }


  private void setPressed(bool isPressed) {
    Logger.Log("ListedDevice::setPressed("+isPressed+")", Logger.Level.TRACE);
    if(isPressed) {
      setSprite(_currentSpriteName + _pressedSuffix);
    } else {
      //managed by hover
      //setSprite(_currentSpriteName);
    }
  }

  // Use this for initialization
  void Start () {
    if(_nameLabel == null) {
      _nameLabel = gameObject.GetComponentInChildren<UILabel>();
    }
    craftZoneManager = CraftZoneManager.get();
  }
}