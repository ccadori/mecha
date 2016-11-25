using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System;

public class SelectorOptionPowerButton : SelectorOption {
		
	public Text powerButton;
	public GameObject blockScreen;

	public Event ev;

	private bool waiting;

	void Update(){
		if (waiting) {

			foreach(KeyCode currentKey in Enum.GetValues(typeof(KeyCode))){
				if (Input.GetKeyDown (currentKey)) {
					StopGetKey (currentKey);
				}
			}
		}
	}

	public override void UpdateOption ()
	{	
		if (GearControllerEditor.singleton.GearFilesHaveSameKey (GearControllerEditor.singleton.GetGearFilesFromGears(SelectorController.singleton.gears))) {
			powerButton.text = "Power Button (" + SelectorController.singleton.gears [0].file.activeKey.ToString () + ")";
		} else {
			powerButton.text = "Power Button (Multiple)";
		}
		base.UpdateOption ();
	}

	public void StopGetKey(KeyCode currentKey){
		if (currentKey != KeyCode.Escape) {
			GearControllerEditor.singleton.SetFilesPowerKey (GearControllerEditor.singleton.GetGearFilesFromGears (SelectorController.singleton.gears), currentKey);
			UpdateOption ();
		}
		waiting = false;
		blockScreen.SetActive (false);
	}

	public void StartGetKey(){
		waiting = true;
		blockScreen.SetActive(true);
	}
}
