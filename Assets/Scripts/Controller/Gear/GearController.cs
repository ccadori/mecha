using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearController : MonoBehaviour {

	public void LoadGearSet (){
		ClearGears ();
		InstantiateGear (PlayerSettingsController.singleton.GetGearSet());
	}

	public void SaveGear(){
		List<GearFile> files = new List<GearFile> ();

		foreach (Transform file in transform) {
			Gear gear = file.GetComponent<Gear> ();
			if (gear != null) {
				files.Add (gear.file);
			}
		}
		PlayerSettingsController.singleton.SetGearFiles (files);
	}

	public virtual void InstantiateGear(List<GearFile> files){
		
	}

	public void ClearGears(){
		foreach (Transform t in transform) {
			Destroy (t.gameObject);
		}
	}

	public List<GearFile> GetGearFilesFromGears(List<Gear> gears){
		List<GearFile> files = new List<GearFile> ();
		foreach(Gear gear in gears){
			files.Add (gear.file);
		}
		return files;
	}

	public bool GearFilesAreSameType(List<GearFile> files){

		int firstType = files[0].type;

		foreach(GearFile file in files){
			if (file.type != firstType) {
				return false;
			}
		}

		return true;
	}

	public bool GearFilesHaveSameKey(List<GearFile> files){

		string firstKey = files [0].activeKey.ToString ();
		foreach(GearFile file in files){
			if (!file.activeKey.ToString().Equals(firstKey)) {
				return false;
			}
		}

		return true;
	}
}