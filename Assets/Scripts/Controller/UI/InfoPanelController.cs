using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelController : MonoBehaviour {

	public static InfoPanelController singleton;

	public Text price;
	public Text title;
	public Text description;

	public GameObject panel;

	void Awake(){
		singleton = this;
	}

	public void ShowPanel(string sTitle, string sPrice, string sDescription){
		title.text = sTitle;
		price.text = "Price: " + sPrice;
		description.text = sDescription;

		panel.SetActive (true);
	}

	public void HidePanel(){
		panel.SetActive (false);
	}
}
