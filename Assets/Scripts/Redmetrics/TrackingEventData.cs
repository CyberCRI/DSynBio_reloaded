﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//From https://github.com/CyberCRI/RedMetrics/blob/master/API.md
public abstract class TrackingEventData {

	/*



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
			foreach(TrackingEvent _trackingEvent in System.Enum.GetValues(typeof(TrackingEvent)))
			{
				if(_trackingEvent.ToString() == value)
				{
					internalTrackingEvent = _trackingEvent;
				}
			}
			if(internalTrackingEvent == TrackingEvent.DEFAULT)
			{
				Debug.LogWarning("unknown tracking event "+value);
			}
		}
		get {return internalTrackingEvent.ToString();}
	}
	private void setTrackingEvent(TrackingEvent _trackingEvent) {
		internalTrackingEvent = _trackingEvent;
	}

	//optional
	public CustomData customData;

	//optional
	public string section;

	//optional
	public int[] coordinates;


	public TrackingEventData(){ }

	public TrackingEventData(
		TrackingEvent _trackingEvent, 
		CustomData _customData = null, 
		string _section = null,
		int[] _coordinates = null
		)
	{
		//cf http://stackoverflow.com/questions/114983/given-a-datetime-object-how-do-i-get-a-iso-8601-date-in-string-format/115002#115002
		userTime = System.DateTime.UtcNow.ToString ("yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture);
	
		setTrackingEvent(_trackingEvent);
		customData = _customData;
		section = _section;
		coordinates = _coordinates;
	}

	public override string ToString ()
	{
		return string.Format ("[TrackingEventData: userTime:{0}, type:{1}, customData:{2}, section:{3}, coordinates:{4}]"
		                      ,userTime
		                      ,type
		                      ,customData
		                      ,section
		                      ,coordinates
		                      );
	}
}

public enum CustomDataTag {
    DNABIT,
    BIOBRICK,
    DEVICE,

    GAMELEVEL
}

public class CustomData: Dictionary<string, string> {

    public CustomData() {}
    
    private CustomData(string key, string value) : base() {
        this.Add(key, value);
    }

    public CustomData(CustomDataTag tag, string value) : this(tag.ToString().ToLowerInvariant(), value) {

    }

	public override string ToString ()
	{
		string content = "";
		foreach(KeyValuePair<string, string> entry in this)
		{
			if(!string.IsNullOrEmpty(content))
			{
				content += ",";
			}
			content += entry.Key+":"+entry.Value;
		}
		return string.Format ("[CustomData:[{0}]]", content);
	}
}

public class TrackingEventDataWithoutIDs : TrackingEventData {
	public TrackingEventDataWithoutIDs(
		TrackingEvent _trackingEvent, 
		CustomData _customData = null, 
		string _section = null,
		int[] _coordinates = null
	) : base(_trackingEvent, _customData, _section, _coordinates)
	{}

	public override string ToString ()
	{
		return string.Format ("[TrackingEventDataWithoutIDs]");
	}
}

public class TrackingEventDataWithIDs : TrackingEventData {
	public string player;
	public string gameVersion;

	public TrackingEventDataWithIDs(
		string _playerID,
		string _gameVersionID,
		TrackingEvent _trackingEvent, 
		CustomData _customData = null, 
		string _section = null,
		int[] _coordinates = null
	) : base(_trackingEvent, _customData, _section, _coordinates){
		player = _playerID;
		gameVersion = _gameVersionID;
	}

	public override string ToString ()
	{
		return string.Format ("[TrackingEventDataWithIDs]");
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

public class CreatePlayerData {

	public string type = TrackingEvent.CREATEPLAYER.ToString().ToLower();
  
	public override string ToString ()
	{
		return string.Format ("[CreatePlayerData: type: {0}]", type);
	}
}

public class ConnectionData {
	public string gameVersionID;

	public ConnectionData(string id) {
		gameVersionID = id;
	}

	public override string ToString ()
	{
		return string.Format ("[ConnectionData: gameVersionID: {0}]", gameVersionID);
	}
}