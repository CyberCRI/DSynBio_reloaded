using UnityEngine;
using System.Collections;

public class AvailableTerminatorsButton : MonoBehaviour {
  public AvailableBioBricksManager _availableBioBricksManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnPress(bool isPressed) {
    if(isPressed) _availableBioBricksManager.displayTerminators();
  }
}
