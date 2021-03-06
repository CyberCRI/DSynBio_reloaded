﻿using UnityEngine;
using System.Collections.Generic;

//From https://github.com/CyberCRI/RedMetrics/blob/master/API.md
public abstract class TrackingEventData
{

    /* RedMetrics API
    
    userTime - Date sent by the game (optional)

    serverTime - Date generated by the server

    type - String

    customData - Any data structure. For "gain" and “lose” events, specifies the number of things are gained or lost.

    section - Section (optional)

    coordinates - Coordinate where the event occurred (optional)
    
   */ 

    //optional
    //date in ISO 8601 format
    public string userTime;

    //managed by RedMetrics server
    //Time serverTime;
  
    private TrackingEvent internalTrackingEvent;

    public string type {
        set {
            internalTrackingEvent = TrackingEvent.DEFAULT;
            foreach (TrackingEvent _trackingEvent in System.Enum.GetValues(typeof(TrackingEvent))) {
                if (_trackingEvent.ToString () == value) {
                    internalTrackingEvent = _trackingEvent;
                }
            }
            if (internalTrackingEvent == TrackingEvent.DEFAULT) {
                Debug.LogWarning ("unknown tracking event " + value);
            }
        }
        get { return internalTrackingEvent.ToString ();}
    }

    private void setTrackingEvent (TrackingEvent _trackingEvent)
    {
        internalTrackingEvent = _trackingEvent;
    }
    public TrackingEvent getTrackingEvent () {
        return internalTrackingEvent;
    }

    //optional
    public CustomData customData;

    //optional
    public string section;

    //optional
    public int[] coordinates;

    public TrackingEventData ()
    {
    }

    public TrackingEventData (
    TrackingEvent _trackingEvent, 
    CustomData _customData = null, 
    string _section = null,
    int[] _coordinates = null,
    string _userTime = null
    )
    {
        //cf http://stackoverflow.com/questions/114983/given-a-datetime-object-how-do-i-get-a-iso-8601-date-in-string-format/115002#115002
        userTime = string.IsNullOrEmpty(_userTime)?System.DateTime.UtcNow.ToString ("yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture):_userTime;
  
        setTrackingEvent (_trackingEvent);
        customData = _customData;
        section = _section;
        coordinates = _coordinates;
    }

    public override string ToString ()
    {
        return string.Format ("[TrackingEventData: userTime:{0}, type:{1}, customData:{2}, section:{3}, coordinates:{4}]"
                          , userTime
                          , type
                          , customData
                          , section
                          , coordinates
        );
    }
}

public enum CustomDataTag
{
    LOCALPLAYERGUID,    //for GUID stored in local PlayerPrefs
    GLOBALPLAYERGUID,   //for GUID associated to an account
    
    PLATFORM,           //the runtime platform on which the game is run
    
    DNABIT,
    BIOBRICK,
    DEVICE,

    NANOBOT,
    PLASMID,

    SOURCE,             // cause of death or source of event - webpage or game

    DEVICES,            // event context: devices
    LIFE,               // event context: life
    ENERGY,             // event context: energy

    SLOT,

    GAMELEVEL,

    OPTION,

    CONTROLS,
    LANGUAGE,
    GRAPHICS,
    SOUND,

    NEWTAB,
    SAMETAB,

    COUNT,

    CHAPTER,
    TOTAL,

    MESSAGE,            // hint message that was displayed

    DURATION
}

public enum CustomDataValue
{
    // sound states
    ON,
    OFF,

    // main menu entries
    START,
    RESUME,
    RESTART,
    ADVENTURE,
    SANDBOX,
    SETTINGS,
    CONTROLS,
    LANGUAGE,
    GRAPHICS,
    SOUND,
    SCIENCE,
    LEARNMORE,
    CONTRIBUTE,
    QUIT,

    // death causes
    MINE,               // instant death - stepped on a mine
    ENEMY,              // instant death - collided with an enemy
    SUICIDEBUTTON,      // instant death - used the suicide button
    CRUSHED,            // instant death - crushed by a door
    OUTOFBOUNDS,        // instant death - got out of playing zone through a bug or bad LB
    NOENERGY,           // no energy left
    AMPICILLIN,         // ampicillin toxins - walls or self-production
    MULTIPLE,           // multiple non instant-death causes: NOENERGY & AMPICILLIN
	AMPICILLINWALL1,	// ampicilin wall 1
	AMPICILLINWALL2,	// ampicilin wall 2
    UNKNOWN,            // ?

    GAME,               // source of event: some events can be triggered from game or webpage
    WEBPAGE,
	QUITYES,
	QUITNO,
	CONTRIBUTEMAINMENU,
	CONTRIBUTEHUD,
	CONTRIBUTEEND,
	CONTRIBUTEQUIT,
	CONTRIBUTETOOLBAR,
    CONTRIBUTESPEECHBUBBLE,
    QUITCROSSMENU,
    QUITCROSSHUD,
    RESETCONFIGURATION,    
}

public class CustomData: Dictionary<string, string>
{

