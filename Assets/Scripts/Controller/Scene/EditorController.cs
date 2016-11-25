using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

public class EditorController : MonoBehaviour {

	public static EditorController singleton;

	private GameObject draggedObject;
	private int draggedId;

	void Awake(){
		singleton = this;
	}

	void Start(){
		EditorUIController.singleton.UpdateCarousel ();
		GearControllerEditor.singleton.LoadGearSet ();
	}

	public bool Buy(){
		PlayerSettingsController.singleton.SubtractCoin (StoreController.singleton.gearDb.GetGearTypeById (draggedId).price);
		return true;
	}

	public bool Sell(int idType){
		PlayerSettingsController.singleton.AddCoin (StoreController.singleton.gearDb.GetGearTypeById (idType).price);
		return true;
	}

	void Update(){
		if (draggedObject == null) {
			if (Input.GetMouseButtonDown (00) && !EditorUIController.singleton.InUIClick()) {
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit2D[] hits = Physics2D.RaycastAll (mousePosition, Vector2.zero);
				if (hits.Length > 0) {
					int find = 0;
					foreach (RaycastHit2D hit in hits) {
						
						if (hit.collider.CompareTag ("Gear")) {
							Gear gear = hit.collider.gameObject.GetComponent<Gear> ();
							SelectorController.singleton.AddSelection (gear);
							find++;
						}
					}

					if (find == 0) {
						SelectorController.singleton.CancelSelection ();
					}
				} else {

					SelectorController.singleton.CancelSelection ();
				}
			}
		}
	}

	public void IntantiateDragger(int id){
		CancelDrag ();
		if (!PlayerSettingsController.singleton.CanSubtractCoin(StoreController.singleton.gearDb.GetGearTypeById(id).price))
			return;
		draggedId = id;
		draggedObject = new GameObject ();
		draggedObject.name = "Drag";
		DraggedController controller = draggedObject.AddComponent<DraggedController> ();
		GearType type =StoreController.singleton.gearDb.GetGearTypeById (draggedId);
		controller.SetDragged (StoreController.singleton.gearDb.GetGearArea(type.gearArea), type.icon);
	}

	public void Drop(){
		if (!EditorUIController.singleton.InUIClick() && draggedId != 0 && draggedObject != null) {
			List<GearFile> files = new List<GearFile> ();
			files.Add (new GearFile () {
				x = (int)draggedObject.transform.position.x,
				y = (int)draggedObject.transform.position.y,
				rotation = (int)draggedObject.transform.rotation.eulerAngles.z,
				type = draggedId
			});
			GearControllerEditor.singleton.InstantiateGear (files);
		}
	}

	public void CancelDrag(){
		draggedObject = null;
		draggedId = 0;
	}
}