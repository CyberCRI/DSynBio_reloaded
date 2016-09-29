﻿using UnityEngine;
using System.Collections;

public class SoundOptionsMainMenuItem : SettingsMainMenuItem {
    public override void click() {
        // Debug.Log(this.GetType());
		RedMetricsManager.get().sendEvent(TrackingEvent.SELECTMENU, new CustomData(CustomDataTag.OPTION, CustomDataValue.SOUND.ToString()));
        MainMenuManager.get ().switchTo (MainMenuManager.MainMenuScreen.SOUNDOPTIONS);
    }
}
