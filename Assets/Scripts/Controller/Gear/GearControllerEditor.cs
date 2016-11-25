using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearControllerEditor : GearController {

	public static GearControllerEditor singleton;

	void Awake(){
		singleton = this;
	}

	public override void InstantiateGear (List<GearFile> files)
	{
		base.InstantiateGear (files);
		foreach(GearFile file in files){
			GearType type = StoreController.singleton.gearDb.GetGearTypeById(file.type);
			GameObject newGear = Instantiate (type.prefab) as GameObject;
			newGear.transform.SetParent (transform);
			newGear.name = file.x.ToString() + file.y.ToString();
			Gear gear = newGear.GetComponent<Gear> ();
			gear.SetFile (file);
			gear.SetGearArea (StoreController.singleton.gearDb.GetGearArea(type.gearArea));
		}
	}

	public bool CheckTileIsBlank(Vector2 pos){
		foreach (Transform gear in transform) {
			if (gear.name.Equals (pos.x.ToString() + pos.y.ToString())) {
				return false;
			}
		}
		return true;
	}

	public bool CheckTileIsBlocked(Vector2 pos){
		bool over = false;
		foreach (Transform gear in transform) {
			if ((Vector2)gear.transform.position == pos) {
				if (!gear.CompareTag ("Add")) {
					over = true;
					break;
				}
			}
		}
		return over;
	}

	public bool CheckTileIsOverJustAdd(Vector2 pos){

		bool over = false;
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			Transform child = transform.GetChild (i);
			if ((Vector2)child.position == pos) {
				if (child.CompareTag ("Add")) {
					over = true;	
				} else {
					over = false;
					break;
				}
			}
		}

		return over;
	}

	public bool CheckTileIsOverAdd(Vector2 pos){
		
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			Transform child = transform.GetChild (i);
			if ((Vector2)child.position == pos) {
				if (child.CompareTag ("Add")) {
					return true;
				}
			}
		}
		return false;
	}

	public Gear GetGearByPos(Vector2 pos){

		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			Transform child = transform.GetChild (i);
			if ((Vector2)child.position == pos) {
				if (child.CompareTag ("Gear")) {
					return child.GetComponent<Gear> ();
				}
			}
		}
		return null;
	}

	public void CascateSell(GearFile file){
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (!(y == 0 && x == 0) && !(y == 1 && x == 1) && (x == 0 || y == 0)) {
					Vector2 pos = new Vector2 (x + file.x, y + file.y);
					Gear gear = GetGearByPos (pos);
					if (gear != null) {
						if (!CheckTileIsOverAdd (pos)) {
							gear.Sell ();
						}
					}
					gear = null;
				}
			}
		}
	}

	public void SetFilesPowerKey(List<GearFile> gears, KeyCode key){
		for (int i = 0; i < gears.Count; i++) {
			gears [i].activeKey = key;
		}
	}
}