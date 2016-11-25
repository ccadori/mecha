using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class EditorUIController : MonoBehaviour {

	public static EditorUIController singleton;

	void Awake(){
		singleton = this;
	}

	void Start(){
		PlayerSettingsController.singleton.GetPlayerSettings ();
	}

	public void LoadTesteScene(){
		SaveButton ();
		SceneManager.LoadScene (1);
	}

	public void CarouselItemEnter(int id){
		GearType type = StoreController.singleton.gearDb.GetGearTypeById (id);
		InfoPanelController.singleton.ShowPanel (type.gearName, type.price.ToString(), type.description);
	}	

	public void CarouselItemOut(){
		InfoPanelController.singleton.HidePanel ();
	}

	public void SaveButton(){
		GearControllerEditor.singleton.SaveGear ();
		PlayerSettingsController.singleton.SetPlayerSettings ();
	}

	public void LoadButton(){
		GearControllerEditor.singleton.LoadGearSet ();
		PlayerSettingsController.singleton.ResetPlayerSettings ();

	}

	public void ItemCarouselClick(int id){
		EditorController.singleton.IntantiateDragger (id);
	}

	public bool InUIClick(){
		return EventSystem.current.IsPointerOverGameObject ();
	}

	public void UpdateCarousel(){
		List<CarouselCollection> collections = GetCollections ();
		collections = SetItems (collections);
		CarouselCollectionController.singleton.SetCollections (collections.ToArray());
		CarouselController.singleton.AddClickItemMethod (ItemCarouselClick);
	}

	List<CarouselCollection> SetItems(List<CarouselCollection> collections){
		foreach(CarouselCollection collection in collections){
			List<GearType> types = StoreController.singleton.gearDb.GetGearTypeByCollection (collection.collectionName);
			foreach(GearType type in types){
				collection.items.Add (new CarouselItem(){id = type.id, image = type.icon});
			}
		}

		return collections;
	}

	List<CarouselCollection> GetCollections(){
		List<CarouselCollection> collections = new List<CarouselCollection> ();

		foreach (var collectionName in Enum.GetValues(typeof(GearCollection))){
			collections.Add (new CarouselCollection (){ collectionName = collectionName.ToString() });
		}

		return collections;
	}
}