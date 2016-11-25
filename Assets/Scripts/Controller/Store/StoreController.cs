using UnityEngine;
using System.Collections;

public class StoreController : MonoBehaviour {
		
	public static StoreController singleton;

	[Header("DataBase")]
	public GearDB gearDb;

	[Header("Prefabs")]
	public GameObject addAreaPrefab;
	public GameObject blockAreaPrefab;
	public GameObject selection;

	void Awake(){
		singleton = this;
	}
}
