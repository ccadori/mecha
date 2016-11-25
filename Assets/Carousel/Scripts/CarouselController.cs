using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CarouselController : MonoBehaviour {

	public static CarouselController singleton;

	private float panelWidth;
	private int itemsPerPage;
	private int currentPage;
	private int maximumPage;
	public delegate void ItemClick(int id);
	private ItemClick methods;

	[Header("Parameters")]
	public float itemSize;
	public float spaceBetweenItems;
	public CarouselItem[] items;

	[Header("Components")]
	public Image carouselPanel;
	public Button nextButton;
	public Button backButton;
	public GameObject button;

	public void SetCollection(CarouselItem[] newItems){
		items = newItems;
		SetDefaultSettints ();
	}

	void Awake(){
		singleton = this;
	}

	public void AddClickItemMethod(ItemClick newMethod){
		methods += newMethod;
	}

	public void NextPage(){
		if (currentPage != maximumPage) {
			currentPage++;
		}
		ActiveButtons ();
		InstantiateItems ();
	}

	public void BackPage(){
		if (currentPage - 1 > 0) {
			currentPage--;
		}
		ActiveButtons ();
		InstantiateItems ();
	}

	void SetDefaultSettints(){
		if (!CheckComponents ())
			return;

		panelWidth = carouselPanel.rectTransform.rect.width;
		itemsPerPage =  (int)(panelWidth / (itemSize + spaceBetweenItems));

		float x = (float)(items.Length) / (float)(itemsPerPage);

		if (x % (int)(x) > 0) {
			maximumPage = ((int)(x))+1;
		} else if (x >= 0 && x < 1) {
			maximumPage = 1;
		} else {
			maximumPage = (int)(x);
		}

		currentPage = 1;
		ActiveButtons ();
		InstantiateItems ();
	}

	void InstantiateItems(){
		ClearItems ();
		float test = (panelWidth - (itemsPerPage * itemSize))/2;

		for (int i = 0; i < itemsPerPage; i ++){

			int pointer = (itemsPerPage * (currentPage-1)) + i + 1;

			if (items.Length >= pointer) {
				GameObject newButton = Instantiate (
					                      button,
					                      Vector3.zero,
					                      Quaternion.Euler (Vector3.zero)
				                      ) as GameObject;
				newButton.transform.SetParent (carouselPanel.transform);
				RectTransform rt = newButton.GetComponent<RectTransform> ();
				Button bt = newButton.GetComponent<Button> ();
				Image im = newButton.GetComponent<Image> ();
				float x = (i * itemSize) + (itemSize / 2) + (spaceBetweenItems * i);
				rt.anchoredPosition = new Vector3 (x + test, 0, 0);
				rt.sizeDelta = new Vector2 (itemSize, itemSize);
				if (methods != null) {
					bt.onClick.AddListener (delegate {
						methods(items[pointer-1].id);
					});
				}
				bt.name = items [pointer - 1].id.ToString ();
				im.sprite = items[pointer-1].image;
			}
		}
	}

	public void OnItemEnter(GameObject item){
		
	}

	public void OnItemExit(){
		
	}

	void ClearItems(){
		foreach (Transform x in carouselPanel.transform) {
			Destroy (x.gameObject);
		}
	}

	void ActiveButtons(){
		if (currentPage > 1) {
			backButton.interactable = true;
		} else if (currentPage == 1) {
			backButton.interactable = false;
		}

		if (currentPage < maximumPage) {
			nextButton.interactable = true;
		} else if (currentPage == maximumPage) {
			nextButton.interactable = false;
		}
	}

	bool CheckComponents(){
		if (carouselPanel == null) {
			Debug.LogError ("Empty components.");
			return false;
		}
		return true;
	}
}