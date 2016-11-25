using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSettingsController : MonoBehaviour {

	public static PlayerSettingsController singleton;

	private PlayerSettings playerSettings;
	private List<GearFile> gearSet;

	void Awake(){
		singleton = this;
	}

	public void UpdateCoin(){
		CoinController.singleton.SetCoinQuantity(playerSettings.coins);
	}

	public List<GearFile> GetGearSet(){
		gearSet = XMLParser.GetGearFileFromXML ();
		if (gearSet != null) {
			Debug.Log ("Load Gear Successfull");
		} else {
			Debug.Log ("Load Gear Failed");
		}
		return gearSet;
	}

	public void SetGearFiles(List<GearFile> files){
		gearSet = files;
		if (XMLParser.WriteGearFileOnXML (gearSet)) {
			Debug.Log ("Save Gear successfull");
		} else {
			Debug.Log ("Save Gear Fail");
		}
	}

	public void GetPlayerSettings(){
		playerSettings = XMLParser.GetPlayerSettings ();
		if (playerSettings != null) {
			Debug.Log ("Load Settings Successfull");
			UpdateCoin ();
		} else {
			Debug.Log ("Load Settings Fail");
		}
	}

	public void ResetPlayerSettings(){
		GetPlayerSettings ();
	}

	public void SetPlayerSettings(){
		if (playerSettings != null) {
			if (XMLParser.WritePlayerSettings (playerSettings)) {
				Debug.Log ("Save Player Settings Successfull");
			} else {
				Debug.Log ("Save Player Settings Fail");
			}
		} 
	}

	public int GetCoin(){
		return playerSettings.coins;
	}

	public void AddCoin(int quantity){
		playerSettings.coins += quantity;
		UpdateCoin ();
	}

	public bool CanSubtractCoin(int quantity){
		if ((playerSettings.coins - quantity) >= 0) {
			return true;
		} else {
			return false;
		}
	}

	public void SubtractCoin(int quantity){
		playerSettings.coins -= quantity;
		UpdateCoin ();
	}
}