    public CustomData ()
    {
    }
    
    private CustomData (string key, string value) : base()
    {
        this.Add (key, value);
    }

    public CustomData (CustomDataTag tag, string value) : this(tag.ToString().ToLowerInvariant(), value)
    {

    }
    
    public void Add (CustomDataTag tag, string value) {
        Add(tag.ToString().ToLowerInvariant(), value);
    }

    /// <summary>
    /// Merges data into this.
    /// </summary>
    public void merge (CustomData data)
    {
        // Debug.Log(this.GetType() + " merge " + data + " into " + this);
        if (null != data)
        {
            foreach(KeyValuePair<string, string> pair in data)
            {
                if (this.ContainsKey(pair.Key))
                {
                    // this key was already present
                    // each key-value pair type needs a specific treatment
                    Debug.LogWarning(this.GetType() + " key " + pair.Key + " present in both CustomData objects " + data + " and " + this);
                }
                else
                {
                    // new key
                    this.Add(pair.Key, pair.Value);
                }                
            }
        }
    }

    public override string ToString ()
    {
        string content = "";
        foreach (KeyValuePair<string, string> entry in this) {
            if (!string.IsNullOrEmpty (content)) {
                content += ",";
            }
            content += entry.Key + ":" + entry.Value;
        }
        return string.Format ("[CustomData:[{0}]]", content);
    }
}

public class TrackingEventDataWithoutIDs : TrackingEventData
{
    public TrackingEventDataWithoutIDs (
    TrackingEvent _trackingEvent, 
    CustomData _customData = null, 
    string _section = null,
    int[] _coordinates = null,
    string userTime = null
    ) : base(_trackingEvent, _customData, _section, _coordinates, userTime)
    {
    }

    public override string ToString ()
    {
        return string.Format ("[TrackingEventDataWithoutIDs _trackingEvent:{0} _customData:{1} _section:{2} _coordinates:{3}]",
                              type, customData, section, coordinates);
    }
}

public class TrackingEventDataWithIDs : TrackingEventData
{
    public System.Guid player;
    public System.Guid gameVersion;

    public TrackingEventDataWithIDs (
        string _playerGuid,
        string _gameVersionGuid,
        TrackingEvent _trackingEvent, 
        CustomData _customData = null, 
        string _section = null,
        int[] _coordinates = null
    ) : base(_trackingEvent, _customData, _section, _coordinates)
    {
        player = new System.Guid(_playerGuid);
        gameVersion = new System.Guid(_gameVersionGuid);
    }
    
    public TrackingEventDataWithIDs (
        System.Guid _playerGuid,
        System.Guid _gameVersionGuid,
        TrackingEvent _trackingEvent, 
        CustomData _customData = null, 
        string _section = null,
        int[] _coordinates = null
    ) : base(_trackingEvent, _customData, _section, _coordinates)
    {
        player = new System.Guid(_playerGuid.ToByteArray());
        gameVersion = new System.Guid(_gameVersionGuid.ToByteArray());
    }

    public override string ToString ()
    {
        return string.Format ("[TrackingEventDataWithIDs _playerGuid:{0} _gameVersionGuid:{1} _trackingEvent:{2} _customData:{3} _section:{4} _coordinates:{5}]",
                              player, gameVersion, type, customData, section, coordinates);
    }
}

/* given up on TypedInfo <- CreatePlayerData
 * TypedInfo <- TrackingEventData
 * caused problems with JsonWriter: JsonException: 'TrackingEventDataWithIDs' already contains the field or alias name 'type'
LitJson.JsonMapper.AddObjectMetadata (System.Type type) (at Assets/UnityLitJson/JsonMapper.cs:242)

public class TypedInfo {
  public string type;

  public TypedInfo() {}

  public override string ToString ()
  {
    return string.Format ("[TypedInfo]");
  }
}
*/

public class CreatePlayerData
{
    public string type = (new TrackingEventDataWithoutIDs(TrackingEvent.CREATEPLAYER)).type;
  
    public override string ToString ()
    {
        return string.Format ("[CreatePlayerData: type: {0}]", type);
    }
}

public class ConnectionData
{
    public System.Guid gameVersionId;

    public ConnectionData (string id)
    {
        gameVersionId = new System.Guid(id);
    }
    
    public ConnectionData (System.Guid id)
    {
        gameVersionId = new System.Guid(id.ToByteArray());
    }

    public override string ToString ()
    {
        return string.Format ("[ConnectionData: gameVersionId: {0}]", gameVersionId);
    }
}