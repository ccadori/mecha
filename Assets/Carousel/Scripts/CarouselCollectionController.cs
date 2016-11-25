using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarouselCollectionController : MonoBehaviour {

	public static CarouselCollectionController singleton;

	[Header("Parameters")]
	public CarouselCollection[] collections;

	[Header("Components")]
	public Dropdown dropdown;

	void Awake(){
		singleton = this;
	}

	public void SetCollections(CarouselCollection[] newCollections){
		collections = newCollections;
		SetDropdownCollection ();
	}

	void SetDropdownCollection(){
		List<string> options = new List<string>();
		dropdown.ClearOptions ();
		if (collections.Length != 0) {
			for (int i = 0; i < collections.Length; i++) {
				options.Add (collections [i].collectionName);
			}
			dropdown.AddOptions (options);
			dropdown.onValueChanged.AddListener (delegate {
				ValueChange ();	
			});

			ValueChange ();
		}
	}

	public void ValueChange(){
		CarouselController.singleton.SetCollection (collections[dropdown.value].items.ToArray());
	}
}