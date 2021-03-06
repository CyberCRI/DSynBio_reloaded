﻿using UnityEngine;

public static class URLOpener
{
    private static string getLocalizedUrl(string urlKey)
    {
        return Localization.Localize(urlKey);
    }

    public static void open(string urlKey, bool newTab = false, string urlParameters = "")
    {
        string localizedUrl = getLocalizedUrl(urlKey) + urlParameters;
        if (!newTab)
        {
            RedMetricsManager.get().sendEvent(TrackingEvent.GOTOURL, new CustomData(CustomDataTag.SAMETAB, localizedUrl));
            Application.OpenURL(localizedUrl);
        }
        else
        {
            RedMetricsManager.get().sendEvent(TrackingEvent.GOTOURL, new CustomData(CustomDataTag.NEWTAB, localizedUrl));
#if UNITY_WEBGL && !UNITY_EDITOR
            // Debug.Log("URLOpener open WebGL attempting to open " + localizedUrl);
            Application.ExternalEval("window.open('" + localizedUrl + "','_blank')");
#else
            // Debug.Log("URLOpener open Editor/Standalone attempting to open " + localizedUrl);
            Application.OpenURL(localizedUrl);
#endif
        }
    }
}
