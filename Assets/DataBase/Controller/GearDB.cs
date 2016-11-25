using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearDB : MonoBehaviour {

	public List<GearType> gearTypes;
	public List<GearArea> areas;

	void Awake(){
		SetGearId ();
	}

	void SetGearId(){
		for (int i = 0; i < gearTypes.Count; i++) {
			gearTypes[i].id = i;
		}
	}

	public GearArea GetGearArea(int id){
		return areas[id];
	}

	public List<GearType> GetTypes(){

		List<GearType> types = new List<GearType> ();

		foreach(GearType type in gearTypes){
			types.Add (type);
		}

		return types;
	}

	public List<GearType> GetGearTypeByCollection(string collection){

		List<GearType> types = new List<GearType> ();

		foreach (GearType type in gearTypes) {
			if (gearTypes.IndexOf(type) == 0) {
				continue;
			}
			if (type.collection.ToString ().Equals (collection)) {
				types.Add (type);
			}
		}

		return types;
	}

	public GearType GetGearTypeById(int id){
		try {
			return gearTypes[id];
		} catch {
			return null;
		}
	}
}
