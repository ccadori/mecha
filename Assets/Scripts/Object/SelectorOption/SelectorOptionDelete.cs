using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectorOptionDelete : SelectorOption {

	public void Delete(){
		for (int i = 0; i < SelectorController.singleton.gears.Count; i++){
			SelectorController.singleton.gears[i].Sell ();
		}
		SelectorController.singleton.CancelSelection ();
	}
}
