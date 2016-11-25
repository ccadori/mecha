using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectorController : MonoBehaviour {

	public static SelectorController singleton;

	[Header("Components")]
	public GameObject selectionObject;
	public Image selectionPanel;
	public Text title;
	public SelectorOption[] options;

	private List<GameObject> selections = new List<GameObject>();
	public List<Gear> gears = new List<Gear> ();

	void Awake(){
		singleton = this;
	}

	public void AddSelection(Gear targetGear){

		if (!gears.Contains (targetGear)) {
			
			gears.Add (targetGear);
			GameObject selection = Instantiate (StoreController.singleton.selection, new Vector2(targetGear.file.x, targetGear.file.y), Quaternion.Euler(Vector3.zero)) as GameObject;
			selections.Add (selection);
		} else {
			int index = gears.IndexOf (targetGear);
			GameObject selection = selections [index];
			selections.RemoveAt (index);
			Destroy (selection);
			gears.RemoveAt (index);
		}

		UpdateSelectionObject ();
	}

	void UpdateSelectionObject(){

		if (gears.Count == 1) {
			title.text = StoreController.singleton.gearDb.GetGearTypeById (gears[0].file.type).gearName;
			foreach(SelectorOption option in options){
				option.UpdateOption ();
			}
			selectionObject.SetActive (true);
		} else if (gears.Count > 1) {
			
			if (GearControllerEditor.singleton.GearFilesAreSameType (GearControllerEditor.singleton.GetGearFilesFromGears(gears))) {
				title.text = title.text = StoreController.singleton.gearDb.GetGearTypeById (gears[0].file.type).gearName + " (" + gears.Count + ")";
			} else {
				title.text = "Diferent Type (" + gears.Count + ")";
			}
			foreach(SelectorOption option in options){
				option.UpdateOption ();
			}
			selectionObject.SetActive (true);
		} else {
			title.text = "";		
			selectionObject.SetActive (false);
		}
	}

	public void CancelSelection(){
		foreach(GameObject selection in selections){
			Destroy (selection);
		}
		gears.Clear ();
		selections.Clear ();
		UpdateSelectionObject ();
	}
}