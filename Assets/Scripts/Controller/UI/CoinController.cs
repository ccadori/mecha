using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinController : MonoBehaviour {

	public static CoinController singleton;

	public Text coinQuantity;

	void Awake(){
		singleton = this;
	}

	public void SetCoinQuantity(int quantity){
		coinQuantity.text = quantity.ToString();
	}
}
