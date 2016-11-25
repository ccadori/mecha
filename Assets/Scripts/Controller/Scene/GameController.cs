using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController singleton;

	void Awake(){
		singleton = this;
	}

	void Start(){
		GearControllerGame.singleton.LoadGearSet ();
	}
}
