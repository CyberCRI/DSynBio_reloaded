﻿public class SoundOptionsMainMenuItem : MainMenuItem
{
    public override void click()
    {
        // Debug.Log(this.GetType());
		base.click();
        MainMenuManager.get().switchTo(MainMenuManager.MainMenuScreen.SOUNDOPTIONS);
        RedMetricsManager.get().sendEvent(TrackingEvent.SELECTMENU, new CustomData(CustomDataTag.OPTION, CustomDataValue.SOUND.ToString()));
    }
}
